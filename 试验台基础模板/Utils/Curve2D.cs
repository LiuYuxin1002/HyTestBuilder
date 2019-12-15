using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardTemplate.utils
{
    public class Curve2D
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(Curve2D));
        private Graphics objGraphics; //Graphics 类提供将对象绘制到显示设备的方法
        private Bitmap objBitmap; //位图对象
        private float fltWidth = 600; //图像宽度,最大流量
        private float fltHeight = 300; //图像高度,最大压力
        private float fltXSlice = 50; //X轴刻度宽度
        private float fltYSlice = 50; //Y轴刻度宽度
        private float fltXSliceValue = 20; //X轴刻度的数值宽度
        private float fltYSliceValue = 20; //Y轴刻度的数值宽度
        private float fltXSliceBegin = 0; //Y轴刻度开始值
        private float fltYSliceBegin = 0; //Y轴刻度开始值
        private float fltXSliceEnd = 0; //X轴刻度开始值
        private float fltYSliceEnd = 0; //Y轴刻度开始值
        private float fltTension = 0.5f;
        private string strTitle = "稳态压差流量特性曲线"; //标题
        private string strXAxisText = "流量（L/Min）"; //X轴说明文字
        private string strYAxisText = "压力（MPa）"; //Y轴说明文字
        //private string[] strsKeys = new string[13] { "0", "10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "110", "120" }; //键
        private List<float> strsKeys = new List<float>();
        private List<float> strsValues = new List<float>(); //流量值
        private List<float> fltsValues = new List<float>();//压力值
        private Color clrBgColor = Color.Snow; //背景色
        private Color clrTextColor = Color.Black; //文字颜色
        private Color clrBorderColor = Color.Black; //整体边框颜色
        private Color clrAxisColor = Color.Black; //轴线颜色
        private Color clrAxisTextColor = Color.Black; //轴说明文字颜色
        private Color clrSliceTextColor = Color.Black; //刻度文字颜色
        private Color clrSliceColor = Color.Black; //刻度颜色
        private Color[] clrsCurveColors = new Color[] { Color.Red }; //曲线颜色
        private float fltXSpace = 100f; //图像左右距离边缘距离
        private float fltYSpace = 100f; //图像上下距离边缘距离
        private int intFontSize = 9; //字体大小号数
        private float fltXRotateAngle = 30f; //X轴文字旋转角度
        private float fltYRotateAngle = 0f; //Y轴文字旋转角度
        private int intCurveSize = 2; //曲线线条大小
        private int intFontSpace = 0; //intFontSpace 是字体大小和距离调整出来的一个比较适合的数字
        public float Xsl;
        public float Ysl;
        public float ssd;
        #region 公共属性
        /// <summary>
        /// 图像的宽度
        /// </summary>
        public float Width
        {
            set
            {
                if (value < 100)
                {
                    fltWidth = 100;
                }
                else
                {
                    fltWidth = value;
                }
            }
            get
            {
                if (fltWidth <= 100)
                {
                    return 100;
                }
                else
                {
                    return fltWidth;
                }
            }
        }
        /// <summary>
        /// 图像的高度
        /// </summary>
        public float Height
        {
            set
            {
                if (value < 100)
                {
                    fltHeight = 100;
                }
                else
                {
                    fltHeight = value;
                }
            }
            get
            {
                if (fltHeight <= 100)
                {
                    return 100;
                }
                else
                {
                    return fltHeight;
                }
            }
        }
        /// <summary>
        /// X轴刻度宽度
        /// </summary>
        public float XSlice
        {
            set { fltXSlice = value; }
            get { return fltXSlice; }
        }
        /// <summary>
        /// Y轴刻度宽度
        /// </summary>
        public float YSlice
        {
            set { fltYSlice = value; }
            get { return fltYSlice; }
        }
        //public float
        /// <summary>
        /// X轴刻度的数值宽度
        /// </summary>
        public float XSliceValue
        {
            set { fltXSliceValue = value; }
            get { return fltXSliceValue; }
        }
        /// <summary>
        /// Y轴刻度的数值宽度
        /// </summary>
        public float YSliceValue
        {
            set { fltYSliceValue = value; }
            get { return fltYSliceValue; }
        }
        /// <summary>
        /// X轴刻度开始值
        /// </summary>
        public float XSliceBegin
        {
            set { fltXSliceBegin = value; }
            get { return fltXSliceBegin; }
        }
        /// <summary>
        /// Y轴刻度开始值
        /// </summary>
        public float YSliceBegin
        {
            set { fltYSliceBegin = value; }
            get { return fltYSliceBegin; }
        }
        /// <summary>
        /// X轴刻度结束值
        /// </summary>
        public float XSliceEnd
        {
            set { fltXSliceEnd = value; }
            get { return fltXSliceEnd; }
        }
        /// <summary>
        /// Y轴刻度结束值
        /// </summary>
        public float YSliceEnd
        {
            set { fltYSliceEnd = value; }
            get { return fltYSliceEnd; }
        }
        /// <summary>
        /// 张力系数
        /// </summary>
        public float Tension
        {
            set
            {
                if (value < 0.0f && value > 1.0f)
                {
                    fltTension = 0.5f;
                }
                else
                {
                    fltTension = value;
                }
            }
            get
            {
                return fltTension;
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { strTitle = value; }
            get { return strTitle; }
        }
        /// <summary>
        /// 键，X轴数据
        /// </summary>
        public List<float> Keys
        {
            set { strsKeys = value; }
            get { return strsKeys; }
        }

        public List<float> Keyss
        {
            set { strsValues = value; }
            get { return strsValues; }
        }

        /// <summary>
        /// 值，Y轴数据
        /// </summary>
        public List<float> Values
        {
            set { fltsValues = value; }
            get { return fltsValues; }
        }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BgColor
        {
            set { clrBgColor = value; }
            get { return clrBgColor; }
        }
        /// <summary>
        /// 文字颜色
        /// </summary>
        public Color TextColor
        {
            set { clrTextColor = value; }
            get { return clrTextColor; }
        }
        /// <summary>
        /// 整体边框颜色
        /// </summary>
        public Color BorderColor
        {
            set { clrBorderColor = value; }
            get { return clrBorderColor; }
        }
        /// <summary>
        /// 轴线颜色
        /// </summary>
        public Color AxisColor
        {
            set { clrAxisColor = value; }
            get { return clrAxisColor; }
        }
        /// <summary>
        /// X轴说明文字
        /// </summary>
        public string XAxisText
        {
            set { strXAxisText = value; }
            get { return strXAxisText; }
        }
        /// <summary>
        /// Y轴说明文字
        /// </summary>
        public string YAxisText
        {
            set { strYAxisText = value; }
            get { return strYAxisText; }
        }
        /// <summary>
        /// 轴说明文字颜色
        /// </summary>
        public Color AxisTextColor
        {
            set { clrAxisTextColor = value; }
            get { return clrAxisTextColor; }
        }
        /// <summary>
        /// 刻度文字颜色
        /// </summary>
        public Color SliceTextColor
        {
            set { clrSliceTextColor = value; }
            get { return clrSliceTextColor; }
        }
        /// <summary>
        /// 刻度颜色
        /// </summary>
        public Color SliceColor
        {
            set { clrSliceColor = value; }
            get { return clrSliceColor; }
        }
        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color[] CurveColors
        {
            set { clrsCurveColors = value; }
            get { return clrsCurveColors; }
        }
        /// <summary>
        /// X轴文字旋转角度
        /// </summary>
        public float XRotateAngle
        {
            get { return fltXRotateAngle; }
            set { fltXRotateAngle = value; }
        }
        /// <summary>
        /// Y轴文字旋转角度
        /// </summary>
        public float YRotateAngle
        {
            get { return fltYRotateAngle; }
            set { fltYRotateAngle = value; }
        }
        /// <summary>
        /// 图像左右距离边缘距离
        /// </summary>
        public float XSpace
        {
            get { return fltXSpace; }
            set { fltXSpace = value; }
        }
        /// <summary>
        /// 图像上下距离边缘距离
        /// </summary>
        public float YSpace
        {
            get { return fltYSpace; }
            set { fltYSpace = value; }
        }
        /// <summary>
        /// 字体大小号数
        /// </summary>
        public int FontSize
        {
            get { return intFontSize; }
            set { intFontSize = value; }
        }
        /// <summary>
        /// 曲线线条大小
        /// </summary>
        public int CurveSize
        {
            get { return intCurveSize; }
            set { intCurveSize = value; }
        }
        #endregion
        /// <summary>
        /// 自动根据参数调整图像大小
        /// </summary>
        public void Fit(float Xstart, float Xend, float Ystart, float Yend, float Xslice, float Yslice)
        {
            Xsl = Xslice;
            Ysl = Yslice;
            YSliceValue = Yslice;
            XSliceBegin = Xstart;
            //计算字体距离
            intFontSpace = FontSize + 5;
            //计算图像边距
            float fltSpace = Math.Min(Width / 6, Height / 6);
            XSpace = fltSpace;
            YSpace = fltSpace;
            //计算X轴刻度宽度
            XSlice = (Width - 2 * XSpace) / ((Xend - Xstart) / Xslice);
            int num = (int)((Xend - Xstart) / Xslice);
            for (int g = 0; g <= num; g++)
            {
                Keys.Add(Xstart + Xslice * g);
            }
            //计算Y轴刻度宽度和Y轴刻度开始值
            float fltMinValue = 0;
            float fltMaxValue = 0;
            
            YSliceBegin = Ystart;
            //int intYSliceCount = (int)(fltMaxValue / YSliceValue);//y最大值除以数值宽度得到个数
            int intYSliceCount = (int)((Yend - Ystart) / Yslice);
            if ((Yend - Ystart) % Yslice != 0)
            {
                intYSliceCount++;
            }
            YSlice = (Height - 2 * YSpace) / intYSliceCount;//y轴大间隔宽度，个数是5
        }
        /// <summary>
        /// 生成曲线图
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Xstart"></param>
        /// <param name="Xend"></param>
        /// <param name="Ystart"></param>
        /// <param name="Yend"></param>
        /// <param name="Xslice"></param>
        /// <param name="Yslice"></param>
        /// <param name="横坐标名称"></param>
        /// <param name="纵坐标名称"></param>
        /// <returns></returns>
        public Bitmap Plot(List<float> X, List<float> Y, float Xstart, float Xend, float Ystart, float Yend, float Xslice, float Yslice, String 横坐标名称, String 纵坐标名称, Color 颜色, string 曲线名称, string 试验名称)
        {
            if (Xstart == Xend || Ystart == Yend)
            {
                log.Error("范围输入错误");
                return objBitmap;
            }
            Fit(Xstart, Xend, Ystart, Yend, Xslice, Yslice);
            intCurveSize++;
            Keyss = X;
            Title = 试验名称;
            //Values = Y;
            InitializeGraph();
            //fltYSpace = (float)10.0;
            int intKeysCount = X.Count;//10
            int intValuesCount = Y.Count;//10
            //int intCurvesCount = intValuesCount / intKeysCount;//1
            float[] fltCurrentValues = new float[intKeysCount];
            for (int j = 0; j < intKeysCount; j++)
            {
                fltCurrentValues[j] = Y[j];
            }
            DrawContent(ref objGraphics, fltCurrentValues, 颜色, Xslice, Yslice);
            GenerateTitle(ref objGraphics, 曲线名称, 颜色);
            return objBitmap;
        }
        public Bitmap Plot(List<float> X, List<float> Y, Color 颜色, string 曲线名称)
        {
            //Fit(Xstart, Xend, Ystart, Yend, Xslice, Yslice);
            //InitializeGraph();
            //fltYSpace = (float)10.0;
            //Values = Y;
            ssd = 3;
            intCurveSize++;
            int intKeysCount = Keyss.Count;//10
            int intValuesCount = Y.Count;//10
            int intCurvesCount = intValuesCount / intKeysCount;//1
            float[] fltCurrentValues = new float[intKeysCount];
            for (int j = 0; j < intKeysCount; j++)
            {
                fltCurrentValues[j] = Y[j];
            }
            DrawContent(ref objGraphics, fltCurrentValues, 颜色, Xsl, Ysl);
            GenerateTitle(ref objGraphics, 曲线名称, 颜色);
            return objBitmap;
        }
        /// <summary>
        /// 初始化和填充图像区域，画出边框，初始标题
        /// </summary>
        private void InitializeGraph()
        {
            //根据给定的高度和宽度创建一个位图图像
            objBitmap = new Bitmap((int)Width, (int)Height);
            //从指定的 objBitmap 对象创建 objGraphics 对象 (即在objBitmap对象中画图)
            objGraphics = Graphics.FromImage(objBitmap);
            //根据给定颜色(LightGray)填充图像的矩形区域 (背景)
            objGraphics.DrawRectangle(new Pen(BorderColor, 1), 0, 0, Width - 1, Height - 1); //画边框
            objGraphics.FillRectangle(new SolidBrush(BgColor), 1, 1, Width - 2, Height - 2); //填充边框
            //画X轴,注意图像的原始X轴和Y轴计算是以左上角为原点，向右和向下计算的
            float fltX1 = XSpace;
            float fltY1 = Height - YSpace;
            float fltX2 = Width - XSpace + XSlice / 2;
            float fltY2 = fltY1;
            objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor), 1), fltX1, fltY1, fltX2, fltY2);
            //画Y轴
            fltX1 = XSpace;
            fltY1 = Height - YSpace;
            fltX2 = XSpace;
            fltY2 = YSpace - YSlice / 2;
            objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor), 1), fltX1, fltY1, fltX2, fltY2);
            //初始化轴线说明文字
            SetAxisText(ref objGraphics);
            //初始化X轴上的刻度和文字
            SetXAxis(ref objGraphics);
            //初始化Y轴上的刻度和文字
            SetYAxis(ref objGraphics);
            //初始化标题
            CreateTitle(ref objGraphics);
        }
        /// <summary>
        /// 初始化轴线说明文字
        /// </summary>
        /// <param name="objGraphics"></param>
        private void SetAxisText(ref Graphics objGraphics)
        {
            float fltX = Width - XSpace + XSlice - (XAxisText.Length - 1) * intFontSpace;
            float fltY = Height - YSpace - intFontSpace;
            objGraphics.DrawString(XAxisText, new Font("宋体", FontSize), new SolidBrush(AxisTextColor), fltX, fltY + 2 * intFontSpace);
            fltX = XSpace + 5;
            fltY = YSpace - YSlice / 2 - intFontSpace;
            //for (int i = 0; i < YAxisText.Length; i++)
            //{
            //    objGraphics.DrawString(YAxisText[i].ToString(), new Font("宋体", FontSize), new SolidBrush(AxisTextColor), fltX - 10, fltY);
            //    fltX += intFontSpace; //字体上下距离
            //}
            objGraphics.DrawString(YAxisText, new Font("宋体", FontSize), new SolidBrush(AxisTextColor), fltX - 10, fltY);
        }
        /// <summary>
        /// 初始化X轴上的刻度和文字
        /// </summary>
        /// <param name="objGraphics"></param>
        private void SetXAxis(ref Graphics objGraphics)
        {
            float fltX1 = XSpace;
            float fltY1 = Height - YSpace;
            float fltX2 = XSpace;
            float fltY2 = Height - YSpace;
            int iCount = 0;
            int iSliceCount = 1;
            float Scale = 0;
            float iWidth = ((Width - 2 * XSpace) / XSlice) * 50; //将要画刻度的长度分段，并乘以50，以10为单位画刻度线。
            float fltSliceHeight = XSlice / 10; //刻度线的高度
            objGraphics.TranslateTransform(fltX1, fltY1); //平移图像(原点)
            objGraphics.RotateTransform(XRotateAngle, MatrixOrder.Prepend); //旋转图像
            objGraphics.DrawString(Keys[0].ToString(), new Font("宋体", FontSize), new SolidBrush(SliceTextColor), 0, 0);
            objGraphics.ResetTransform(); //重置图像
            for (int i = 0; i <= iWidth; i += 10) //以10为单位
            {
                Scale = i * XSlice / 50;//即(i / 10) * (XSlice / 5)，将每个刻度分五部分画，但因为i以10为单位，得除以10
                if (iCount == 5)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)), fltX1 + Scale, fltY1 + fltSliceHeight * 1.5f, fltX2 + Scale, fltY2 - fltSliceHeight * 1.5f);
                    //画网格虚线
                    Pen penDashed = new Pen(new SolidBrush(AxisColor));
                    penDashed.DashStyle = DashStyle.Dash;
                    objGraphics.DrawLine(penDashed, fltX1 + Scale, fltY1, fltX2 + Scale, YSpace - YSlice / 2);
                    //这里显示X轴刻度
                    if (iSliceCount <= Keys.Count - 1)
                    {
                        objGraphics.TranslateTransform(fltX1 + Scale, fltY1);
                        objGraphics.RotateTransform(XRotateAngle, MatrixOrder.Prepend);
                        objGraphics.DrawString(Keys[iSliceCount].ToString(), new Font("宋体", FontSize), new SolidBrush(SliceTextColor), 0, 0);
                        objGraphics.ResetTransform();
                    }
                    else
                    {
                        //超过范围，不画任何刻度文字
                    }
                    iCount = 0;
                    iSliceCount++;
                    if (fltX1 + Scale > Width - XSpace)
                    {
                        break;
                    }
                }
                else
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)), fltX1 + Scale, fltY1 + fltSliceHeight, fltX2 + Scale, fltY2 - fltSliceHeight);
                }
                iCount++;
            }
        }
        /// <summary>
        /// 初始化Y轴上的刻度和文字
        /// </summary>
        /// <param name="objGraphics"></param>
        private void SetYAxis(ref Graphics objGraphics)
        {
            float fltX1 = XSpace;
            float fltY1 = Height - YSpace;
            float fltX2 = XSpace;
            float fltY2 = Height - YSpace;
            int iCount = 0;
            float Scale = 0;
            int iSliceCount = 1;
            float iHeight = ((Height - 2 * YSpace) / YSlice) * 50; //将要画刻度的长度分段，并乘以50，以10为单位画刻度线。
            float fltSliceWidth = YSlice / 10; //刻度线的宽度
            string strSliceText = string.Empty;
            objGraphics.TranslateTransform(XSpace - intFontSpace * YSliceBegin.ToString().Length, Height - YSpace); //平移图像(原点)
            //objGraphics.TranslateTransform(XSpace, Height - YSpace); //平移图像(原点)
            objGraphics.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //旋转图像
            objGraphics.DrawString(YSliceBegin.ToString(), new Font("宋体", FontSize), new SolidBrush(SliceTextColor), 0, 0);
            objGraphics.ResetTransform(); //重置图像
            for (int i = 0; i < iHeight; i += 10)
            {
                Scale = i * YSlice / 50; //即(i / 10) * (YSlice / 5)，将每个刻度分五部分画，但因为i以10为单位，得除以10
                if (iCount == 5)
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(AxisColor)), fltX1 - fltSliceWidth * 1.5f, fltY1 - Scale, fltX2 + fltSliceWidth * 1.5f, fltY2 - Scale);
                    //画网格虚线
                    Pen penDashed = new Pen(new SolidBrush(AxisColor));
                    penDashed.DashStyle = DashStyle.Dash;
                    objGraphics.DrawLine(penDashed, XSpace, fltY1 - Scale, Width - XSpace + XSlice / 2, fltY2 - Scale);
                    //这里显示Y轴刻度
                    strSliceText = Convert.ToString(YSliceValue * iSliceCount + YSliceBegin);
                    objGraphics.TranslateTransform(XSpace - intFontSize * strSliceText.Length, fltY1 - Scale); //平移图像(原点)
                    objGraphics.RotateTransform(YRotateAngle, MatrixOrder.Prepend); //旋转图像
                    objGraphics.DrawString(strSliceText, new Font("宋体", FontSize), new SolidBrush(SliceTextColor), 0, 0);
                    objGraphics.ResetTransform(); //重置图像
                    iCount = 0;
                    iSliceCount++;
                }
                else
                {
                    objGraphics.DrawLine(new Pen(new SolidBrush(SliceColor)), fltX1 - fltSliceWidth, fltY1 - Scale, fltX2 + fltSliceWidth, fltY2 - Scale);
                }
                iCount++;
            }
        }
        /// <summary>
        /// 画曲线
        /// </summary>
        /// <param name="objGraphics"></param>
        private void DrawContent(ref Graphics objGraphics, float[] fltCurrentValues, Color clrCurrentColor, float Xslice, float Yslice)
        {
            Pen CurvePen = new Pen(clrCurrentColor, 1);
            PointF[] CurvePointF = new PointF[Keyss.Count];//10
            float keys = 0;
            float values = 0;
            for (int i = 0; i < Keyss.Count; i++)
            {
                //keys = XSlice * i + XSpace;
                keys = XSlice * ((Keyss[i] - XSliceBegin) / Xslice) + XSpace;
                //keys =(Width)
                values = (Height - YSpace) - YSlice * ((fltCurrentValues[i] - YSliceBegin) / Yslice);//刻度宽度为图片宽度，数值宽度为压力值
                //values = Height-YSpace;
                CurvePointF[i] = new PointF(keys, values);
            }
            objGraphics.DrawCurve(CurvePen, CurvePointF, Tension);
        }

        /// <summary>
        /// 初始化标题
        /// </summary>
        /// <param name="objGraphics"></param>
        private void CreateTitle(ref Graphics objGraphics)
        {
            objGraphics.DrawString(Title, new Font("宋体", FontSize + 2, FontStyle.Bold), new SolidBrush(TextColor), new Point((int)(Width - XSpace) - intFontSize * Title.Length - 230, (int)(YSpace - YSlice / 2 - intFontSpace)));

        }

        private void GenerateTitle(ref Graphics objGraphics, string 曲线名称, Color 颜色)
        {
            objGraphics.DrawString(曲线名称, new Font("宋体", FontSize, FontStyle.Bold), new SolidBrush(TextColor), new Point((int)(Width - XSpace) - intFontSize * 曲线名称.Length + 10, (int)(YSpace - YSlice / 2 + intCurveSize * 14)));
            Point point1 = new Point((int)(Width - XSpace) - intFontSize * 曲线名称.Length - 25, (int)(YSpace - YSlice / 2 + intCurveSize * 14 + 4));
            Point point2 = new Point((int)(Width - XSpace) - intFontSize * 曲线名称.Length + 10, (int)(YSpace - YSlice / 2 + intCurveSize * 14 + 4));
            objGraphics.DrawLine(new Pen(new SolidBrush(颜色), 3), point1, point2);


        }
    }
}
