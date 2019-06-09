using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyTestIEEntity
{
    [Serializable]
    public struct IOdevice : IXmlSerializable
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

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
