using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyTestIEEntity
{
    [Serializable]
    public class IOdevice
    {
        public int id;
        public int type;//Di,DO,AI,AO
        public string name;
        public int channelNum;

        public IOdevice() { }

        public IOdevice(int id, int type, string name, int channelNum):this()
        {
            this.id = id;
            this.type = type;
            this.name = name;
            this.channelNum = channelNum;
        }
    }
}
