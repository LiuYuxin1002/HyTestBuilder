using System;
using System.Linq;
using HyTestRTDataService.ConfigMode;
using HyTestIEInterface;
using System.Threading;
using HyTestRTDataService.ConfigMode.MapEntities;
using System.Data;
using System.Windows.Forms;

namespace HyTestRTDataService.RunningMode
{
    public class RunningServer
    {
        private static object locky = new object();
        private static RunningServer server;
        public static RunningServer getServer() //多线程单例
        {
            if (server == null)
            {
                lock (locky)
                {
                    if (server==null)
                    {
                        server = new RunningServer();
                    }
                }
            }
            return server;
        }
        public bool isConnected;

        //Event下面有使用说明
        public event EventHandler<EventArgs> DataRefresh;
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<EventArgs> DisConnected;

        private Notifier notifier;

        /// <summary>
        /// 控件数据订阅如下：
        /// </summary>
        // RunningServer server = RunningServer.GetServer();   //获取服务
        // 
        // server.DataRefresh += OnDataRefresh;                //委托绑定事件
        // 
        // private void OnDataRefresh(object sender, EventArgs e)  //DataRefresh函数触发调用事件
        // {
        //     FetchDataAndShow();      //自己写函数，向server获取数据并令控件显示获取到的数据
        
        // }
        //数字量Light、模拟量meter, LED，趋势图time-value
        //void FetchDataAndShow()
        //{
        //    int data = server.NormalRead<int>(varname);
        //    this.Value = data;
        //}

        /*read & write*/
        IReader reader;//有默认值EtherCAT
        IWriter writer;
        IConnection conn;
        IAdapterLoader adapterLoader;

        /*data pool*/
        private RealTimeDataPool datapool;

        /*config info*/
        private ConfigManager       configManager;
        private Config              config;
        private ConfigAdapterInfo   adapterInfo;
        private ConfigDeviceInfo    deviceInfo;
        private ConfigIOmapInfo     iomapInfo;
        private ConfigTestEnvInfo   testInfo;

        private int refreshFrequency;

        private RunningServer()     //构造函数
        {
            notifier = Notifier.notifier;
            notifier.ProgramRunning += OnProgramRunning;
        }

        private void OnProgramRunning(object sender, EventArgs e)
        {
            InitializeDataPool();
            InitializeConfig();
            reader.DataChanged += ReadDataToDatapool;
            Connected(null, null);
        }

        //Useless.
        private void InitializeDataPool()
        {
            datapool = new RealTimeDataPool();
        }

        //Load config from xml.
        private void InitializeConfig()
        {
            configManager = new ConfigManager();
            configManager.LoadConfig();

            config = ConfigManager.config;
            adapterInfo = config.adapterInfo;
            deviceInfo = config.deviceInfo;
            iomapInfo = config.iomapInfo;
            testInfo = config.testInfo;

            conn = ConfigProtocol.GetConnection();
            conn.Connect(adapterInfo.currentAdapterId);

            reader = (IReader)conn;
            writer = (IWriter)conn;
        }

        private void ReadDataToDatapool(object sender, EventArgs e)
        {
            //if (datapool.rdataList == null) return;
            //for (int i = 0; i < datapool.rdataList.Count(); i++)
            //{
            //    datapool.rdataList[i] = ReadDataFromDevice(i);
            //}
            if(DataRefresh != null) DataRefresh(this, e);     //通知各控件
        }

        /// <summary>
        /// 从底层读取数值，返回以更新datapool中的数据
        /// </summary>
        /// <param name="index">datapool中的序号</param>
        /// <returns></returns>
        private double ReadDataFromDevice(int index)
        {
            string varName = iomapInfo.mapIndexToName[index];
            Port varPort = new Port(iomapInfo.mapNameToPort[varName]);
            Type varType = Type.GetType(iomapInfo.mapNameToType[varName]);

            if (varType == typeof(bool))
            {
                bool value = InstantRead<bool>(varName);
                return value ? 1 : 0;
            }
            else if (varType == typeof(int))
            {
                return InstantRead<int>(varName);
            }
            else if (varType == typeof(double))
            {
                return InstantRead<double>(varName);
            }
            else
            {
                MessageBox.Show("变量类型不正确，请检查配置文件");
                return default(double);
            }
        }

        /// <summary>
        /// 常规读取，读取的是本地数据池
        /// </summary>
        public T NormalRead<T>(string varName)
        {
            Type varType = Type.GetType(iomapInfo.mapNameToType[varName]);
            int varIndex = iomapInfo.mapNameToIndex[varName];
            if (varType == typeof(int))
            {
                int value1 = DataTransformer.DoubleToInt(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
            else if (varType == typeof(bool))
            {
                double value1 = DataTransformer.DoubleToDouble(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
            else
            {
                bool value1 = DataTransformer.DoubleToBool(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
        }

        /// <summary>
        /// 常规写入，写入的是本地数据池
        /// </summary>
        public void NormalWrite<T>(string varName, T value)
        {
            Type varType = Type.GetType(iomapInfo.mapNameToType[varName]);
            int varIndex = iomapInfo.mapNameToIndex[varName];
            if (varType == typeof(int))
            {
                int value1 = (int)Convert.ChangeType(value, typeof(int));
                datapool.rdataList[varIndex] = value1;
            }
            else if (varType == typeof(bool))
            {
                double value1 = (double)Convert.ChangeType(value, typeof(double));
                datapool.rdataList[varIndex] = value1;
            }
            else
            {
                bool value1 = (bool)Convert.ChangeType(value, typeof(bool));
                datapool.rdataList[varIndex] = value1 ? 1 : 0;
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
                varPort = new Port(iomapInfo.mapNameToPort[varName]);
                varType = Type.GetType(iomapInfo.mapNameToType[varName]);
                varMax = iomapInfo.mapNameToMax[varName];
                varMin = iomapInfo.mapNameToMin[varName];
            }
            catch(Exception e)
            {
                MessageBox.Show(varName + "\n" + e.Message);
            }

            if (varType == typeof(bool))        //if digital
            {
                bool data = reader.ReadDigital(varPort.deviceId, varPort.channelId);
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
                MessageBox.Show("变量类型不正确，请检查配置文件");
                return default(T);
            }
        }

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
                varPort = new Port(iomapInfo.mapNameToPort[varName]);
                varType = Type.GetType(iomapInfo.mapNameToType[varName]);
                varMax = iomapInfo.mapNameToMax[varName];
                varMin = iomapInfo.mapNameToMin[varName];
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message); //varName找不到报错
            }

            if (varType == typeof(bool))        //if digital
            {
                data = writer.WriteDigital(varPort.deviceId, varPort.channelId, 
                        (byte)Convert.ChangeType(value, typeof(byte)));
            }
            else if (varType == typeof(int))    //if int, but maybe useless
            {
                int realValue = DataTransformer.PhysicalToAnalog((double)Convert.ChangeType(value, typeof(int)), varMax, varMin);
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
            else if (varType == typeof(double)) //if double
            {
                double tmp = (double)Convert.ChangeType(value, typeof(double));
                if(tmp > varMax || tmp < varMin)
                {
                    MessageBox.Show(string.Format("该值不在范围内: %d ~ %d", varMin, varMax));
                }
                int realValue = DataTransformer.PhysicalToAnalog(tmp, varMax, varMin);
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
        }

        /// <summary>
        /// 设置订阅列表
        /// </summary>
        /// <param name="nameList">订阅变量名数组</param>
        /// <returns>成功</returns>
        public bool SetDataPack(ReadingTask task)
        {
            return default(bool);
        }

        /// <summary>
        /// 获取订阅列表
        /// </summary>
        /// <returns>订阅变量名数组</returns>
        public string[] GetDataPack()
        {
            return null;
        }

        /// <summary>
        /// 清空变量列表
        /// </summary>
        /// <returns>将Data Package 初始化</returns>
        public string[] ClearDataPack()
        {
            return null;
        }

        /// <summary>
        /// 添加订阅变量，添加失败返回-1
        /// </summary>
        /// <param name="varName">待添加变量名</param>
        /// <returns>变量在列表中的ID（index）</returns>
        public int AddDataPackMember(string varName)
        {
            //throw new Exception("变量名已存在");
            return default(int);
        }

        /// <summary>
        /// 移除订阅变量
        /// </summary>
        /// <param name="varName">待移除变量名</param>
        /// <returns>成功</returns>
        public bool RemoveDataPackMember(string varName)
        {
            return default(bool);
        }

        /// <summary>
        /// 获取本次试验数据
        /// </summary>
        /// <remarks>
        /// 查阅MSDN，DataTable最大存储行数为16,777,216，足够需求。如果经计算不够尽早反馈。
        /// </remarks>
        /// <returns>返回DataTable结构为Id, Name, Type 和 Value。
        /// 其中Value是一个Dictionary，key代表时间(hhmmss)，value代表值，统一为double类型</returns>
        public DataTable DataPackCallback()
        {
            return null;
        }//C# dictionary java map C++ map
    }

}
