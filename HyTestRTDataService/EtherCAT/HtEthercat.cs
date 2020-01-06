using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using HyTestRTDataService.Interfaces;
using HyTestRTDataService.Entities;
using HyTestRTDataService.Context;
using System.Threading;

namespace HyTestRTDataService.EtherCAT
{
    public class HtEthercat : IConnection, IAdapterLoader, IDeviceLoader, IReader, IWriter, IAutoReader
    {
        #region region_Properties
        
        public int AdapterSelected { get; set; }
        public static HtEthercat getEtherCAT()
        {
            if (ethercat == null) ethercat = new HtEthercat();
            return ethercat;
        }
        #endregion

        #region region_Member
        /*log4net.dll Logger*/
        ILog log = LogManager.GetLogger(typeof(HtEthercat));
        /*Singleton*/
        private static HtEthercat ethercat;
        /*Flag of driver load.*/
        /* It will be set TRUE while call BuildConnection(), 
         * and will be varified in this method too.*/
        private bool isLoadedDriver = false;    

        #endregion

        #region region_Constructor

        private HtEthercat()
        {
            InitConnectionContext();
            InitAutoReadConfig();
        }

        #endregion

        #region region_IConnection
        //TODO: 需要么？
        public OperationResult Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 主要作用是激发RunningServer与硬件的连接，让硬件处于OP状态。
        /// </summary>
        /// <param name="ID">所选网卡</param>
        /// <returns>应该返回操作结果</returns>
        public OperationResult Connect()
        {
            BuildConnection();
            StartAutoRead();
            return new OperationResult();
        }

        /// <summary>
        /// 主要作用是停止硬件运行，对于EtherCAT来说是恢复Init状态。
        /// </summary>
        /// <returns>应该返回操作结果</returns>
        public OperationResult Disconnect()
        {
            HtEcConnector.stopRunning();
            return new OperationResult();
        }

        // 建立连接状态，包括：初始化网卡，选择网卡，初始化从站
        private void BuildConnection()
        {
            if (isLoadedDriver) return;

            try
            {
                SetAdapter(ConnectionContext.adapterId);

                //this.GetDevice();
                ConnectionContext.deviceNum = HtEcConnector.initSlaveConfig();

                isLoadedDriver = true;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message + "\n" + ex.StackTrace);
            }
        }

        #endregion

        #region region_IDeviceLoader
        public int InitDevice()
        {
            return HtEcConnector.initSlaveConfig();
        }
        /// <summary>
        /// 获取设备列表
        /// </summary>
        public List<List<IOdevice>> GetDevice()
        {
            BuildConnection();
            int slaveNum = ConnectionContext.deviceNum;

            List<List<IOdevice>> deviceContiner = new List<List<IOdevice>>();   //全部device
            List<IOdevice> devGroup = null;     //一个device组
            IOdevice tmpDevice = null;

            for (int i = 1; i < slaveNum + 2; i++)
            {
                HtEcEntity tmpSlave = new HtEcEntity();
                StringBuilder tmpSlaveName = new StringBuilder();
                tmpSlaveName.Capacity = 128;

                int err = HtEcConnector.getSlaveInfo(tmpSlaveName, ref tmpSlave, i);

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
            return ConnectionContext.deviceNum;
        }

        #endregion

        #region region_IReader
        /* 这里的两个函数表示我们的原生读函数，由于功能需要我们依旧保留这个方法，但是在更新数据缓冲池、
         * 控件显示等方面我们还是采用redis读取的方式。
         * Redis读取的好处也是明显的，无需依赖C#中极其不准确的Timer来触发读取，可以保证缓冲池中数据时
         * 钟保持最新；不足之处在于，由于控制逻辑依旧是在C#中实现，一些周期循环控制可能还是不尽如人意。
         * 值得一提的是，我们在编写控制逻辑的时候，可以绑定OnDataRefresh等事件，事件触发之时我们再执行
         * 对应的控制算法。
         */

        public int ReadAnalog(int deviceId, int channel)
        {
            int tmpValue = HtEcConnector.getAnalogValue(deviceId, channel);
            return tmpValue;
        }

        public bool ReadBoolean(int deviceId, int channel)
        {
            int tmpValue = HtEcConnector.getDigitalValue(deviceId, channel);
            if (tmpValue == 0) return false;
            else if (tmpValue == 1) return true;
            else
            {
                //throw new Exception("读取bool值错误，请检查:"+typeof(EtherCAT));
                return false;
            }
        }

        IList<HighFreqReadCallback> highCallbacks = new List<HighFreqReadCallback>();
        public void HighFreqRead(int deviceId, int channel, int freq, HighCallback call)
        {
            HighFreqReadCallback callback = (ans) =>
            {
                call(ans);
            };
            highCallbacks.Add(callback);
            HtEcConnector.HighFreqRead(deviceId, channel, freq, callback);
        }

        public void HighFreqReadStop(int deviceId, int channelId)
        {
            HtEcConnector.HighFreqReadStop(deviceId, channelId);
            Thread.Sleep(1000); //等待回传最后一组数据
            //highCallbacks.Remove(XXX);highcallbacks应该作一个dictionary，以便通过键值获取并释放委托内存
        }
        #endregion

        #region region_IAutoReader
        public event EventHandler<DataChangedEventArgs> AutoDataChanged;
        public ProcessCallback callback;

        public void InitAutoReadConfig()
        {
            callback = (slave, channel, value) =>
            {
                AutoDataChanged(null, new DataChangedEventArgs(slave, channel, value));
            };
            //binding callback.
            HtEcConnector.doWork(callback);
        }

        public void StartAutoRead()
        {
            HtEcConnector.readStart();
        }
        #endregion

        #region region_IWriter
        public int WriteAnalog(int deviceId, int channel, int value)
        {
            return HtEcConnector.setAnalogValue(deviceId, channel, value);
        }

        public int WriteBoolean(int deviceId, int channel, byte value)
        {
            return HtEcConnector.setDigitalValue(deviceId, channel, value);
        }
        
        #endregion

        #region region_IAdapterLoader

        /// <summary>
        /// 获取本机网卡列表，调用底层C++函数。
        /// 由于无法实现传递string数组，因此基本原理是首先获取网络适配器数量adapterNum；
        /// 然后获取每一个adapter的Name，用StringBuilder来传递。
        /// </summary>
        public Adapter[] GetAdapter()
        {
            int adapterNum = HtEcConnector.getAdapterNum();    //Get adapter number first.
            Adapter[] adapters = new Adapter[adapterNum];

            StringBuilder tmpAdapterName = new StringBuilder();
            StringBuilder tmpAdapterDesc = new StringBuilder();
            tmpAdapterName.Capacity = 128;
            tmpAdapterDesc.Capacity = 128;
            for (int i = 0; i < adapterNum; i++)    //Get adapter name one-by-one.
            {
                int err = HtEcConnector.getAdapterName(tmpAdapterName, tmpAdapterDesc, i);
                if (err <= 0)//有错误
                {
                    throw new Exception(); //一般来讲是这个错误
                }
                adapters[i] = new Adapter();
                adapters[i].Name = tmpAdapterName.ToString();
                adapters[i].Desc = tmpAdapterDesc.ToString();
                tmpAdapterName.Clear();
                tmpAdapterDesc.Clear();
            }
            ConnectionContext.adapters = adapters;
            return adapters;
        }

        /// <summary>
        /// 选择网卡，调用底层C++函数，这一步会导致其中变量ifbuf赋值为网卡名。
        /// </summary>
        /// <param name="id">此ID与网卡扫描顺序有关</param>
        /// <returns>成功返回100，失败返回-1</returns>
        public OperationResult SetAdapter(string id)
        {
            StringBuilder sb = new StringBuilder("\\Device\\NPF_" + id);
            HtEcConnector.SetAdapter(sb);   //返回-1表示赋值失败
            return new OperationResult();
        }

        #endregion

        #region ConnectionContext
        //TODO: fill it
        public void InitConnectionContext()
        {

        }


        #endregion

    }
}
