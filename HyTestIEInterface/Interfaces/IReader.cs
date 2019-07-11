using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IReader
    {
        event EventHandler<EventArgs> myevent;

        int SetAdapterFromConfig(int AdapterId);

        /// <summary>
        /// 数据刷新时调用myevent
        /// </summary>
        void OnDataRefresh(object sender, EventArgs e);

        /// <summary>
        /// 单点读模拟量
        /// </summary>
        int ReadAnalog(int deviceId, int channel);
        
        /// <summary>
        /// 单点读数字量
        /// </summary>
        bool ReadDigital(int deviceId, int channel);
        
        /// <summary>
        /// 批量读模拟量
        /// </summary>
        int ReadAnalog(List<int> deviceList, List<int[]> channelList, ref List<int[]> values);
        
        /// <summary>
        /// 批量读数字量
        /// </summary>
        int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<byte[]> values);
    }
}
