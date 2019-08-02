using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// 数值转换工具。
    /// 模拟量输入4-20ma  对应的数字量是5530-27648
    /// 模拟量输出0-10V 对应的数字量是0-27648
    /// </summary>
    public class DataTransformer
    {
        public Type datatype;
        
        //模拟量数字最小值
        private const int ANALOG_MIN = -32767;
        //模拟量数字最大值
        private const int ANALOG_MAX = 32768;

        private const int ANALOG_MIN_IN = 0;
        private const int ANALOG_MAX_IN = 65535;
        ////物理输入最大值
        //private const double INPUT_MAX = 10.0;
        ////物理输入最小值
        //private const double INPUT_MIN = -10.0;
        ////物理输出最大值
        //private const double OUTPUT_MAX = 10.0;
        ////物理输出最小值
        //private const double OUTPUT_MIN = -10.0;

        private const double TRUE = 1.0;
        private const double FALSE = 0.0;

        public DataTransformer() { }

        /// <summary>
        /// 将数据池double转为bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DoubleToBool(double value)
        {
            if (value == TRUE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将数据池double转为int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DoubleToInt(double value)
        {
            return (int)value;
        }

        /// <summary>
        /// 将数据池double转为double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double DoubleToDouble(double value)
        {
            return value;
        }

        /// <summary>
        /// 将模拟量转为实际物理量
        /// </summary>
        public static double AnalogToPhysical(int analog, int vmax, int vmin)   //缩小
        {
            double rate = (double)(vmax - vmin) / (double)(ANALOG_MAX - ANALOG_MIN);
            double result = rate * (analog - ANALOG_MIN) + vmin;
            return result;
        }

        /// <summary>
        /// 将实际物理量转为模拟量
        /// </summary>
        public static int PhysicalToAnalog(double pvalue, int vmax, int vmin)   //放大
        {
            double rate = (ANALOG_MAX - ANALOG_MIN) / (vmax - vmin);
            int result = (int)(rate * (pvalue - vmin)) + ANALOG_MIN;
            return result;
        }

        /// <summary>
        /// 将模拟量转为实际物理量
        /// </summary>
        public static double AnalogToPhysical_IN(int analog, int vmax, int vmin)   //缩小
        {
            double rate = (double)(vmax - vmin) / (double)(ANALOG_MAX_IN - ANALOG_MIN_IN);
            double result = rate * (analog - ANALOG_MIN_IN) + vmin;
            return result;
        }

        /// <summary>
        /// 将实际物理量转为模拟量
        /// </summary>
        public static int PhysicalToAnalog_OUT(double pvalue, int vmax, int vmin)   //放大
        {
            double rate = (ANALOG_MAX_IN - ANALOG_MIN_IN) / (vmax - vmin);
            int result = (int)(rate * (pvalue - vmin)) + ANALOG_MIN_IN;
            return result;
        }

        /// <summary>
        /// 将模拟量转为物理输入量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static double InputAnalogToPhysical(int analog, int vmax, int vmin)
        {
            double rate = (double)(vmin - vmax) / (double)(ANALOG_MIN - ANALOG_MAX);    //rate<1
            double result = rate * (analog - ANALOG_MIN) + vmin;
            return result;
        }

        /// <summary>
        /// 将物理输入量转为模拟量
        /// </summary>
        /// <param name="physical"></param>
        /// <returns></returns>
        public static int InputPhysicalToAnalog(double physical, int vmax, int vmin)
        {
            double rate = (ANALOG_MAX - ANALOG_MAX) / (vmax - vmin);  //rate>1
            int result = (int)((physical - vmin) * rate) + ANALOG_MIN;
            return result;
        }

        /// <summary>
        /// 将模拟量转为物理输出量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static double OutputAnalogToPhysical(int analog, int vmax, int vmin)
        {
            double rate = (vmax - vmin) / (ANALOG_MAX - ANALOG_MIN);    //rate<1
            double result = rate * (analog - ANALOG_MIN) + vmin;
            return result;
        }

        /// <summary>
        /// 将物理输出量转换为模拟量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static int OutputPhysicalToAnalog(double physical, int vmax, int vmin)
        {
            if (physical > vmax || physical<vmin)
            {
                MessageBox.Show(physical+"不在有效范围");
                return -1;
            }

            double rate = (ANALOG_MAX - ANALOG_MIN) / (vmax - vmin);    //rate>1
            int result = (int)(rate * (physical - vmin)) + ANALOG_MIN;
            return result;
        }

        public double TransDigitalToBoolDouble(byte digital) 
        {
            if (digital==1)
            {
                return TRUE;
            }
            else
            {
                return FALSE;
            }
        }

    }
}
