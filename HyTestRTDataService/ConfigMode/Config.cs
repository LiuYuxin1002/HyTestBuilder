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
        private ConfigAdapterInfo adapterInfo;

        /// <summary>
        /// device infomation read from driver
        /// </summary>
        private ConfigDeviceInfo deviceInfo;

        /// <summary>
        /// iomap infomation config from excel/custom
        /// </summary>
        private ConfigIOmapInfo iomapInfo;

        /// <summary>
        /// test environment info such as some refresh_interval
        /// </summary>
        private ConfigTestEnvInfo testInfo;

        public ConfigAdapterInfo AdapterInfo { get => adapterInfo; set => adapterInfo = value; }
        public ConfigDeviceInfo DeviceInfo { get => deviceInfo; set => deviceInfo = value; }
        public ConfigIOmapInfo IomapInfo { get => iomapInfo; set => iomapInfo = value; }
        public ConfigTestEnvInfo TestInfo { get => testInfo; set => testInfo = value; }

        public Config()
        {
            AdapterInfo = new ConfigAdapterInfo();
            DeviceInfo = new ConfigDeviceInfo();
            IomapInfo = new ConfigIOmapInfo();
            TestInfo = new ConfigTestEnvInfo();
        }
    }
}
