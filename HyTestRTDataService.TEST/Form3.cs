using HyTestRTDataService.Interfaces;
using HyTestRTDataService.RunningMode;
using System;
using System.Threading;
using System.Windows.Forms;

namespace HyTestRTDataService.TEST
{
    public partial class Form3 : Form
    {
        public RunningServer server = RunningServer.getServer();
         
        public Form3()
        {
            InitializeComponent();
            server.Run();
            htUserCurve1.SetCurve("Displacement", true, null, System.Drawing.Color.Blue, 0.5f);
            htUserCurve2.SetCurve("DIS", true, null, System.Drawing.Color.Red, 0.5f);
            server.DataRefresh += Server_DataRefresh;
        }

        private void Server_DataRefresh(object sender, System.EventArgs e)
        {
            double val = server.NormalRead<double>("D_in");
            htUserCurve2.AddCurveData("DIS", (float)val);
        }

        private void btn_ao1_Click(object sender, System.EventArgs e)
        {
            server.InstantWrite<double>("D_out", double.Parse(textBox1.Text.Trim()));
            //普通读取
            double val = server.InstantRead<double>("AI2");
            //普通写
            server.InstantWrite<double>("AO3", 2.0);
            //订阅

        }

        /// <summary>
        /// 开始采集
        /// </summary>
        HighCallback callback;
        private void button1_Click(object sender, System.EventArgs e)
        {
            //double value = server.InstantRead<double>("D_in");
            callback = (data) =>
            {
                htUserCurve1.AddCurveData("Displacement", Array.ConvertAll(data, s=>(double)s));
                Thread.Sleep(2);
            };
            server.StartReadingTask(1, "D_in", 2, callback);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {

        }
    }
}
