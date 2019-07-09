using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IReader
    {
        /// <summary>
        /// 单点读模拟量
        /// </summary>
        /// <param name="deviceId">设备本地ID</param>
        /// <param name="channel">端口</param>
        /// <param name="value"></param>
        /// <returns></returns>
        int ReadAnalog(int deviceId, int channel);
        
        /// <summary>
        /// 单点读数字量
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool ReadDigital(int deviceId, int channel);
        
        /// <summary>
        /// 批量读模拟量
        /// </summary>
        /// <param name="deviceList"></param>
        /// <param name="channelList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int ReadAnalog(List<int> deviceList, List<int[]> channelList, ref List<int[]> values);
        
        /// <summary>
        /// 批量读数字量
        /// </summary>
        /// <param name="deviceList"></param>
        /// <param name="channelList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<byte[]> values);
    }
}
