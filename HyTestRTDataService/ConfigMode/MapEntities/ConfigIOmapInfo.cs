using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigIOmapInfo
    {
        /*Dictionary*/
        public SerializableDictionary<string, string> mapPortToName;    //port->name
        public SerializableDictionary<string, string> mapNameToPort;    //name->port
        public SerializableDictionary<int, string>    mapIndexToName;   //index->name
        public SerializableDictionary<string, int>    mapNameToIndex;   //name->index
        public SerializableDictionary<string, string> mapNameToType;    //name->type
        public SerializableDictionary<string, int>    mapNameToMax;     //name->max
        public SerializableDictionary<string, int>    mapNameToMin;     //name->min

        /*Table*/
        public DataTable ioMapTable;

        /*num of input & output*/
        public int inputVarNum, outputVarNum;

        public ConfigIOmapInfo()
        {
            mapPortToName = new SerializableDictionary<string, string>();
            mapNameToPort = new SerializableDictionary<string, string>();
            mapIndexToName = new SerializableDictionary<int, string>();
            mapNameToIndex = new SerializableDictionary<string, int>();
            mapNameToType = new SerializableDictionary<string, string>();
            mapNameToMax = new SerializableDictionary<string, int>();
            mapNameToMin = new SerializableDictionary<string, int>();
        }
    }
}
