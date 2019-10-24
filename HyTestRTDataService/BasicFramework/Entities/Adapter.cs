using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace HyTestRTDataService.Entities
{
    [Serializable]
    [XmlRoot("Adapter")]
    public class Adapter
    {
        private string name;
        private string desc;
        private string state;
        private int isSelected;

        public string Name { get => name; set => name = value; }
        public string Desc { get => desc; set => desc = value; }
        public string State { get => state; set => state = value; }
        public int IsSelected { get => isSelected; set => isSelected = value; }

        public Adapter() { }

    }
}
