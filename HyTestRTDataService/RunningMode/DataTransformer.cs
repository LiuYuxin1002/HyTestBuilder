using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.RunningMode
{
    public class DataTransformer
    {
        public Type datatype;
        public int analogMin, analogMax;
        public double doubleMin, doubleMax;
        public int intMin, intMax;

        //bool
        public DataTransformer()
        {

        }
        //int
        public DataTransformer(int analogMin, int analogMax, int intMin, int intMax)
        {
            this.analogMin = analogMin;
            this.analogMax = analogMax;
            this.intMin = intMin;
            this.intMax = intMax;
        }
        //double
        public DataTransformer(int analogMin, int analogMax, double doubleMin, double doubleMax)
        {
            this.analogMin = analogMin;
            this.analogMax = analogMax;
            this.doubleMin = doubleMin;
            this.doubleMax = doubleMax;
        }

        private const double TRUE = 1.0;
        private const double FALSE = 0.0;
        public static bool TransformingBool(double value)
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
        public static int TransformingInt(double value)
        {
            return (int)value;
        }
        public static double TransformingDouble(double value)
        {
            return value;
        }

        public double TransAnalogToDouble(int analog)
        {
            double rate = (double)((doubleMin - doubleMax) / analogMin - analogMax);
            double result = rate * (analog - analogMin) + doubleMin;
            return result;
        }

        public double TransAnalogToIntDouble(int analog)
        {
            double rate = (double)(intMax - intMin) / (analogMax - analogMin);
            int result = (int)(rate * (analog - analogMin)) + intMin;
            return result;
        }

        public double TransDigitalToBoolDouble(bool digital)
        {
            if (digital)
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
