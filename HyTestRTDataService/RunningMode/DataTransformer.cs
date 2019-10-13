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
        
        /**
         * TODO: This part have some problems. Different device have different min/max value.
         * such as: EL3004 is -10V~+10V(-32768~32767), but EL3164 is 0~10V(0~65535)
         */
        private const int ANALOG_MIN = -32768;
        private const int ANALOG_MAX = 32767;
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
        public static double AnalogToPhysical(int analog, int vmax, int vmin)   //缩小
        {
            double rate = (double)(vmax - vmin) / (double)(ANALOG_MAX - ANALOG_MIN);
            double result = rate * (analog - ANALOG_MIN) + vmin;
            return result;
        }
    }
}
