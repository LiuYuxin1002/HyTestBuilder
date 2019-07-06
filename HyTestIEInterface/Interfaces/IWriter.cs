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
        /// <returns></returns>
        int WriteAnalog(int deviceId, int channel, int value);
        /// <summary>
        /// 单点写数字量
        /// </summary>
        /// <returns></returns>
        int WriteDigital(int deviceId, int channel, byte value);
        /// <summary>
        /// 批量写模拟量
        /// </summary>
        /// <returns></returns>
        int WriteAnalog(List<int> deviceList, List<int[]> channelList, List<int[]> values);
        /// <summary>
        /// 批量写数字量
        /// </summary>
        /// <returns></returns>
        int WriteDigital(List<int> deviceList, List<int[]> channelList, List<byte[]> values);
    }
}
