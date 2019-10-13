using System;
using System.Collections.Generic;
using System.Text;
using HyTestIEInterface;
using HyTestEtherCAT.localEntity;
using System.Windows.Forms;
using HyTestIEInterface.Entity;
using log4net;
using ServiceStack.Redis;
using System.Threading;

namespace HyTestEtherCAT
{
    public class EtherCAT : ConnectionContext, IConnection, IAdapterLoader, IDeviceLoader, IReader, IWriter, IRedisReader
    {
        #region region_属性
        /// <summary>
        /// EtherCAT刷新频率
        /// </summary>
        public static int RefreshFrequency{ get; set; }
        /// <summary>
        /// 所选网卡
        /// </summary>
        public int AdapterSelected { get; set; }

        public IDataStruct<object>[] Buffer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ip
        {
            get
            {
                return this.redisIp;
            }

            set
            {
                this.redisIp = value;
            }
        }

        public int port
        {
            get
            {
                return this.redisPort;
            }

            set
            {
                this.redisPort = value;
            }
        }
        #endregion

        #region region_常量
        private const int ERRCODE = 0;
        private const int DEFAULT_FREQUENCY = 300;
        #endregion

        #region region_成员变量
        ILog log = LogManager.GetLogger(typeof(EtherCAT));

        private static EtherCAT ethercat;
        public static EtherCAT getEtherCAT(int refreshFrequency)//单例
        {
            if(ethercat == null) ethercat = new EtherCAT(refreshFrequency);
            else if(ethercat != null && RefreshFrequency==0 ) {
                RefreshFrequency = refreshFrequency;
            }

            return ethercat;
        }
        public static EtherCAT getEtherCAT()
        {
            if (ethercat == null) ethercat = new EtherCAT();

            return ethercat;
        }

        private bool isLoadedDriver = false;

        

        #endregion

        #region region_构造函数
        private EtherCAT()
        {
            InitRedisSubjectConfig(DEFAULT_FREQUENCY);
        }
        private EtherCAT(int refreshFrequency)
        {
            InitRedisSubjectConfig(refreshFrequency);
        }

        #endregion

        #region region_事件

        public event EventHandler<EventArgs> DataChanged;
        public event EventHandler<EventArgs> RedisDataChanged;

        #endregion

        #region region_连接管理

        public int Close()
        {
            throw new NotImplementedException();
        }

        public int Connect(int ID)
        {
            SetAdapterFromConfig(ID);
            BuildConnection();
            return 1;
        }

        public int Disconnect()
        {
            CppConnect.stopRunning();
            return 1;
        }

        // 建立连接状态，包括：初始化网卡，选择网卡，初始化从站
        private void BuildConnection()
        {
            if (isLoadedDriver) return;

            try
            {
                CppConnect.getAdapterNum();
                CppConnect.setAdapterId(this.AdapterSelected);

                this.GetDevice();
                //StartTimer();
                redisSubjectThread = new Thread(subjectRedis);
                redisSubjectThread.Start();

                isLoadedDriver = true;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message+"\n"+ex.StackTrace);
            }
        }

        // 将本地adapterSelected设置为给定值 <returns>成功返回1，失败返回2</returns>
        public int SetAdapterFromConfig(int AdapterId)
        {
            if (this.AdapterSelected == 0)
            {
                this.AdapterSelected = AdapterId;
                //BuildConnection();
                return 1;
            }
            else return 2;
        }
        #endregion

        #region region_硬件
        public int InitDevice()
        {
            return CppConnect.initSlaveConfig();
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        public List<List<IOdevice>> GetDevice()
        {
            int slaveNum = InitDevice();
            deviceNum = slaveNum == 0 ? 0 : slaveNum;

            List<List<IOdevice>> deviceContiner = new List<List<IOdevice>>();   //全部device
            List<IOdevice> devGroup = null;     //一个device组
            IOdevice tmpDevice = null;

            for (int i = 1; i < slaveNum + 2; i++)
            {
                SlaveInfo tmpSlave = new SlaveInfo();
                StringBuilder tmpSlaveName = new StringBuilder();
                tmpSlaveName.Capacity = 128;

                int err = CppConnect.getSlaveInfo(tmpSlaveName, ref tmpSlave, i);

                if (tmpSlave.type == 10 || tmpSlave.type == 20) //耦合器或驱动器
                {
                    if (devGroup != null) deviceContiner.Add(devGroup);
                    devGroup = new List<IOdevice>();
                }
                
                tmpDevice = new IOdevice(tmpSlave.id, DeviceType.NULL, tmpSlaveName.ToString(), tmpSlave.channelNum);
                //type
                switch (tmpSlave.type)
                {
                    case 1:
                        tmpDevice.type = DeviceType.DI;
                        break;
                    case 2:
                        tmpDevice.type = DeviceType.DO;
                        break;
                    case 3:
                        tmpDevice.type = DeviceType.AI;
                        break;
                    case 4:
                        tmpDevice.type = DeviceType.AO;
                        break;
                    case 10:
                        tmpDevice.type = DeviceType.COUPLER;
                        break;
                    case 20:
                        tmpDevice.type = DeviceType.SOLVER;
                        break;
                    default:
                        break;
                }

                devGroup.Add(tmpDevice);
                tmpSlaveName.Clear();
            }
            deviceContiner.Add(devGroup);
            return deviceContiner;
        }

        public int GetDeviceNum()
        {
            return deviceNum;
        }

        #endregion

        #region region_读写
        //数据刷新时触发，目前做成Timer触发，后面应该是Redis更新触发
        public void OnDataRefresh(object sender, EventArgs e)
        {
            DataChanged(this, new EventArgs());
        }
        
        /// <summary>
        /// 读模拟量（单个）
        /// </summary>
        /// <returns>返回-1表示失败</returns>
        public int ReadAnalog(int deviceId, int channel)
        {
            int tmpValue = CppConnect.getAnalogValue(deviceId, channel);
            return tmpValue;
        }

        /// <summary>
        /// 读取数字量（单个）
        /// </summary>
        public bool ReadDigital(int deviceId, int channel)
        {
            int tmpValue = CppConnect.getDigitalValue(deviceId, channel);
            if (tmpValue == 0) return false;
            else if (tmpValue == 1) return true;
            else
            {
                //throw new Exception("读取bool值错误，请检查:"+typeof(EtherCAT));
                return false;
            }
        }

        /// <summary>
        /// 写模拟量（单个）
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int WriteAnalog(int deviceId, int channel, int value)
        {
            return CppConnect.setAnalogValue(deviceId, channel, value);
        }

        /// <summary>
        /// 写数字量（单个）
        /// </summary>
        public int WriteDigital(int deviceId, int channel, byte value)
        {
            return CppConnect.setDigitalValue(deviceId, channel, value);
        }

        //TODO:批量读 
        public int ReadAnalog(List<int> deviceList, List<int[]> channelList, ref List<int[]> values)
        {
            throw new NotImplementedException();
        }

        //TODO:
        public int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<byte[]> values)
        {
            throw new NotImplementedException();
        }

        //TODO: 
        public int WriteAnalog(List<int> deviceList, List<int[]> channelList, List<int[]> values)
        {
            bool succeed = true;
            for (int i = 0; i < deviceList.Count; i++)
            {
                int device = deviceList[i];
                //TODO: complete these write
            }
            return succeed ? 0 : -1;
        }

        //TODO: foreach write
        public int WriteDigital(List<int> deviceList, List<int[]> channelList, List<byte[]> values)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region region_网卡
        public Adapter[] GetAdapter()
        {
            adapterNum = CppConnect.getAdapterNum();
            adapters = new Adapter[adapterNum];

            StringBuilder tmpAdapterName = new StringBuilder();
            StringBuilder tmpAdapterDesc = new StringBuilder();
            tmpAdapterName.Capacity = 128;
            tmpAdapterDesc.Capacity = 128;
            for (int i = 0; i < adapterNum; i++)
            {
                int err = CppConnect.getAdapterName(tmpAdapterName, tmpAdapterDesc, i);
                if (err <= 0)//有错误
                {
                    throw new Exception(); //一般来讲是这个错误
                }
                adapters[i] = new Adapter();
                adapters[i].name = tmpAdapterName.ToString();
                adapters[i].desc = tmpAdapterDesc.ToString();
                tmpAdapterName.Clear();
                tmpAdapterDesc.Clear();
            }
            return adapters;
        }

        /// <summary>
        /// 选择网卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns>成功返回100，失败返回-1</returns>
        public int SetAdapter(int id)
        {
            int errmsg = CppConnect.setAdapterId(id);   //返回-1表示赋值失败
            if (errmsg < 0)
            {
                throw new Exception("Adapter selected failed");
            }
            return errmsg;
        }

        #endregion

        #region redis method
        private Thread redisSubjectThread;
        private RedisClient client = null;
        private IRedisSubscription subscription = null;
        private string redisIp = "127.0.0.1";
        private int redisPort = 6379;
        private IDataStruct<object> buffer = null;
        private string listenChannel = "channel1";

        //this method need to op. cpp file
        public void SetRefreshParams(int interval)
        {

        }

        private void InitRedisSubjectConfig(int interval)
        {
            client = new RedisClient(ip, port);
            subscription = client.CreateSubscription();
            //收到消息时
            subscription.OnMessage = (channel, msg) =>
            {
                string key = msg;
                Dictionary<string, string> values = client.GetAllEntriesFromHash(key);
                this.Buffer = transDictToBuffer(values);
                if (RedisDataChanged != null) RedisDataChanged(null, null);   //remind all
            };
            //订阅频道时
            subscription.OnSubscribe = (channel) =>
            {
                log.Info("redis客户端启动正常，正在开启数据订阅。CHANNEL_ID = " + channel);
            };
            //取消订阅频道时
            subscription.OnUnSubscribe = (a) => { log.Info("Redis客户端已停止订阅。"); };

            redisSubjectThread = new Thread(subjectRedis);
        }

        public void StartRedisRead()
        {
            this.redisSubjectThread.Start();
        }

        public void StopRedisRead()
        {
            this.redisSubjectThread.Abort();
        }

        public void subjectRedis()
        {
            //订阅频道(becareful this is a block method)
            subscription.SubscribeToChannels(listenChannel);   
        }

        private IDataStruct<object>[] transDictToBuffer(Dictionary<string, string> values)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
