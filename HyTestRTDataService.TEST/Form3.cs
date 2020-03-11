using HyTestRTDataService.Interfaces;
using HyTestRTDataService.RunningMode;
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
        }

        private void btn_ao1_Click(object sender, System.EventArgs e)
        {
            server.InstantWrite<double>("D1", double.Parse(textBox1.Text.Trim()));
        }
        HighCallback callback;
        private void button1_Click(object sender, System.EventArgs e)
        {
            callback = (data) =>
            {
                //这里如果返回的是float可以直接调用htUserCurve1.AddCurveData("AI1", data);
                foreach (var val in data)
                {
                    htUserCurve1.AddCurveData("Displacement", val);
                    Thread.Sleep(2);
                }
            };
            server.StartReadingTask(1, "D2", 10, callback);
        }
    }
}
