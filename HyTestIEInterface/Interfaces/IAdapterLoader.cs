using HyTestIEInterface.Entity;

namespace HyTestIEInterface
{
    public interface IAdapterLoader
    {
        int AdapterSelected { get; set; }

        /// <summary>
        /// 获取网卡列表
        /// </summary>
        /// <returns></returns>
        Adapter[] GetAdapter();

        /// <summary>
        /// 选定网卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns>成功返回1，失败返回0</returns>
        int SetAdapter(int id);
    }
}
