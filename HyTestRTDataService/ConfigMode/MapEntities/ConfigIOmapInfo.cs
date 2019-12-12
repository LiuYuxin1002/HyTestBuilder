using HyTestRTDataService.Entities;
using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigIOmapInfo
    {
        /*Dictionary*/
        private SerializableDictionary<string, string>  mapPortToName;    //port->name
        private SerializableDictionary<string, string>  mapNameToPort;    //name->port
        private SerializableDictionary<int, string>     mapIndexToName;   //index->name
        private SerializableDictionary<string, int>     mapNameToIndex;   //name->index
        private SerializableDictionary<string, string>  mapNameToType;    //name->type
        private SerializableDictionary<string, int>     mapNameToMax;     //name->max
        private SerializableDictionary<string, int>     mapNameToMin;     //name->min

        /*Table*/
        private DataTable ioMapTable;

        /*num of input & output*/
        private int outputVarNum;
        private int inputVarNum;

        private string fileName;

        public SerializableDictionary<string, string> MapPortToName
        {
            get
            {
                return mapPortToName;
            }

            set
            {
                mapPortToName = value;
            }
        }
        public SerializableDictionary<string, string> MapNameToPort
        {
            get
            {
                return mapNameToPort;
            }

            set
            {
                mapNameToPort = value;
            }
        }
        public SerializableDictionary<int, string> MapIndexToName
        {
            get
            {
                return mapIndexToName;
            }

            set
            {
                mapIndexToName = value;
            }
        }
        public SerializableDictionary<string, int> MapNameToIndex
        {
            get
            {
                return mapNameToIndex;
            }

            set
            {
                mapNameToIndex = value;
            }
        }
        public SerializableDictionary<string, string> MapNameToType
        {
            get
            {
                return mapNameToType;
            }

            set
            {
                mapNameToType = value;
            }
        }
        public SerializableDictionary<string, int> MapNameToMax
        {
            get
            {
                return mapNameToMax;
            }

            set
            {
                mapNameToMax = value;
            }
        }
        public SerializableDictionary<string, int> MapNameToMin
        {
            get
            {
                return mapNameToMin;
            }

            set
            {
                mapNameToMin = value;
            }
        }
        public DataTable IoMapTable
        {
            get
            {
                return ioMapTable;
            }

            set
            {
                ioMapTable = value;
            }
        }
        public int InputVarNum
        {
            get
            {
                return inputVarNum;
            }

            set
            {
                inputVarNum = value;
            }
        }
        public int OutputVarNum
        {
            get
            {
                return outputVarNum;
            }

            set
            {
                outputVarNum = value;
            }
        }
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public ConfigIOmapInfo()
        {
            MapPortToName = new SerializableDictionary<string, string>();
            MapNameToPort = new SerializableDictionary<string, string>();
            MapIndexToName = new SerializableDictionary<int, string>();
            MapNameToIndex = new SerializableDictionary<string, int>();
            MapNameToType = new SerializableDictionary<string, string>();
            MapNameToMax = new SerializableDictionary<string, int>();
            MapNameToMin = new SerializableDictionary<string, int>();
        }
    }
}
