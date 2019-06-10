using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IWriter
    {
        /// <summary>
        /// 单点写模拟量
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int WriteAnalog(int deviceId, int channel, int value);
        /// <summary>
        /// 单点写数字量
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int WriteDigital(int deviceId, int channel, bool value);
        /// <summary>
        /// 批量写模拟量
        /// </summary>
        /// <param name="deviceList"></param>
        /// <param name="channelList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int WriteAnalog(List<int> deviceList, List<int[]> channelList, List<int[]> values);
        /// <summary>
        /// 批量写数字量
        /// </summary>
        /// <param name="deviceList"></param>
        /// <param name="channelList"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        int WriteDigital(List<int> deviceList, List<int[]> channelList, List<bool[]> values);
    }
}
