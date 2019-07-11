﻿using HyTestIEInterface;
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
        RunningServer server = RunningServer.getServer();

        bool[] state = new bool[8];

        public FormTestRunning()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            state[0] = !state[0];
            server.InstantWrite("DI1", state[0]);
            this.lbLed1.State = state[0] ? LedState.On : LedState.Off;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            state[1] = !state[1];
            server.InstantWrite("DI2", state[1]);
            this.lbLed2.State = state[1] ? LedState.On : LedState.Off;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            state[2] = !state[2];
            server.InstantWrite("DI3", state[2]);
            this.lbLed3.State = state[2] ? LedState.On : LedState.Off;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            state[3] = !state[3];
            server.InstantWrite("DI4", state[3]);
            this.lbLed4.State = state[3] ? LedState.On : LedState.Off;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            state[4] = !state[4];
            server.InstantWrite("DI5", state[4]);
            this.lbLed5.State = state[4] ? LedState.On : LedState.Off;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            state[5] = !state[5];
            server.InstantWrite("DI6", state[5]);
            this.lbLed6.State = state[5] ? LedState.On : LedState.Off;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state[6] = !state[6];
            server.InstantWrite("DI7", state[6]);
            this.lbLed7.State = state[6] ? LedState.On : LedState.Off;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state[7] = !state[7];
            server.InstantWrite("DI8", state[7]);
            this.lbLed8.State = state[7] ? LedState.On : LedState.Off;
        }
    }
}
