using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
            //Subscribe();
        }

        /// <summary>
        /// Redis订阅
        /// </summary>
        public static void Subscribe()
        {
            using (RedisClient consumer = new RedisClient("127.0.0.1", 6379))
            {
                //创建订阅
                IRedisSubscription subscription = consumer.CreateSubscription();
                //接受到消息时
                subscription.OnMessage = (channel, msg) =>
                {
                    Console.WriteLine($"从频道：{channel}上接受到消息：{msg},时间：{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}");
                    Console.WriteLine($"频道订阅数目：{subscription.SubscriptionCount}");
                    Console.WriteLine("___________________________________________________________________");
                };
                //订阅频道时
                subscription.OnSubscribe = (channel) =>
                {
                    Console.WriteLine("订阅客户端：开始订阅" + channel);
                };
                //取消订阅频道时
                subscription.OnUnSubscribe = (a) => { Console.WriteLine("订阅客户端：取消订阅"); };

                //订阅频道
                subscription.SubscribeToChannels("channel1");
            }
        }
    }
}
