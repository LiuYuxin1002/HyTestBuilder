using HyTestIEEntity;
using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Data;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode
{
    [Serializable]
    public class Config
    {
        //adapter config
        public DataTable adapterTable;
        public Adapter currentAdapter;
        public int adapterNum;

        //device config
        public DataTable deviceTable;
        public int deviceNum;
        public IOdevice[] deviceArr;
       // public TreeNode rootNode;

        //iomap config
        public SerializableDictionary<string, string> mapPortToName;
        public SerializableDictionary<string, string> mapNameToPort;
        public SerializableDictionary<int, string> mapIndexToName;
        public SerializableDictionary<string, int> mapNameToIndex;
        public SerializableDictionary<string, string> mapNameToType;
        public DataTable ioMapTable;
        public int inputVarNum, outputVarNum;

        //协议配置
        public Protocol currentProtocol;

        //环境配置
        public int refreshFrequency;

        public Config()
        {
        }
    }
}
