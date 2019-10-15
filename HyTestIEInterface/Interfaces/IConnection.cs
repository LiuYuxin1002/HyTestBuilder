using System;

namespace HyTestIEInterface
{
    public interface IConnection
    {
        OperationResult Connect();
        OperationResult Close();
        OperationResult Disconnect();
    }
}