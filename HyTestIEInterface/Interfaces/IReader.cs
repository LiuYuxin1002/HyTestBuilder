using HyTestIEInterface.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestIEInterface
{
    public interface IReader
    {
        int SetAdapterFromConfig(int AdapterId);

        /// <summary>
        /// 单点读模拟量，对于伺服驱动器，需要按照配置表来选择channel值
        /// </summary>
        int ReadAnalog(int deviceId, int channel);
        
        /// <summary>
        /// 单点读数字量
        /// </summary>
        bool ReadBoolean(int deviceId, int channel);
        
    }

    public interface IRedisReader
    {
        string                     Ip      { get; set; }
        int                        Port    { get; set; }
        Dictionary<string, string> Buffer  { get; }

        event EventHandler<EventArgs> RedisDataChanged;

        void subjectRedis();
    }
}
