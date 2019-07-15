using HyTestRTDataService.ConfigMode.MapEntities;
using System;

namespace HyTestRTDataService.ConfigMode
{
    [Serializable]
    public class Config
    {
        /// <summary>
        /// adapter infomation (nullable)
        /// </summary>
        public ConfigAdapterInfo adapterInfo;

        /// <summary>
        /// device infomation read from driver
        /// </summary>
        public ConfigDeviceInfo deviceInfo;

        /// <summary>
        /// iomap infomation config from excel/custom
        /// </summary>
        public ConfigIOmapInfo iomapInfo;

        /// <summary>
        /// test environment info such as some refresh_interval
        /// </summary>
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
