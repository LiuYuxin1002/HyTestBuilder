using HyTestIEEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IDeviceLoader
    {
        //IOdevice[] devices { get; set; }
        IOdevice[] getDevice();
    }
}
