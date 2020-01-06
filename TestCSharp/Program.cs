using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCSharp
{
    class Program
    {
        static HighFreqReadCallback callback;

        static void Main(string[] args)
        {
            BindingMethod();
            Connector.HighFrequencyRead(1, 1, callback);

            Thread.Sleep(5000);
        }

        static void BindingMethod()
        {
            callback = (res) =>
            {
                Console.WriteLine("本次测得试验数据共有{0}组", res.Length);
                foreach(var row in res)
                {
                    foreach(var val in row)
                    {
                        Console.Write("\t" + val);
                    }
                    Console.WriteLine();
                }
            };
        }
    }

    class Connector
    {
        /// <summary>
        /// 高速采集接口
        /// </summary>
        /// <param name="slave">设备列表</param>
        /// <param name="channelId">端口列表</param>
        /// <param name="callbackPoint">采集后的数据集回调函数</param>
        [DllImport("TestC.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void HighFrequencyRead(int slave, int channelId, [MarshalAs(UnmanagedType.FunctionPtr)]HighFreqReadCallback callbackPoint);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void HighFreqReadCallback([MarshalAs(UnmanagedType.LPArray, SizeConst = 1000)]int[][] res);
}
