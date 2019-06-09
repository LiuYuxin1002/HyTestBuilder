using HyTestIEEntity;
using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode
{
    [Serializable]
    public class Config
    {
        private static Config config;
        public static Config getConfig()
        {
            if (config == null)
            {
                config = new Config();
            }
            return config;
        }

        //adapter config
        public DataTable adapterTable;
        public Adapter currentAdapter;
        public string currentName;
        public string currentDesc;
        public int adapterNum;

        //device config
        public IOdevice[] deviceArray;
        public int deviceNum;

        //数据绑定
        public SerializableDictionary<Port, string> mapPortToName;
        public SerializableDictionary<string, Port> mapNameToPort;

        

        private Config()
        {

        }
    }
}
