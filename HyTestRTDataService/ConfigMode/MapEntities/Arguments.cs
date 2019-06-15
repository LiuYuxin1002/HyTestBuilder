using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    public static class Arguments   ///无法实例化和被继承，成员被全局共享
    {
        static Arguments()
        {
            plcTypeServerName.Add("S7-200", "s7200.OPCServer");
            plcTypeServerName.Add("S7-200SMART", "s7200SMART.OPCServer");
            plcTypeServerName.Add("S7-300", "OPC.SimaticNet");
            plcTypeServerName.Add("S7-400", "OPC.SimaticNet");

            plcItemIDForServer.Add("S7-200", "MicroWin");
            plcItemIDForServer.Add("S7-200SMART", "MWSMART");
        }
        public static Dictionary<string, string> plcTypeServerName = new Dictionary<string, string>();
        public static Dictionary<string, string> plcItemIDForServer = new Dictionary<string, string>();
        internal enum DataType : short
        {
            Integer = 2,
            Long = 3,
            Float = 4,
            Double = 5,
            String = 8,
            Boolean = 11,
            Decimal = 14,
            Byte = 17
        }

        public enum PlcDataType : short
        {
            DI = 0,
            DO = 1,
            AI = 2,
            AO = 3,
            Vb = 4,
            VB = 5,
            VW = 6,
            VD = 7,
            Mb = 8,
            MB = 9,
            MW = 10,
            MD = 11,
            VF = 12          ///
        }

        public static System.Type GetSystemType(PlcDataType plcType)
        {
            switch (plcType)
            {
                case PlcDataType.DI:
                case PlcDataType.DO:
                case PlcDataType.Vb:
                case PlcDataType.Mb:
                    return typeof(Boolean);
                case PlcDataType.AI:
                case PlcDataType.AO:
                    return typeof(Double);
                case PlcDataType.VB:
                case PlcDataType.MB:
                    return typeof(Byte);
                case PlcDataType.VW:
                case PlcDataType.MW:
                    return typeof(Int16);
                case PlcDataType.VD:
                case PlcDataType.MD:
                    return typeof(Int32);
                case PlcDataType.VF:
                    return typeof(float);
                default:
                    return typeof(Object);
            }
        }
    }
}
