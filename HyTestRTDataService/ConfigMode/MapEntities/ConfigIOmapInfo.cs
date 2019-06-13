using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigIOmapInfo
    {
        public SerializableDictionary<string, string> mapPortToName;
        public SerializableDictionary<string, string> mapNameToPort;
        public SerializableDictionary<int, string> mapIndexToName;
        public SerializableDictionary<string, int> mapNameToIndex;
        public SerializableDictionary<string, string> mapNameToType;
        public DataTable ioMapTable;
        public int inputVarNum, outputVarNum;
    }
}
