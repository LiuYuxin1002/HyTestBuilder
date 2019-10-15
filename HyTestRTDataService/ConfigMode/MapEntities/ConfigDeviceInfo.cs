using HyTestIEInterface.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigDeviceInfo
    {
        //有几个耦合器就有几个group
        public int groupNum;
        /*total number of devices*/
        public int deviceNum;
        /*used for buffer initialize*/
        public int allChannelCount;
        /// <summary>
        /// 设备列表，管理info的对象需要提供各种呈现方式（树，表，数组等）
        /// </summary>
        public List<List<IOdevice>> deviceList;

        /// <summary>
        /// There must be a Constructor
        /// </summary>
        public ConfigDeviceInfo()
        {
            
        }

    }
}
