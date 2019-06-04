using HyTestEtherCAT;
using HyTestIEEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestEtherCAT.localEntity;

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
            Console.WriteLine(slave.id+","+slave.name);
            Console.WriteLine(StringToBinary(slave.name));
        }

        public static string StringToBinary(string ss)
        {
            byte[] u = Encoding.Unicode.GetBytes(ss);
            string result = string.Empty;
            foreach (byte a in u)
            {
                result += a.ToString();
            }
            return result;

        }
    }
}
