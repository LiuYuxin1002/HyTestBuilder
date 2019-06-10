using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private double[] rdataList;
        private double[] wdataList;

        IReader reader;
        IWriter writer; 

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
            for(int i=0; i<rdataList.Count(); i++)
            {
                rdataList[i] = ReadData(i);
            }
        }

        private double ReadData(int index)
        {
            int data = 0;
            string varName = config.mapIndexToName[index];
            Port varPort = config.mapNameToPort[varName];
            reader.ReadAnalog(varPort.deviceId, varPort.channelId, ref data);
            return (double)data;
        }

        public void LoadDataTable()
        {

        }
    }
}
