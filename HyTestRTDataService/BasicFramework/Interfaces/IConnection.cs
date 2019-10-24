using System;

namespace HyTestRTDataService.Interfaces
{
    public interface IConnection
    {
        OperationResult Connect();
        OperationResult Close();
        OperationResult Disconnect();
    }
}