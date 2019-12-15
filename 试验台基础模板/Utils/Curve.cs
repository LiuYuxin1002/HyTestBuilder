using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardTemplate.utils
{
    public class Curve
    {
        IList<double> XList { get; set; }
        IList<double> YList { get; set; }

        public double[] X
        {
            get
            {
                return XList.ToArray();
            }
        }

        public double[] Y
        {
            get
            {
                return YList.ToArray();
            }
        }

        public string Name { get; set; }

        public Curve()
        {
            this.XList = new List<double>();
            this.YList = new List<double>();
        }

        public void AddPoint(double x, double y)
        {
            this.XList.Add(x);
            this.YList.Add(y);
        }

        public void AddPoint(float x, float y)
        {
            this.XList.Add(x);
            this.YList.Add(y);
        }
    }
}
