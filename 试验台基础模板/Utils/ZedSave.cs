using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace StandardTemplate.utils
{
    class ZedSave
    {
        /// <summary>
        /// zedgraph控件保存图片
        /// </summary>
        /// <param name="save_path">保存地址 e.g. @"C:\Users\JrZhang\Desktop\1.bmp"</param>
        /// <param name="title">图片标题</param>
        /// <param name="x_label">x轴坐标名称</param>
        /// <param name="y_label">y轴坐标名称</param>
        /// <param name="x_min">x轴最小值</param>
        /// <param name="x_max">x轴最大值</param>
        /// <param name="y_min">y轴最小值</param>
        /// <param name="y_max">y轴最大值</param>
        /// <param name="args">坐标点对列表，依次是每个曲线的x，y轴坐标</param>
        /// e.g.    List<double[]> args;
        ///         args = new List<double[]> { x1, y1, x2, y2, ...., xn, yn };
        /// <param name="curve_names">曲线名称列表</param>
        /// e.g.    List<string> names;
        ///         names = new List<string> { "name_1", "name_2",...., "name_n" };
        /// <param name="show_symbol">是否显示曲线的坐标点标记，默认为true</param>
        /// <param name="size_factor">图片大小的比例，例如：1.2或0.8</param>
        public static void SavePic(string save_path, string title, string x_label, string y_label,
            double x_min, double x_max, double y_min, double y_max, List<double[]> args,
            List<string> curve_names, bool show_symbol = true, double size_factor = 2)
        {
            ZedGraphControl zgc = new ZedGraphControl();
            zgc.Width = (int)(400 * size_factor);
            zgc.Height = (int)(300 * size_factor);
            zgc.GraphPane.Title.Text = title;
            zgc.GraphPane.XAxis.Title.Text = x_label;
            zgc.GraphPane.YAxis.Title.Text = y_label;
            zgc.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.Scale.Max = y_max;
            zgc.GraphPane.YAxis.Scale.Min = y_min;
            zgc.GraphPane.XAxis.Scale.Max = x_max;
            zgc.GraphPane.XAxis.Scale.Min = x_min;
            List<SymbolType> list_symbol;
            list_symbol = new List<SymbolType>();
            foreach (SymbolType type in Enum.GetValues(typeof(SymbolType)))
            {
                list_symbol.Add(type);
            }

            // zgc.GraphPane.XAxis.Type = ZedGraph.AxisType.LinearAsOrdinal;

            for (int i = 0; i < curve_names.Count; i++)
            {
                PointPairList list = new PointPairList(args[2 * i], args[2 * i + 1]);
                if (show_symbol == true)
                    zgc.GraphPane.AddCurve(curve_names[i], list, Color.Black, list_symbol[i % 12]);
                else
                    zgc.GraphPane.AddCurve(curve_names[i], list, Color.Black, SymbolType.None);
            }
            zgc.AxisChange();
            zgc.Refresh();
            zgc.GetImage().Save(save_path);
        }

        public static void AddPic(string save_path, string title, string x_label, string y_label,
           double x_min, double x_max, double y_min, double y_max, List<double[]> args,
           List<string> curve_names, bool show_symbol = true)
        {

            ZedGraphControl zgc = new ZedGraphControl();
            zgc.Width = 800;
            zgc.Height = 600;
            zgc.GraphPane.Title.Text = title;
            zgc.GraphPane.XAxis.Title.Text = x_label;
            zgc.GraphPane.YAxis.Title.Text = y_label;
            zgc.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.Scale.Max = y_max;
            zgc.GraphPane.YAxis.Scale.Min = y_min;
            zgc.GraphPane.XAxis.Scale.Max = x_max;
            zgc.GraphPane.XAxis.Scale.Min = x_min;
            List<SymbolType> list_symbol;
            list_symbol = new List<SymbolType>();
            foreach (SymbolType type in Enum.GetValues(typeof(SymbolType)))
            {
                list_symbol.Add(type);
            }

            // zgc.GraphPane.XAxis.Type = ZedGraph.AxisType.LinearAsOrdinal;

            for (int i = 0; i < curve_names.Count; i++)
            {
                //up曲线
                PointPairList list1 = new PointPairList(args[2 * i], args[2 * i + 1]);
                if (show_symbol == true)
                    zgc.GraphPane.AddCurve(curve_names[i], list1, Color.Black, list_symbol[i % 12]);
                else
                    zgc.GraphPane.AddCurve(curve_names[i], list1, Color.Black, SymbolType.None);
                //down曲线
                PointPairList list2 = new PointPairList(args[2 * i + 2], args[2 * i + 3]);
                if (show_symbol == true)
                    zgc.GraphPane.AddCurve(curve_names[i], list2, Color.Black, list_symbol[i % 12]);
                else
                    zgc.GraphPane.AddCurve(curve_names[i], list2, Color.Black, SymbolType.None);
            }
            zgc.AxisChange();
            zgc.Refresh();
            zgc.GetImage().Save(save_path);
        }
        public static void AddPicRen(string save_path, string title, string x_label, string y_label,
           double x_min, double x_max, double y_min, double y_max, List<double[]> args,
           List<string> curve_names, bool show_symbol = true)
        {

            ZedGraphControl zgc = new ZedGraphControl();
            zgc.Width = 800;
            zgc.Height = 600;
            zgc.GraphPane.Title.Text = title;
            zgc.GraphPane.XAxis.Title.Text = x_label;
            zgc.GraphPane.YAxis.Title.Text = y_label;
            zgc.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.MajorGrid.IsVisible = true;
            zgc.GraphPane.YAxis.Scale.Max = y_max;
            zgc.GraphPane.YAxis.Scale.Min = y_min;
            zgc.GraphPane.XAxis.Scale.Max = x_max;
            zgc.GraphPane.XAxis.Scale.Min = x_min;
            List<SymbolType> list_symbol;
            list_symbol = new List<SymbolType>();
            foreach (SymbolType type in Enum.GetValues(typeof(SymbolType)))
            {
                list_symbol.Add(type);
            }
            // zgc.GraphPane.XAxis.Type = ZedGraph.AxisType.LinearAsOrdinal;

            for (int i = 0; i < curve_names.Count; i++)
            {
                //up曲线
                PointPairList list1 = new PointPairList(args[2 * i], args[2 * i + 1]);
                if (show_symbol == true)
                    zgc.GraphPane.AddCurve(curve_names[i], list1, Color.Red, list_symbol[i % 12]);//SymbolType.None
                else
                    zgc.GraphPane.AddCurve(curve_names[i], list1, Color.Black, SymbolType.None);
                //down曲线
                PointPairList list2 = new PointPairList(args[2 * i + 2], args[2 * i + 3]);
                if (show_symbol == true)
                    //zgc.GraphPane.AddCurve(curve_names[i], list2, Color.Black, list_symbol[i+6 % 12]);
                    zgc.GraphPane.AddCurve(curve_names[i], list2, Color.Black, list_symbol[i % 12]);
                else
                    zgc.GraphPane.AddCurve(curve_names[i], list2, Color.Black, SymbolType.None);
            }
            zgc.AxisChange();
            zgc.Refresh();
            zgc.GetImage().Save(save_path);
        }
    }
}
