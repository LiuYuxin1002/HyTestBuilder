using HyTestRTDataService.Entities;
using System;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    /*
     * Usage:
     * You should initialize this class first when running server. Then write these info of this class
     * to the connector.
     * 
     */
    [Serializable]
    public class ConfigTestEnvInfo
    {
        /*Base Setting*/
        private int baseReadFrequency;          //基础采样频率
        private int baseWriteFrequency;         //基础写入频率
        private int userControlFreshFrequency;  //用户控件刷新频率
        private int resolution;                 //传感器分辨率（滤波）
        private int bufferSize;                 //运行时缓存
        /*Redis*/
        private string redisIp;
        private int redisPort;

        public ConfigTestEnvInfo() { }

        public int BaseReadFrequency
        {
            get
            {
                return baseReadFrequency;
            }

            set
            {
                baseReadFrequency = value;
            }
        }

        public int BaseWriteFrequency
        {
            get
            {
                return baseWriteFrequency;
            }

            set
            {
                baseWriteFrequency = value;
            }
        }

        public int UserControlFreshFrequency
        {
            get
            {
                return userControlFreshFrequency;
            }

            set
            {
                userControlFreshFrequency = value;
            }
        }

        public int Resolution
        {
            get
            {
                return resolution;
            }

            set
            {
                resolution = value;
            }
        }

        public int BufferSize
        {
            get
            {
                return bufferSize;
            }

            set
            {
                bufferSize = value;
            }
        }

        public string RedisIp
        {
            get
            {
                return redisIp;
            }

            set
            {
                redisIp = value;
            }
        }

        public int RedisPort
        {
            get
            {
                return redisPort;
            }

            set
            {
                redisPort = value;
            }
        }
    }
}
