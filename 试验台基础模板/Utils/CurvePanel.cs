using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardTemplate.utils
{
    class CurvePanel
    {
        private IList<Curve> curveList = new List<Curve>();

        public string Title { get; set; }
        public string XLabel { get; set; }
        public string YLabel { get; set; }

        public CurvePanel()
        {
        }

        public void AddCurve(Curve curve)
        {
            this.curveList.Add(curve);
        }

        public void SaveBMP(string path)
        {
            List<double[]> listXY = new List<double[]>();
            List<string> curve_names = new List<string>();
            List<double> listMaxX = new List<double>();
            List<double> listMinX = new List<double>();
            List<double> listMaxY = new List<double>();
            List<double> listMinY = new List<double>();
            foreach (Curve curve in this.curveList)
            {
                if (curve.X.Count() == 0)
                    continue;
                listXY.Add(curve.X);
                listXY.Add(curve.Y);
                curve_names.Add(curve.Name);
                listMaxX.Add(curve.X.Count() == 0 ? 1 : curve.X.Max());
                listMinX.Add(curve.X.Count() == 0 ? -1 : curve.X.Min());
                listMaxY.Add(curve.Y.Count() == 0 ? 1 : curve.Y.Max());
                listMinY.Add(curve.Y.Count() == 0 ? -1 : curve.Y.Min());
            }

            ZedSave.SavePic(path, Title, XLabel, YLabel, listMinX.Min(), listMaxX.Max(), listMinY.Min(), listMaxY.Max(), listXY, curve_names, true);
        }
    }
}
