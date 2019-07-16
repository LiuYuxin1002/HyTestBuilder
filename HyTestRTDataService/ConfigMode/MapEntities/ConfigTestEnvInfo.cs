using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigTestEnvInfo
    {
        /// <summary>
        /// 当前协议
        /// </summary>
        public Protocol currentProtocol;

        /// <summary>
        /// 控件刷新频率
        /// </summary>
        public int refreshFrequency;

        /// <summary>
        /// 驱动采样频率us
        /// </summary>
        public int driveRefreshFrequency;

        /// <summary>
        /// redis写入频率ms
        /// </summary>
        public int redisRefreshFrequency;

        /// <summary>
        /// 最大设备连接数
        /// </summary>
        public int maxDeviceNum;

        public ConfigTestEnvInfo() { }
    }
}
