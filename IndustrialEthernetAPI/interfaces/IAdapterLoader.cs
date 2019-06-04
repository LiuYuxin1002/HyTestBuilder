using HyTestIEEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IAdapterLoader
    {
        Adapter[] getAdapter();
        ErrorCode setAdapter(int id);
    }
}
