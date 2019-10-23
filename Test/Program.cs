using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        //[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
            //System.Net.NetworkInformation.NetworkInterface[] networks = 
            //    System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //Console.WriteLine("ID:" + networks[0].Id + "\n" +
            //    "Description:" + networks[0].Description + "\n" +
            //    "Name:" + networks[0].Name + "\n" +
            //    "PhysicalAddress: " + networks[0].GetPhysicalAddress());
        }
    }
}
