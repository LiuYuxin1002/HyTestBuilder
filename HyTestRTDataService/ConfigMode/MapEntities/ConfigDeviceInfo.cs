using HyTestRTDataService.Entities;
using System;
using System.Collections.Generic;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigDeviceInfo
    {
        //有几个耦合器就有几个group
        private int groupNum;
        /*total number of devices*/
        private int deviceNum;
        /*used for buffer initialize*/
        private int allChannelCount;
        //设备列表，管理info的对象需要提供各种呈现方式（树，表，数组等）
        private List<List<IOdevice>> deviceList;

        public int GroupNum
        {
            get
            {
                return groupNum;
            }

            set
            {
                groupNum = value;
            }
        }
        public int DeviceNum
        {
            get
            {
                return deviceNum;
            }

            set
            {
                deviceNum = value;
            }
        }
        public int AllChannelCount
        {
            get
            {
                return allChannelCount;
            }

            set
            {
                allChannelCount = value;
            }
        }
        public List<List<IOdevice>> DeviceList
        {
            get
            {
                return deviceList;
            }

            set
            {
                deviceList = value;
            }
        }

        public ConfigDeviceInfo() { }
    }
}
