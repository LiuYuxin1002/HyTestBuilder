using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.TEST
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            htUserCurve1.SetCurve("AI1", true, null, Color.Red, 1);
            htUserCurve2.SetCurve("AI2", true, null, Color.Yellow, 1);
            htUserCurve3.SetCurve("AI3", true, null, Color.Green, 1);
            htUserCurve4.SetCurve("AI4", true, null, Color.Orange, 1);
        }
        Random random = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            double[] data1 = new double[1000];
            double[] data2 = new double[1000];
            double[] data3 = new double[1000];
            double[] data4 = new double[1000];
            for (int i=0; i<1000; i++)
            {
                data1[i] = (float)Math.Sin(i * 2 * Math.PI / 50.0) * 10 + random.Next(-100, 100) / 150.0f;
                data2[i] = (float)Math.Sin(i * 2 * Math.PI / 50.0) * 10 + random.Next(-100, 100) / 150.0f;
                data3[i] = (float)Math.Sin(i * 2 * Math.PI / 50.0) * 10 + random.Next(-100, 100) / 150.0f;
                data4[i] = (float)Math.Sin(i * 2 * Math.PI / 50.0) * 10 + random.Next(-100, 100) / 150.0f;
                Console.WriteLine(data1[i] + "\t" + data2[i] + "\t" + data3[i] + "\t" + data4[i]);
            }
            htUserCurve1.AddCurveData("AI1", data1);
            htUserCurve2.AddCurveData("AI2", data2);
            htUserCurve3.AddCurveData("AI3", data3);
            htUserCurve4.AddCurveData("AI4", data4);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
