using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using System.Configuration;
using System.Threading;

namespace HyTestEtherCAT.database
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
        public RedisManager()
        {
            
        }

        private void dataSubject()
        {
            IRedisSubscription subscription = client.CreateSubscription();
            //接收到消息时
            subscription.OnMessage = (channel, msg) =>
            {
                Console.WriteLine($"从频道：{channel}上接受到消息：{msg},时间：{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}");
                Console.WriteLine($"频道订阅数目：{subscription.SubscriptionCount}");
            };

            //订阅频道时
            subscription.OnSubscribe = (channel) =>
            {
                Console.WriteLine("订阅客户端：开始订阅" + channel);
            };

            subscription.OnUnSubscribe = (a) => { Console.WriteLine("订阅客户端：取消订阅"); };

            //订阅频道
            subscription.SubscribeToChannels("channel1");
        }

        public void subjectPublish()
        {
            Thread t1 = new Thread(dataSubject);
            t1.Start();
        }


    }
}
