using log4net;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HyTestRTDataService.RunningMode;
using System.Text;

namespace Test
{
    public partial class TestForm : Form
    {
        ILog log = log4net.LogManager.GetLogger(typeof(TestForm));

        private ProcessCallback callback;

        RunningServer server = RunningServer.getServer();

        public TestForm()
        {
            InitializeComponent();
            Prepare();
        }

        private void Prepare()
        {
            callback = (slave, channel, value) =>
            {
                log.Info(String.Format("当前回调：slave={0},channel={1},value={2}.", slave, channel, value));
            };

            Connector.SetAdapter(new StringBuilder("\\Device\\NPF_{987548EA-D20A-4222-897B-A7D44F172A93}"));
            Connector.initSlaveConfig();
            Connector.doWork(callback);
            //Connector.EndWork();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connector.readStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connector.readStop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connector.setDigitalValue(3, 0, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Connector.setDigitalValue(3, 1, 1);
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void ProcessCallback(int slave, int channel, int value);

    public class Connector
    {
        [DllImport("TestC.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DoWork([MarshalAs(UnmanagedType.FunctionPtr)] ProcessCallback callbackPointer);
        [DllImport("TestC.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EndWork();

        /*HyTestEthercatDriver使用说明：
         步骤：
         1.SetAdapterName(string name);     //设置网卡名称
         2.initSlaveConfig();               //初始化从站到OP状态
         3.doWork(ProcessCallback callback);//设置读取回调函数
         4.readStart();                     //开始读取
             */
        //获取/设置计算机网卡信息，接收到Context结构体
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int getAdapterNum();
        [DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAdapterName(StringBuilder namestr, StringBuilder descstr, int id);
        //设置所选网卡，如果失败返回-1，成功返回100
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setAdapterId", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAdapterId(int nicId);
        //自动配置从站，成功更新结构体数组
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int initSlaveConfig();
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setAdapter", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAdapter(StringBuilder name);
        //[DllImport("HyTestEthercatDriver.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int getSlaveInfo(StringBuilder slaveName, ref EtherCATEntity slaveInfo, int id);

        /*Set slave running state to init. If you need to running again, please call initSlaveConfig() and getSlaveInfo() again.*/
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern void stopRunning();
        /*Write*/
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setAnalogValue(int slaveId, int channel, int value);
        [DllImport("HyTestEthercatDriver.dll", EntryPoint = "setDigitalValue", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setDigitalValue(int slaveId, int channel, byte value);
        /*Read*/
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int doWork([MarshalAs(UnmanagedType.FunctionPtr)]ProcessCallback callbackPoint);
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getAnalogValue(int slaveId, int channelId);
        [DllImport("HyTestEthercatDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDigitalValue(int slaveId, int channelId);
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStart();
        [DllImport("HyTestEthercatDriver.dll")]
        public static extern int readStop();
    }
}
