using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandardTemplate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            htUserCurve1.SetCurve("Line1", true, null, Color.Red, 1);
            htUserCurve1.SetCurve("Line2", true, null, Color.Black, 1);
            htUserCurve1.SetCurve("Line3", true, null, Color.Orange, 1);
            htUserCurve1.SetCurve("Line4", true, null, Color.Green, 1);
            htUserCurve1.SetCurve("Line5", true, null, Color.Blue, 1);

            for(int i=0; i<1000; i++)
            {
                htUserCurve1.AddCurveData("Line1", (float)Math.Sin(i / 50.0));
                htUserCurve1.AddCurveData("Line2", 1.2f * (float)Math.Sin(i / 50.0));
                htUserCurve1.AddCurveData("Line3", 1.5f*(float)Math.Sin(i / 50.0));
                htUserCurve1.AddCurveData("Line4", 1.7f*(float)Math.Sin(i / 50.0));
                htUserCurve1.AddCurveData("Line5", 2.0f*(float)Math.Sin(i / 50.0));

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
