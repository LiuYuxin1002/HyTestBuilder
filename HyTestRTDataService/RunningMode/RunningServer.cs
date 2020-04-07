using System;
using HyTestRTDataService.ConfigMode;
using HyTestRTDataService.ConfigMode.MapEntities;
using System.Data;
using System.Windows.Forms;
using log4net;
using HyTestRTDataService.Interfaces;
using HyTestRTDataService.Context;
using HyTestRTDataService.Forms;
using HyTestRTDataService.Entities;

namespace HyTestRTDataService.RunningMode
{
    public class RunningServer
    {
        private static ILog log = LogManager.GetLogger(typeof(RunningServer));
        private static object locky = new object();
        private static RunningServer server;
        public static RunningServer getServer() //singleton
        {
            if (server == null)
            {
                lock (locky)
                {
                    if (server == null)
                    {
                        server = new RunningServer();
                    }
                }
            }
            return server;
        }
        public bool isConnected = false;

        /*event*/
        public event EventHandler<EventArgs> DataRefresh;
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<EventArgs> DisConnected;

        /*read & write*/
        private Buffer buffer;
        private IReader reader;//有默认值EtherCAT
        private IAutoReader autoReader;
        private IWriter writer;
        private IConnection conn;

        /*config info*/
        private ConfigManager configManager;
        private Config config;
        private ConfigAdapterInfo adapterInfo;
        private ConfigDeviceInfo deviceInfo;
        private ConfigIOmapInfo iomapInfo;
        private ConfigTestEnvInfo testInfo;

        private RunningServer()     //构造函数
        {
            //这里最好不要加任何东西，主要原因是事件绑定会有问题。
        }

        #region Operator

        /// <summary>
        /// 服务开始运行，全局控制
        /// </summary>
        public void Run()
        {
            InitializeConfig();

            InitGlobleContext();

            SetProtocolEntity();

            InitBuffer(this.iomapInfo.InputVarNum, this.iomapInfo.OutputVarNum);

            if (Connected != null)
            {
                Connected(null, null);
            }
        }

        /// <summary>
        /// 服务停止运行，全局控制
        /// </summary>
        public void Stop()
        {
            if (conn != null)
            {
                conn.Disconnect();
                if (DisConnected != null)
                {
                    DisConnected(null, null);
                }
            }
        }

        #endregion

        //Initialize buffer with its size.
        private OperationResult InitBuffer(int inputSize, int outputSize)
        {
            buffer = new Buffer(inputSize, outputSize);     //TODO: 这两个数据来源都有问题，应该用device->index
            return new OperationResult();
        }

        //Get connection, reader, writer, autoReader and so on.
        private OperationResult SetProtocolEntity()
        {
            try
            {
                this.conn = ConfigProtocol.GetConnection();
                this.conn.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //return new OperationResult(1, ex.Message);
            }
            finally
            {
                if (this.conn == null) throw new NullReferenceException("EtherCAT初始化失败");
            }
            /*reader and writer*/
            reader = (IReader)conn;
            writer = (IWriter)conn;
            if (ConnectionContext.isAutoRead)
            {
                autoReader = (IAutoReader)conn;
                
                if (autoReader != null)
                {
                    autoReader.InitAutoReadConfig();
                    autoReader.AutoDataChanged += ReadDataToDatapool;
                }
                else
                {
                    throw new Exception("获取AutoReader失败");
                }
            }

            return new OperationResult();
        }

        //Load config from xml.
        private OperationResult InitializeConfig()
        {
            try
            {
                configManager = new ConfigManager();
            }
            catch (Exception ex)
            {
                //return new OperationResult(1, ex.Message);
            }

            config = configManager.Config;
            adapterInfo = config.AdapterInfo;
            deviceInfo = config.DeviceInfo;
            iomapInfo = config.IomapInfo;
            testInfo = config.TestInfo;

            return new OperationResult();
        }

        private OperationResult InitGlobleContext()
        {
            ConnectionContext.adapterNum = adapterInfo.Adapters.Length;
            //ConnectionContext.adapters = null;
            ConnectionContext.deviceNum = deviceInfo.DeviceNum;
            //ConnectionContext.devices = null;
            //ConnectionContext.inputDeviceNum = 
            ConnectionContext.isAutoRead = true;    //TODO: tmperary setting
            ConnectionContext.needAdapter = true;   //TODO: tmperary setting.
            //ConnectionContext.outputDeviceNum = 
            ConnectionContext.adapterSelectId = adapterInfo.Selected;
            ConnectionContext.adapterId = adapterInfo.Adapters[adapterInfo.Selected].Desc;

            return new OperationResult();
        }

        /*update buffer when redis notifies us*/
        private void ReadDataToDatapool(object sender, DataChangedEventArgs e)
        {
            if (buffer == null) return;
            if (buffer.BufferSizeInput == 0) return;

            /*get params from EventArgs e. When we config slave in soem, the compler-id=1, but 
              in our config system, we count from 0. As for channel, we count from 1~channelnum.*/
            int slave = e.slave - 1;
            int channel = e.channel + 1;
            string key = slave + "-" + channel; //becareful the key format : A-B other than A_B

            int index = -1;
            try
            {
                string name = iomapInfo.MapPortToName[key];
                index = iomapInfo.MapNameToIndex[name];
            }
            catch (Exception ex)
            {
                /*you havent config this port in your iomap*/
                log.Debug(ex.Message +
                    "\n Maybe you havent config this port in your iomap.");
            }

            buffer.Update(index, e.value);

            if (DataRefresh != null) DataRefresh(this, e);     //notify user control.TODO: Needed?
        }

        #region Read
        /// <summary>
        /// 常规读取，读取的是本地数据池
        /// </summary>
        public T NormalRead<T>(string varName)
        {
            Port varPort = null;
            Type varType = null;
            int varMax = default(int);
            int varMin = default(int);
            int index = -1;

            try
            {
                varPort = new Port(iomapInfo.MapNameToPort[varName]);
                varType = Type.GetType(iomapInfo.MapNameToType[varName]);
                varMax = iomapInfo.MapNameToMax[varName];
                varMin = iomapInfo.MapNameToMin[varName];
                index = iomapInfo.MapNameToIndex[varName];
            }
            catch (Exception e)
            {
                log.Debug(e.Message + ":error location:" + varName);
            }
            if (varType == typeof(int))
            {
                int value = (int)buffer.Get(index);
                return (T)Convert.ChangeType(
                    DataTransformer.AnalogToPhysical(value, varMax, varMin),
                    typeof(T));
            }
            else if (varType == typeof(double))
            {
                int value = (int)buffer.Get(index);
                return (T)Convert.ChangeType(
                    DataTransformer.AnalogToPhysical(value, varMax, varMin),
                    typeof(T));
            }
            else
            {
                bool value = DataTransformer.DoubleToBool(buffer.Get(index));
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        /// <summary>
        /// 直接从端口读取
        /// </summary>
        public T InstantRead<T>(string varName)
        {
            Port varPort = null;
            Type varType = null;
            int varMax = default(int);
            int varMin = default(int);

            try
            {
                varPort = new Port(iomapInfo.MapNameToPort[varName]);
                varType = Type.GetType(iomapInfo.MapNameToType[varName]);
                varMax = iomapInfo.MapNameToMax[varName];
                varMin = iomapInfo.MapNameToMin[varName];
            }
            catch (Exception e)
            {
                log.Debug(e.Message + "::" + varName);
            }

            if (varType == typeof(bool))        //if bool
            {
                bool data = reader.ReadBoolean(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (varType == typeof(double)) //if double
            {   
                int data = reader.ReadAnalog(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(DataTransformer.AnalogToPhysical(data, varMax, varMin), typeof(T));
            }
            else if (varType == typeof(int))    //if int, but maybe useless
            {
                int data = reader.ReadAnalog(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(DataTransformer.AnalogToPhysical(data, varMax, varMin), typeof(T));
            }
            else
            {
                new HTAutoCloseForm("变量类型不正确，请检查配置文件").Show();
                return default(T);
            }
        }

        #endregion

        #region Write
        /// <summary>
        /// 直接写到端口
        /// </summary>
        public void InstantWrite<T>(string varName, T value)
        {
            double data = -1;
            Port varPort = null;
            Type varType = null;
            int varMax = default(int);
            int varMin = default(int);

            try
            {
                varPort = new Port(iomapInfo.MapNameToPort[varName]);
                varType = Type.GetType(iomapInfo.MapNameToType[varName]);
                varMax = iomapInfo.MapNameToMax[varName];
                varMin = iomapInfo.MapNameToMin[varName];
            }
            catch (Exception e)
            {
                log.Debug(e.Message + "::" + varName); //varName找不到报错
            }

            if (varType == typeof(bool))        //if digital
            {
                data = writer.WriteBoolean(varPort.deviceId, varPort.channelId,
                        (byte)Convert.ChangeType(value, typeof(byte)));
            }
            else if (varType == typeof(int))    //if int, but maybe useless
            {
                int realValue = DataTransformer.PhysicalToAnalog((double)Convert.ChangeType(value, typeof(int)), 
                                                                  varMax, 
                                                                  varMin);
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
            else if (varType == typeof(double)) //if double
            {
                double tmp = (double)Convert.ChangeType(value, typeof(double));
                if (tmp > varMax || tmp < varMin)
                {
                    MessageBox.Show(string.Format("{0}值不在范围内: {1} ~ {2}", varName, varMin, varMax));
                }
                int realValue = DataTransformer.PhysicalToAnalog(tmp, varMax, varMin);
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
        }

        #endregion

        #region 高速采集任务
        private ReadingTask readTask;
        /// <summary>
        /// 开始高速采集，每产生1000组数据就会回调callback函数
        /// </summary>
        /// <param name="taskId">任务的标识符</param>
        /// <param name="varname"></param>
        /// <param name="period"></param>
        /// <param name="callback">对于1000组数据的处理意见，是显示还是保存</param>
        public void StartReadingTask(int taskId, string varname, int period, HighCallback callback)
        {
            readTask = new ReadingTask(taskId, varname, period, reader, callback, 
                                       new Port(iomapInfo.MapNameToPort[varname]));
            readTask.StartSampling();
        }
        /// <summary>
        /// 停止高速采集
        /// TODO: 多个端口高频采集时，还需实现对taskID的管理
        /// </summary>
        public void StopReadingTask()
        {
            readTask.StopSampling();
        }

        #endregion
    }

}
