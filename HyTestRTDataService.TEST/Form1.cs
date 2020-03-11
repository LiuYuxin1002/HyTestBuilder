using HyTestRTDataService.Interfaces;
using HyTestRTDataService.RunningMode;
using log4net;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HyTestRTDataService.TEST
{
    public partial class Form1 : Form
    {
        public ILog log = LogManager.GetLogger(typeof(Form1));
        public RunningServer server = RunningServer.getServer();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
            server.Run();
            log.Info("LALLAA");
            for (int i = 0; i < 8; i++) do_state[i] = false;

            htUserCurve2.SetCurve("AI1", true, null, System.Drawing.Color.Blue, 0.5f);
            htUserCurve2.SetCurve("AI2", true, null, System.Drawing.Color.Red, 0.5f);
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            server.DataRefresh += Server_DataRefresh;
        }

        private void Server_DataRefresh(object sender, EventArgs e)
        {
            double value = server.NormalRead<double>("AI1");
            htUserCurve2.AddCurveData("AI1", (float)value);
            server.InstantWrite<double>("AO1", value);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            htUserCurve2.AddCurveData("AI1", (float)server.NormalRead<double>("AI1"));
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
            server.InstantWrite<bool>("DO2", do_state[1]);
        }

        //DO3
        private void HtLed14_Load(object sender, EventArgs e)
        {
            do_state[2] = do_state[2] ? false : true;
            server.InstantWrite<bool>("DO3", do_state[2]);
        }

        //DO4
        private void HtLed13_Load(object sender, EventArgs e)
        {
            do_state[3] = do_state[3] ? false : true;
            server.InstantWrite<bool>("DO4", do_state[3]);
        }

        //DO5
        private void HtLed12_Load(object sender, EventArgs e)
        {
            do_state[4] = do_state[4] ? false : true;
            server.InstantWrite<bool>("DO5", do_state[4]);
        }

        //DO6
        private void HtLed11_Load(object sender, EventArgs e)
        {
            do_state[5] = do_state[5] ? false : true;
            server.InstantWrite<bool>("DO6", do_state[5]);
        }

        //DO7
        private void HtLed10_Load(object sender, EventArgs e)
        {
            do_state[6] = do_state[6] ? false : true;
            server.InstantWrite<bool>("DO7", do_state[6]);
        }

        //DO8
        private void HtLed9_Load(object sender, EventArgs e)
        {
            do_state[7] = do_state[7] ? false : true;
            server.InstantWrite<bool>("DO8", do_state[7]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }

        

        private void btn_ao1_Click(object sender, EventArgs e)
        {
            string value = textBox1.Text.Trim();
            
            try
            {
                server.InstantWrite<double>("AO1", double.Parse(value));
            }
            catch { throw; }
        }

        private void btn_ao2_Click(object sender, EventArgs e)
        {
            string value = textBox2.Text.Trim();
            try
            {
                server.InstantWrite<double>("AO2", double.Parse(value));
            }
            catch { throw; }
        }

        private void btn_ao3_Click(object sender, EventArgs e)
        {
            string value = textBox3.Text.Trim();
            try
            {
                server.InstantWrite<double>("AO3", double.Parse(value));
            }
            catch { throw; }
        }

        private void btn_ao4_Click(object sender, EventArgs e)
        {
            string value = textBox4.Text.Trim();
            try
            {
                server.InstantWrite<double>("AO4", double.Parse(value));
            }
            catch { throw; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            log.Info("log测试 ...");
            ExportData();
            log.Info("测试结束...");
        }

        private void ExportData()
        {
            //定义变量
            string analogFile = "..\\..\\bin\\analogWriteTest.txt";
            string digitalFile = "..\\..\\bin\\digitalWriteTest.txt";
            double[] intervals_a = new double[1000];
            double[] intervals_b = new double[1000];
            double ave_a = 0;
            double ave_b = 0;
            MyTimer timer = new MyTimer();
            
            //开始测试
            server.InstantWrite<double>("AO1", 1.2);
            log.Info("测试开始...");

            log.Info("测试Bool值写...");
            for (int i = 0; i < 1000; i++)
            {
                timer.GetElapsedTime();
                server.InstantWrite<bool>("DO1", true);
                server.InstantWrite<bool>("DO2", true);
                server.InstantWrite<bool>("DO3", true);
                server.InstantWrite<bool>("DO4", true);
                server.InstantWrite<bool>("DO5", true);
                server.InstantWrite<bool>("DO6", true);
                server.InstantWrite<bool>("DO7", true);
                server.InstantWrite<bool>("DO8", true);
                intervals_b[i] = timer.GetElapsedTime() / 0.008;  //每次写入平均时间ms
                ave_b += intervals_b[i];
                htUserCurve2.AddCurveData("AI1", (float)(intervals_b[i] * 1000));   //时间转换为us
            }
            log.Info("Bool测试结果：" + ave_b + "us");
            log.Info("测试Double值写");
            for (int i = 0; i < 1000; i++)
            {
                timer.GetElapsedTime();
                server.InstantWrite<double>("AO1", 1);
                server.InstantWrite<double>("AO2", 2);
                server.InstantWrite<double>("AO3", 3);
                server.InstantWrite<double>("AO4", 4);
                intervals_a[i] = timer.GetElapsedTime() / 0.004;
                ave_a += intervals_a[i];
                htUserCurve2.AddCurveData("AI2", (float)(intervals_a[i]));
            }
            log.Info("Double测试结果：" + ave_a + "us");

            log.Info("写入文件中...");
            //保存测试结果
            using (FileStream fs1 = new FileStream(analogFile, FileMode.OpenOrCreate),
                              fs2 = new FileStream(digitalFile, FileMode.OpenOrCreate)
            )
            {
                //analog
                StreamWriter sw = new StreamWriter(fs1, Encoding.ASCII);
                StringBuilder sb = new StringBuilder();
                foreach(var k in intervals_a)
                {
                    sb.Append(k + "\n");
                }
                sw.Write(sb.ToString());
                //sw.Close();
                //digital
                sw = new StreamWriter(fs2, Encoding.ASCII);
                sb.Clear();
                foreach(var k in intervals_b)
                {
                    sb.Append(k + "\n");
                }
                sw.Write(sb.ToString());
                sw.Close();
            }
            log.Info("写入完成...");
        }
        HighCallback callback;
        private void button2_Click(object sender, EventArgs e)
        {
            callback = (data) =>
            {
                //这里如果返回的是float可以直接调用htUserCurve1.AddCurveData("AI1", data);
                foreach (var val in data)
                {
                    Thread.Sleep(2);
                }
                log.Info("OK");
            };
            server.StartReadingTask(1, "AI1", 10, callback);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            server.StopReadingTask();
        }
    }
}
