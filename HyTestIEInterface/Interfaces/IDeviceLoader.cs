using HyTestIEInterface.Entity;
using System.Collections.Generic;

namespace HyTestIEInterface
{
    public interface IDeviceLoader
    {
        List<List<IOdevice>> GetDevice();

        int GetDeviceNum();
    }
}
