using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AdapterConfigButton_Click(object sender, EventArgs e)
        {
            FormNIC nicf = new FormNIC();
            nicf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RWTest ftest = new RWTest();
            ftest.Show();
        }
    }
}
