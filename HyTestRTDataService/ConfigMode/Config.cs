using HyTestIEEntity;
using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Data;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode
{
    [Serializable]
    public class Config
    {
        public ConfigAdapterInfo adapterInfo;
        public ConfigDeviceInfo deviceInfo;
        public ConfigIOmapInfo iomapInfo;
        public ConfigTestEnvInfo testInfo;

        public Config()
        {
            adapterInfo = new ConfigAdapterInfo();
            deviceInfo = new ConfigDeviceInfo();
            iomapInfo = new ConfigIOmapInfo();
            testInfo = new ConfigTestEnvInfo();
        }
    }
}
