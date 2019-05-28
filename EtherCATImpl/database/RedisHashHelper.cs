using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherCATImpl.database
{
    public class RedisHashHelper
    {
        private const int KEY = 1;
        private const int VALUE = 2;
        static RedisHashHelper() { }

        /// <summary>
        /// 为key对应的hash增加一个键值对
        /// </summary>
        public static void AddKeyValue(RedisClient client, string key, string field, string value)
        {
            long output = client.HSet(key, stringToBytes(field), stringToBytes(value));
        }
        /// <summary>
        /// 批量增加键值对
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="dicToAdd"></param>
        public static void AddKeyValues(RedisClient client, string key, Dictionary<string, string> dicToAdd)
        {
            client.HMSet(key, stringToBytesArr(dicToAdd, KEY), stringToBytesArr(dicToAdd, VALUE));
        }
        /// <summary>
        /// 获取单个键值对
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetKeyValue(RedisClient client, string key, string field)
        {
            byte[] strbytes = client.HGet(stringToBytes(key), stringToBytes(field));
            return bytesToString(strbytes);
        }
        /// <summary>
        /// 获取key对应的全部键值对
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetKeyValues(RedisClient client, string key)
        {
            byte[][] fields = client.HKeys(key);//可能比较慢，算法要改进
            byte[][] values = client.HMGet(key, fields);
            return BytesArrToDic(fields, values);
        }
        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        public static void DeleteKeyValues(RedisClient client, string key, string field)
        {
            long output = client.HDel(key, stringToBytes(field));
        }
        /// <summary>
        /// 删除key对应的全部hash
        /// </summary>
        /// <param name="client"></param>
        /// <param name="key"></param>
        public static void DeleteAllHash(RedisClient client, string key)
        {
            client.Del(key);
        }

        #region utils
        
        private static byte[] stringToBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        private static byte[][] stringToBytesArr(Dictionary<string, string> dic, int keyorvalue)
        {
            int i = 0;
            string[] needToTransStr = new string[dic.Count];
            foreach(KeyValuePair<string, string> kv in dic)//获取要转换的字符串
            {
                if (keyorvalue == KEY)
                {
                    needToTransStr[i] = kv.Key;
                }
                else
                {
                    needToTransStr[i] = kv.Value;
                }
                i++;
            }

            byte[][] result = new byte[dic.Count][];
            for(i=0; i<dic.Count; i++)
            {
                result[i] = Encoding.UTF8.GetBytes(needToTransStr[i]);
            }
            return result;
        }

        private static string bytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        //未实现
        private static Dictionary<string, string> BytesArrToDic(byte[][] keys, byte[][] values)
        {
            string key, value;
            Dictionary<string, string> resultDic = new Dictionary<string, string>();
            for(int i=0; i<keys.Length; i++)
            {
                key = Encoding.UTF8.GetString(keys[i]);
                value = Encoding.UTF8.GetString(values[i]);
                resultDic.Add(key, value);
            }
            return resultDic;
        }
        #endregion
    }
}
