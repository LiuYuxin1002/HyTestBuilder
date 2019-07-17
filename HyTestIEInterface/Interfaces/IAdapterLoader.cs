using HyTestIEInterface.Entity;

namespace HyTestIEInterface
{
    public interface IAdapterLoader
    {
        int AdapterSelected { get; set; }
        Adapter[] GetAdapter();
        int SetAdapter(int id);
    }
}
