using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    abstract class AbstractIEBConnector : IConnection
    {
        public abstract event EventHandler datachanged;
        private AbstractIEBConnector Connector;

        public ConnectionContext context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private AbstractIEBConnector()
        {

        }

        public abstract OperationResult Connect();
        public abstract OperationResult Close();
        public abstract OperationResult Disconnect();
    }
}
