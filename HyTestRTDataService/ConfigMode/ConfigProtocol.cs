using HyTestRTDataService.ConfigMode.MapEntities;
using System.Collections.Generic;
using HyTestIEInterface;
using HyTestEtherCAT;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigProtocol
    {
        public static int currentProtocol;
        public IList<Protocol> protocolList;

        public ConfigProtocol()
        {
            protocolList = new List<Protocol>
            {
                new Protocol("EtherCAT"),
                new Protocol("NULL"),
            };
        }

        public static IReader getReader()
        {
            IReader reader;
            switch (currentProtocol)
            {
                case 0:
                    reader = EtherCAT.getEtherCAT();
                    break;
                default:
                    reader = null;
                    break;
            }
            return reader;
        }

        public static IWriter getWriter()
        {
            IWriter writer;
            switch (currentProtocol)
            {
                case 0:
                    writer = EtherCAT.getEtherCAT();
                    break;
                default:
                    writer = null;
                    break;
            }
            return writer;
        }
    }
}
