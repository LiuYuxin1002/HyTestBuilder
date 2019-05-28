using IndustrialEthernetEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialEthernetAPI
{
    public interface IDeviceLoader
    {
        //IOdevice[] devices { get; set; }
        IOdevice[] getDevice();
    }
}
