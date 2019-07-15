using HyTestIEInterface.Entity;

namespace HyTestIEInterface
{
    public interface IAdapterLoader
    {
        int AdapterSelected { get; set; }
        Adapter[] getAdapter();
        int setAdapter(int id);
    }
}
