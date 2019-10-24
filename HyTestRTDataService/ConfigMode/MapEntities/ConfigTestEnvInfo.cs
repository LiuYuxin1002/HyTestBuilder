using HyTestRTDataService.Entities;
using System;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigTestEnvInfo
    {
        /// <summary>
        /// 当前协议
        /// </summary>
        private Protocol currentProtocol;

        /// <summary>
        /// 控件刷新频率
        /// </summary>
        private int refreshFrequency;

        /// <summary>
        /// 驱动采样频率us
        /// </summary>
        private int driveRefreshFrequency;

        /// <summary>
        /// redis写入频率ms
        /// </summary>
        private int redisRefreshFrequency;

        /// <summary>
        /// 最大设备连接数
        /// </summary>
        private int maxDeviceNum;

        public Protocol CurrentProtocol { get => currentProtocol; set => currentProtocol = value; }
        public int RefreshFrequency { get => refreshFrequency; set => refreshFrequency = value; }
        public int DriveRefreshFrequency { get => driveRefreshFrequency; set => driveRefreshFrequency = value; }
        public int RedisRefreshFrequency { get => redisRefreshFrequency; set => redisRefreshFrequency = value; }
        public int MaxDeviceNum { get => maxDeviceNum; set => maxDeviceNum = value; }

        public ConfigTestEnvInfo() { }
    }
}
