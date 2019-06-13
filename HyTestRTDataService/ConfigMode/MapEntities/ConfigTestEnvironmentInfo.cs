using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigTestEnvironmentInfo
    {
        public Protocol currentProtocol;
        public int refreshFrequency;
    }
}
