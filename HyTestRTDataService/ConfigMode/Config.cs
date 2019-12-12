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

        public ConfigAdapterInfo AdapterInfo
        {
            get
            {
                return adapterInfo;
            }

            set
            {
                adapterInfo = value;
            }
        }
        public ConfigDeviceInfo DeviceInfo
        {
            get
            {
                return deviceInfo;
            }

            set
            {
                deviceInfo = value;
            }
        }
        public ConfigIOmapInfo IomapInfo
        {
            get
            {
                return iomapInfo;
            }

            set
            {
                iomapInfo = value;
            }
        }
        public ConfigTestEnvInfo TestInfo
        {
            get
            {
                return testInfo;
            }

            set
            {
                testInfo = value;
            }
        }

        public Config()
        {
            AdapterInfo = new ConfigAdapterInfo();
            DeviceInfo = new ConfigDeviceInfo();
            IomapInfo = new ConfigIOmapInfo();
            TestInfo = new ConfigTestEnvInfo();
        }
    }
}
