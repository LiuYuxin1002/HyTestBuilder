using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EtherCATImpl;

namespace TestForm
{
    public partial class Form1 : Form
    {
        //EtherCAT ethercat;
        Server server = new Server();

        public Form1()
        {
            InitializeComponent();
            //ethercat = new EtherCAT(false);
            server.DataRefresh += Server_DataRefresh;
        }

        private void Server_DataRefresh(object sender, EventArgs e)
        {
            textBox1.Text = server.datapool.data_ai.value1.ToString();
            textBox2.Text = server.datapool.data_ai.value2.ToString();
            textBox3.Text = server.datapool.data_ai.value3.ToString();
            textBox4.Text = server.datapool.data_ai.value4.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox1.Text = ((int)ethercat.context.devices[0].value[0]).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.serverStart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.serverStop();
        }
    }
}
