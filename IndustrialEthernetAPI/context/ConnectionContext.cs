using IndustrialEthernetEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialEthernetAPI
{
    public class ConnectionContext
    {
        public static IOdevice[] devices { get; set; }
        public static Adapter[] adapters { get; set; }
        public static bool haveAdapter { get; set; }
        public static int deviceNum { get; set; }
        public static int inputDeviceNum { get; set; }
        public static int outputDeviceNum { get; set; }
        public static int adapterNum { get; set; }
    }
}
