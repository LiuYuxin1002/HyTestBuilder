using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Collections.Generic.Dictionary<string, string>;

namespace HyTestEtherCAT.database
{
    /// <summary>
    /// 数据反序列化
    /// 数据格式
    /// key = time, 
    /// </summary>
    public class DataDeserialization
    {


        static DataDeserialization() { }
        /// <summary>
        /// 将map转化为可以使用的dataframe
        /// </summary>
        public static Dataframe deserializedDic(Dictionary<string, string> dataDic)
        {
            int dicSize = dataDic.Count();

            Dataframe dataframe = new Dataframe(dicSize, new int[dicSize], new int[dicSize], new string[dicSize]);

            int i = 0;
            foreach (KeyValuePair<string, string> kv in dataDic)
            {
                dataframe.data[i] = int.Parse(kv.Value);
                string slaveAndChennel = kv.Key;
                string[] strs = slaveAndChennel.Split('_');
                dataframe.slaveName[i] = strs[0];
                dataframe.channel[i] = int.Parse(strs[1]);
                i++;
            }

            return dataframe;
        }

        //TODO: int的反序列化？
        public static void deserialInt(string slaveAndchannel, string data)
        {

        }
    }

    public class Dataframe
    {
        public int count;
        public int[] data;
        public int[] channel;
        public string[] slaveName;

        public Dataframe(int count, int[] data, int[] channel, string[] slaveName)
        {
            this.count = count;
            this.data = data;
            this.channel = channel;
            this.slaveName = slaveName;
        }

        public string[] toString()
        {
            string[] result = new string[this.count];
            for(int i=0; i<this.count; i++)
            {
                result[i] = slaveName[i] + "->" + channel[i] + " : " + data[i];
            }
            return result;
        }
    }
}
