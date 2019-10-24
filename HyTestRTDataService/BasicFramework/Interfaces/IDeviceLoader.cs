using HyTestRTDataService.Entities;
using System.Collections.Generic;

namespace HyTestRTDataService.Interfaces
{
    public interface IDeviceLoader
    {
        List<List<IOdevice>> GetDevice();

        int GetDeviceNum();
    }
}
