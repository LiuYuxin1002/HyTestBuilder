using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyTestRTDataService.Entities
{
    [Serializable]
    public class IOdevice
    {
        public int id;
        public DeviceType type;
        public string name;
        public int channelNum;

        public IOdevice() { }

        public IOdevice(int id, DeviceType type, string name, int channelNum)
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.channelNum = channelNum;
        }
    }

    [Serializable]
    public enum DeviceType
    {
        NULL,
        DI = 1,
        DO,
        AI,
        AO,
        COUPLER = 10,
        SOLVER = 20,
    }
}
