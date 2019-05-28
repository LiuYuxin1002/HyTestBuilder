using System;

namespace IndustrialEthernetEntity
{
    public struct IOdevice
    {
        public int id;
        public int type;//Di,DO,AI,AO
        public string name;
        public int channelNum;
        public Type[] value;
        public IOdevice(int id, int type, string name, int channelNum):this()
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.channelNum = channelNum;
        }
    }
}
