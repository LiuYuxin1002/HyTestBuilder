using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyTestIEInterface.Entity
{
    [Serializable]
    [XmlRoot("Adapter")]
    public class Adapter
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int isSelected { get; set; }
    }
}
