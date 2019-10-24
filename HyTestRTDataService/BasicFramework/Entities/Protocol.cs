using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.Entities
{
    [Serializable]
    public class Protocol
    {
        public string name;
        public Protocol() { }
        public Protocol(string name)
        {
            this.name = name;
        }
    }
}
