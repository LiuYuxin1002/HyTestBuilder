using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialEthernetAPI
{
    public interface IReadWrite
    {
        /// <summary>
        /// 单点读模拟量
        /// </summary>
        /// <param name="deviceId">设备本地ID</param>
        /// <param name="channel">端口</param>
        /// <param name="value"></param>
        /// <returns></returns>
        int ReadAnalog(int deviceId, int channel, ref int value);
        /// <summary>
        /// 单点读数字量
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="channel"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int ReadDigital(int deviceId, int channel, ref bool value);
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
        int ReadDigital(List<int> deviceList, List<int[]> channelList, ref List<bool[]> values);
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
