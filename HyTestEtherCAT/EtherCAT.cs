using System;
using System.Collections.Generic;
using System.Text;
using HyTestIEInterface;
using HyTestEtherCAT.localEntity;
using System.Windows.Forms;
using HyTestIEInterface.Entity;

namespace HyTestEtherCAT
{
    public class EtherCAT : ConnectionContext, IConnection, IAdapterLoader, IDeviceLoader, IReader, IWriter
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
        #endregion

        #region region_常量
        private const int SAFECODE = 0;
        #endregion

        #region region_成员变量
        Timer timer = new Timer();

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
        private EtherCAT() { }
        private EtherCAT(int refreshFrequency)
        {
            InitTimer(refreshFrequency);
            BuildConnection();
        }

        #endregion

        #region region_事件

        public event EventHandler datachanged;
        public event EventHandler<EventArgs> DataChanged;

        #endregion

        #region region_连接管理
        
        public int close()
        {
            throw new NotImplementedException();
        }

        public int connect()
        {
            throw new NotImplementedException();
        }

        public int disconnect()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region region_硬件
        /// <summary>
        /// 获取设备列表
        /// </summary>
        public IOdevice[] getDevice()
        {
            int slaveNum = CppConnect.initSlaveConfig();
            deviceNum = slaveNum == 0 ? 0 : slaveNum - 1;

            devices = new IOdevice[deviceNum];
            for (int i = 2; i < slaveNum+1; i++)
            {
                SlaveInfo tmpSlave = new SlaveInfo();

                StringBuilder tmpSlaveName = new StringBuilder();
                tmpSlaveName.Capacity = 128;

                int err = CppConnect.getSlaveInfo(ref tmpSlave,tmpSlaveName, i);
                if (err == SAFECODE)
                {
                    devices[i-2] = new IOdevice();
                    devices[i-2].id         = tmpSlave.id;
                    devices[i-2].channelNum = tmpSlave.channelNum;
                    devices[i-2].name       = tmpSlaveName.ToString();    //renew get_name method
                    devices[i-2].type       = tmpSlave.type;
                }
                else//有错误
                {
                    throw new Exception("设备获取出错");
                }
            }
            return devices;
        }

        #endregion

        #region region_读写
        //数据刷新时触发，目前做成Timer触发，后面应该是Redis更新触发
        public void OnDataRefresh(object sender, EventArgs e)
        {
            //数据刷新
            DataChanged(null, null);
        }

        //TODO:批量读
        public int ReadAnalog(List<int> deviceList, List<int[]> channelList, ref List<int[]> values)
        {
            throw new NotImplementedException();
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

        //TODO:
        public int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<byte[]> values)
        {
            throw new NotImplementedException();
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
                throw new Exception("读取bool值错误，请检查:"+typeof(EtherCAT));
            }
        }

        //TODO: 
        public int WriteAnalog(List<int> deviceList, List<int[]> channelList, List<int[]> values)
        {
            bool succeed = true;
            for (int i=0; i<deviceList.Count; i++)
            {
                int device = deviceList[i];
                //TODO: complete these write
            }
            return succeed ? 0 : -1;
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
        
        //TODO: foreach write
        public int WriteDigital(List<int> deviceList, List<int[]> channelList, List<byte[]> values)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写数字量（单个）
        /// </summary>
        public int WriteDigital(int deviceId, int channel, byte value)
        {
            return CppConnect.setDigitalValue(deviceId, channel, value);
        }
        #endregion

        #region region_网卡
        public Adapter[] getAdapter()
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

        public int setAdapter(int id)
        {
            int errmsg = CppConnect.setAdapterId(id);   //返回-1表示赋值失败
            if (errmsg < 0)
            {
                throw new Exception("Adapter selected failed");
            }
            else                                        //返回0表示没有错误
            {
                deviceNum = errmsg;
                return deviceNum;
            }
        }

        #endregion

        #region other method
        public void SetRefreshParams(int interval)
        {
            this.timer.Interval = interval;
        }

        private void InitTimer(int interval)
        {
            this.timer.Interval = interval;
            timer.Tick += OnDataRefresh;
        }

        public void StartTimer()
        {
            this.timer.Start();
        }

        public void StopTimer()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// 建立连接状态，包括：初始化网卡，选择网卡，初始化从站
        /// </summary>
        /// <returns></returns>
        private void BuildConnection()
        {
            if (isLoadedDriver) return;

            CppConnect.getAdapterNum();
            CppConnect.setAdapterId(this.AdapterSelected);
            this.getDevice();
            StartTimer();

            isLoadedDriver = true;
        }

        public int SetAdapterFromConfig(int AdapterId)
        {
            if (this.AdapterSelected == 0)
            {
                this.AdapterSelected = AdapterId;
                BuildConnection();
                return 2;
            }
            else return 1;
        }
        #endregion
    }
}
