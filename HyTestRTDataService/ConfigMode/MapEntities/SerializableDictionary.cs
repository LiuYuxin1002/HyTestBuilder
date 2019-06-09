using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public SerializableDictionary()
        {
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this)
            {
                writer.WriteStartElement("SerializableDictionary");
                writer.WriteStartElement("key");
                KeySerializer.Serialize(writer, kv.Key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                ValueSerializer.Serialize(writer, kv.Value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return;
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("SerializableDictionary");
                reader.ReadStartElement("key");
                TKey tk = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue tv = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(tk, tv);
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }
    }
}
