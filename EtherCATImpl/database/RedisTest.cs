using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherCATImpl.database
{
    class RedisTest
    {
        RedisClient client;
        public RedisTest()
        {
            client = new RedisClient("127.0.0.1",6379);

        }
    }
}
