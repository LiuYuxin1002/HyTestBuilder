using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBSoft.IndustrialCtrls.Base;
using HyTestRTDataService.RunningMode;

namespace LBIndustrialCtrls.AnalogLable
{
    public partial class DataScanner : UserControl
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
            double value = server.InstantRead<double>(varName);
            this.textBox1.Text = value.ToString();
        }

        public DataScanner()
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
