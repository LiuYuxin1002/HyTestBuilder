using HyTestEtherCAT;
using HyTestIEEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestEtherCAT.localEntity;
using System.Threading;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            EtherCAT ethercat = EtherCAT.getEtherCAT(true);
            /*网卡测试部分*/
            Adapter[] adapters = ethercat.getAdapter();
            //for(int i=0; i<adapters.Length; i++)
            //{
            //    Console.WriteLine(adapters[i].desc + "->" + adapters[i].name);
            //}
            ErrorCode err = ethercat.setAdapter(1);
            /*从站读取测试部分*/
            IOdevice[] slaveList = ethercat.getDevice();
            for(int i=0; i<slaveList.Length; i++)
            {
                Console.WriteLine(slaveList[i].id + "," + slaveList[i].name);
            }
            RedisTest redis = new RedisTest();
            redis.TestStart();
        }
    }

    class RedisTest
    {
        public RedisTest()
        {

        }

        public void TestStart()
        {
            int count = ImportClass.prepareToRead();

            ImportClass.readStart();
            Thread.Sleep(1000);
            ImportClass.readStop();
        }

    }
}
