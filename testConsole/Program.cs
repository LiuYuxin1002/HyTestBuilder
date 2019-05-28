using EtherCATImpl;
using IndustrialEthernetEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndustrialEthernetAPI;
using EtherCATImpl.localEntity;

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
            ErrorCode err = ethercat.setAdapter(0);
            /*从站读取测试部分*/
            SlaveInfo slave = new SlaveInfo();
            CppConnect.getSlaveInfo(ref slave, 0);
            Console.WriteLine(slave.id);
        }
    }
}
