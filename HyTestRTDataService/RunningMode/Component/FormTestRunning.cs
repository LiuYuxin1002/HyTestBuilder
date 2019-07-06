using HyTestIEInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HyTestEtherCAT;
using static LBSoft.IndustrialCtrls.Leds.LBLed;

namespace HyTestRTDataService.RunningMode.Component
{
    public partial class FormTestRunning : Form
    {
        //RunningServer server = RunningServer.getServer();
        IWriter writer = EtherCAT.getEtherCAT(true);

        bool[] state = new bool[8];
        byte TRUE = 1;
        byte FALSE = 0;

        public FormTestRunning()
        {
            InitializeComponent();
            CppConnect.getAdapterNum();
            CppConnect.setAdapterId(1);
            CppConnect.initSlaveConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            state[0] = !state[0];
            writer.WriteDigital(3, 0, state[0] ? TRUE : FALSE);
            this.lbLed1.State = state[0] ? LedState.On : LedState.Off;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            state[1] = !state[1];
            writer.WriteDigital(3, 1, state[1] ? TRUE : FALSE);
            this.lbLed2.State = state[1] ? LedState.On : LedState.Off;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            state[2] = !state[2];
            writer.WriteDigital(3, 2, state[2]?TRUE:FALSE);
            this.lbLed3.State = state[2] ? LedState.On : LedState.Off;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            state[3] = !state[3];
            writer.WriteDigital(3, 3, state[3] ? TRUE : FALSE);
            this.lbLed4.State = state[3] ? LedState.On : LedState.Off;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            state[4] = !state[4];
            writer.WriteDigital(3, 4, state[4] ? TRUE : FALSE);
            this.lbLed5.State = state[4] ? LedState.On : LedState.Off;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            state[5] = !state[5];
            writer.WriteDigital(3, 5, state[5] ? TRUE : FALSE);
            this.lbLed6.State = state[5] ? LedState.On : LedState.Off;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state[6] = !state[6];
            writer.WriteDigital(3, 6, state[6] ? TRUE : FALSE);
            this.lbLed7.State = state[6] ? LedState.On : LedState.Off;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state[7] = !state[7];
            writer.WriteDigital(3, 7, state[7] ? TRUE : FALSE);
            this.lbLed8.State = state[7] ? LedState.On : LedState.Off;
        }
    }
}
