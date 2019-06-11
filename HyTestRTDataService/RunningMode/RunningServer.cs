﻿using System;
using System.Linq;
using HyTestRTDataService.ConfigMode;
using HyTestIEInterface;
using System.Threading;
using HyTestRTDataService.ConfigMode.MapEntities;

namespace HyTestRTDataService.RunningMode
{
    class RunningServer
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

        IReader reader;
        IWriter writer;
        private RealTimeDataPool datapool = new RealTimeDataPool();

        private System.Windows.Forms.Timer timer;
        private int refreshFrequency;
        private Config config;
        private bool subscribeStart = true;

        private RunningServer()     //构造函数
        {
            InitializeConfig();
            InitializeTimer();
            StartTimer();
        }

        private void StartTimer()
        {
            this.timer.Start();
        }

        private void InitializeConfig()
        {
            config = Config.getConfig();
            config.LoadXmlConfig();
            reader = ConfigProtocol.getReader();
            writer = ConfigProtocol.getWriter();
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = config.refreshFrequency;   //可能有未实例化对象错误
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
            for(int i=0; i<datapool.rdataList.Count(); i++)
            {
                datapool.rdataList[i] = ReadDataFromDevice(i);
            }
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
            string varName = config.mapIndexToName[index];
            Port varPort = config.mapNameToPort[varName];
            Type varType = config.mapNameToType[varName];

            if (varType == typeof(bool))
            {
                bool value = false;
                reader.ReadDigital(varPort.deviceId, varPort.channelId, ref value);
                transformer = new DataTransformer();
                data = transformer.TransDigitalToBoolDouble(value);
            }
            else if (varType == typeof(int))
            {
                int value = -1;
                reader.ReadAnalog(varPort.deviceId, varPort.channelId, ref value);
                transformer = new DataTransformer();
                data = transformer.TransAnalogToIntDouble(value);
            }
            else if (varType == typeof(double))
            {
                int value = -1;
                reader.ReadAnalog(varPort.deviceId, varPort.channelId, ref value);
                transformer = new DataTransformer();
                data = transformer.TransAnalogToIntDouble(value);
            }
            
            return (double)data;
        }

        //实例化过程中，先把XML读到config
        public void LoadDataTable()
        {

        }

        public T NormalRead<T>(string varName)
        {
            T value;
            Type varType = config.mapNameToType[varName];
            //Port varPort = config.mapNameToPort[varName];
            int varIndex = config.mapNameToIndex[varName];
            if (varType == typeof(int))
            {
                int value1 = DataTransformer.TransformingInt(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
            else if(varType == typeof(bool))
            {
                double value1 = DataTransformer.TransformingDouble(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
            else
            {
                bool value1 = DataTransformer.TransformingBool(datapool.rdataList[varIndex]);
                return (T)Convert.ChangeType(value1, typeof(T));
            }
            return default(T);
        }

        public void NormalWrite<T>(string varName, T value)
        {
            Type varType = config.mapNameToType[varName];
            int varIndex = config.mapNameToIndex[varName];
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

        //直接从端口读取
        public T InstantRead<T>(string varName)
        {

            return default(T);
        }

        //直接写到端口
        public void InstantWrite<T>(string varName, T value)
        {

        }
    }
}
