using HyTestRTDataService.RunningMode;
using log4net;
using System;
using System.Windows.Forms;

namespace HyTestRTDataService.TEST
{
    public partial class Form1 : Form
    {
        public ILog log = LogManager.GetLogger(typeof(Form1));
        public RunningServer server = RunningServer.getServer();
        public Form1()
        {
            InitializeComponent();
            server.Run();
            for (int i = 0; i < 8; i++) do_state[i] = true;
        }

        bool[] do_state = new bool[8];
        //DO1
        private void HtLed16_Load(object sender, EventArgs e)
        {
            log.Info("do1 click...");
            do_state[0] = do_state[0] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[0]);
        }

        //DO2
        private void HtLed15_Load(object sender, EventArgs e)
        {
            do_state[1] = do_state[1] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[1]);
        }

        //DO3
        private void HtLed14_Load(object sender, EventArgs e)
        {
            do_state[2] = do_state[2] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[2]);
        }

        //DO4
        private void HtLed13_Load(object sender, EventArgs e)
        {
            do_state[3] = do_state[3] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[3]);
        }

        //DO5
        private void HtLed12_Load(object sender, EventArgs e)
        {
            do_state[4] = do_state[4] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[4]);
        }

        //DO6
        private void HtLed11_Load(object sender, EventArgs e)
        {
            do_state[5] = do_state[5] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[5]);
        }

        //DO7
        private void HtLed10_Load(object sender, EventArgs e)
        {
            do_state[6] = do_state[6] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[6]);
        }

        //DO8
        private void HtLed9_Load(object sender, EventArgs e)
        {
            do_state[7] = do_state[7] ? false : true;
            server.InstantWrite<bool>("DO1", do_state[7]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            log.Info("log测试 ...");
        }
    }
}
