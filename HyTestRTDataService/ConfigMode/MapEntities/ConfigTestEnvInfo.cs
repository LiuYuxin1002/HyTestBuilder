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
        /*当前协议*/
        public Protocol currentProtocol;

        /*控件刷新频率*/
        public int refreshFrequency;

        /// <summary>
        /// 驱动采样频率us
        /// </summary>
        public int driveRefreshFrequency;

        /// <summary>
        /// redis写入频率ms
        /// </summary>
        public int redisRefreshFrequency;

        /*最大设备连接数*/
        public int maxDeviceNum;

        public ConfigTestEnvInfo()
        {

        }
    }
}
