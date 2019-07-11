using System;
using System.Linq;
using HyTestRTDataService.ConfigMode;
using HyTestIEInterface;
using System.Threading;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestRTDataService.ConfigMode.Component;
using System.Collections.Generic;
using System.Data;
using HyTestEtherCAT;

namespace HyTestRTDataService.RunningMode
{
    public class TmpClass
    {
        RunningServer server = RunningServer.getServer();

        void RunTest()
        {
            server.NormalWrite<int>("var1", 1);
            server.NormalWrite<double>("var2", 2);

            server.NormalRead<int>("var3");

            //注意：write的必须是输出；read的可以是输入和输出；

            //变量的订阅
            server.SetDataPack(new string[] { "var1", "var2" });
            server.GetDataPack();
            server.AddDataPackMember("var3");
            server.DataPackCallback();

        }

        
    }
    public class RunningServer
    {
        private static RunningServer server;
        public static RunningServer getServer()
        {
            if (server == null)
            {
                server = new RunningServer();
            }
            return server;
        }

        /*Event*/
        public event EventHandler<EventArgs> DataRefresh;
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

        /*data pool*/
        private RealTimeDataPool datapool;

        /*config info*/
        private ConfigManager       configManager;
        private Config              config;
        private ConfigAdapterInfo   adapterInfo;
        private ConfigDeviceInfo    deviceInfo;
        private ConfigIOmapInfo     iomapInfo;
        private ConfigTestEnvInfo   testInfo;

        //private System.Windows.Forms.Timer timer;
        private int refreshFrequency;
        private bool subscribeStart = true;

        private RunningServer()     //构造函数
        {
            InitializeDataPool();
            InitializeConfig();
            
        }

        private void InitializeDataPool()
        {
            datapool = new RealTimeDataPool();
        }

        private void InitializeConfig()
        {
            configManager = new ConfigManager();
            configManager.LoadConfig();

            config = ConfigManager.config;
            adapterInfo = config.adapterInfo;
            deviceInfo = config.deviceInfo;
            iomapInfo = config.iomapInfo;
            testInfo = config.testInfo;

            reader = ConfigProtocol.getReader();
            writer = ConfigProtocol.getWriter();

            writer.SetAdapterFromConfig(config.adapterInfo.currentAdapterId);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (subscribeStart)
            {
                Thread rthread = new Thread(ReadDataToDatapool);
                rthread.Start();
            }
        }

        private void ReadDataToDatapool()
        {
            if (datapool.rdataList == null) return;

            for (int i = 0; i < datapool.rdataList.Count(); i++)
            {
                datapool.rdataList[i] = ReadDataFromDevice(i);
            }
            DataRefresh(this, new EventArgs());     //通知各控件
        }

        DataTransformer transformer;

        /// <summary>
        /// 从底层读取数值，返回以更新datapool中的数据
        /// </summary>
        /// <param name="index">datapool中的序号</param>
        /// <returns></returns>
        private double ReadDataFromDevice(int index)
        {
            double data = -1;
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
            
            else return default(double);
        }

        /// <summary>
        /// 常规读取，读取的是本地数据池
        /// </summary>
        public T NormalRead<T>(string varName)
        {
            T value;
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
            return default(T);
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
            //int data = -1;
            Port varPort = new Port(iomapInfo.mapNameToPort[varName]);
            Type varType = Type.GetType(iomapInfo.mapNameToType[varName]);

            if (varType == typeof(bool))    //if digital
            {
                bool data = reader.ReadDigital(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(data, typeof(T));
            }
            else if (varType == typeof(int))    //if int, but maybe useless
            {
                int data = reader.ReadAnalog(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(DataTransformer.InputAnalogToPhysical(data), typeof(T));
            }
            else if (varType == typeof(double)) //if double
            {
                int data = reader.ReadAnalog(varPort.deviceId, varPort.channelId);
                return (T)Convert.ChangeType(DataTransformer.InputAnalogToPhysical(data), typeof(T));
            }
            else throw new Exception();
        }

        /// <summary>
        /// 直接写到端口
        /// </summary>
        public void InstantWrite<T>(string varName, T value)
        {
            double data = -1;
            Port varPort = new Port(iomapInfo.mapNameToPort[varName]);
            Type varType = Type.GetType(iomapInfo.mapNameToType[varName]);

            if (varType == typeof(bool))    //if digital
            {
                data = writer.WriteDigital(varPort.deviceId, varPort.channelId, 
                        (byte)Convert.ChangeType(value, typeof(byte)));
            }
            else if (varType == typeof(int))    //if int, but maybe useless
            {
                int realValue = DataTransformer.OutputPhysicalToAnalog((int)Convert.ChangeType(value, typeof(int)));
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
            else if (varType == typeof(double)) //if double
            {
                int realValue = DataTransformer.OutputPhysicalToAnalog((double)Convert.ChangeType(value, typeof(double)));
                data = writer.WriteAnalog(varPort.deviceId, varPort.channelId, realValue);
            }
        }

        /// <summary>
        /// 设置订阅列表
        /// </summary>
        /// <param name="nameList">订阅变量名数组</param>
        /// <returns>成功</returns>
        public bool SetDataPack(string[] nameList)
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
