using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using System.Configuration;

namespace EtherCATImpl.database
{
    /// <summary>
    /// RedisClient管理类，可以获取Redis客户端。
    /// 由于Redis是单线程的，所以暂不考虑加锁，然后很多人用PooledRedisClientManager之后也可以考虑。
    /// </summary>
    public class RedisManager
    {
        private const string REDIS_HOST = "127.0.0.1";
        private const int REDIS_PORT = 6379;

        private static RedisClient client = null;

        public static RedisClient Client
        {
            get
            {
                if (client == null)
                {
                    return new RedisClient(REDIS_HOST, REDIS_PORT);
                }
                else return client;
            }
        }

        /// <summary>
        /// 之后可以处理一些配置
        /// </summary>
        static RedisManager()
        {
            //client = new RedisClient(REDIS_HOST, REDIS_PORT);
        }


    }
}
