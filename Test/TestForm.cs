using HyTestEtherCAT.database;
using log4net;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class TestForm : Form
    {
        ILog log = log4net.LogManager.GetLogger(typeof(TestForm));
        RedisClient client;
        IRedisSubscription subscription;
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (client = new RedisClient("127.0.0.1", 6379))
            {
                //创建订阅
                this.subscription = client.CreateSubscription();
                //接受到消息时
                subscription.OnMessage = (channel, msg) =>
                {
                    log.Info($"从频道：{channel}上接受到消息：{msg},时间：{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}");
                    log.Info($"频道订阅数目：{subscription.SubscriptionCount}");
                    log.Info("___________________________________________________________________");
                };
                //订阅频道时
                subscription.OnSubscribe = (channel) =>
                {
                    log.Info("订阅客户端：开始订阅" + channel);
                };
                //取消订阅频道时
                Thread t1 = new Thread(method);
                subscription.OnUnSubscribe = (a) => { log.Info("订阅客户端：取消订阅"); };

                //订阅频道
                t1.Start();
            }
        }

        public void method()
        {
            subscription.SubscribeToChannels("channel1");
        }
    }
}
