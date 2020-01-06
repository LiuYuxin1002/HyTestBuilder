using System;
using System.ComponentModel;
using System.Windows.Forms;
using HyTestRTDataService.RunningMode;
using System.Threading;

namespace HyTestRTDataService.Controls.Scopes
{
    public partial class HTDataScanner : UserControl
    {
        [Category("AAA DATA BINDING")]
        public string VarName
        {
            get
            {
                return this.varName;
            }
            set
            {
                this.varName = value;
                Invalidate();
            }
        }

        private String varName = "AI1";

        public void OnDataChanged(object sender, EventArgs e)
        {
            FetchDataAndShow();
        }

        private void FetchDataAndShow()
        {
            if (varName == null || varName == "") return;

            RunningServer server = RunningServer.getServer();
            double value = server.NormalRead<double>(varName);

            /*using delegate to avoid cross-thread changing.*/
            while (!this.IsHandleCreated)
            {
                Thread.Sleep(1000);
            }
            Action action = () =>
            {
                this.textBox1.Text = value.ToString();
            };
            Invoke(action);
        }

        public HTDataScanner()
        {
            InitializeComponent();
            //data subjection
            RunningServer server = RunningServer.getServer();
            server.Connected += OnConnected;
        }

        private void OnConnected(object sender, EventArgs e)
        {
            RunningServer server = RunningServer.getServer();
            server.DataRefresh += OnDataChanged;
        }
    }
}
