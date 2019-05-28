using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialEthernetAPI
{
    public enum ErrorCode
    {
        NO_ERROR,
        ADAPTER_NOT_FOUND = 0x1,
        ADAPTER_SELECT_FAIL = 0x2,
        NO_SLAVE_CONNECTED = 0x3,
    }

    public enum ExceptionCode
    {

    }
}
