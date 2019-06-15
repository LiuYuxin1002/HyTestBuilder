using HyTestRTDataService.ConfigMode.MapEntities;
using System.Data;
using HyTestRTDataService.Utils;
using System;

namespace HyTestRTDataService.ConfigMode
{
    /// <summary>
    /// 通过datatable初始化map
    /// </summary>
    public class ConfigIOmap
    {
        public ConfigIOmapInfo iomapInfo;

        //public SerializableDictionary<string, string> mapPortToName;
        //public SerializableDictionary<string, string> mapNameToPort;
        //public SerializableDictionary<int, string> mapIndexToName;
        //public SerializableDictionary<string, int> mapNameToIndex;
        //public SerializableDictionary<string, string> mapNameToType;
        //public DataTable ioMapTable;
        //public int inputVarNum, outputVarNum;

        public ConfigIOmap(ConfigIOmapInfo iomapInfo)
        {
            this.iomapInfo = iomapInfo;
        }

        //从xml获取IOmapTable
        private void initIOmapTable()
        {

        }

        public DataTable getIOmapFromExcel()
        {
            iomapInfo.ioMapTable = ExcelHelper.SelectExcelToDataTable();
            return iomapInfo.ioMapTable;
        }

        //public的初始化方法
        public void initIOmap()
        {
            if (iomapInfo.ioMapTable == null)
            {
                initIOmapTable();
            }
            //将table的值用来初始化map
            foreach (DataRow dr in iomapInfo.ioMapTable.Rows)
            {

            }

        }

        internal void saveIOmapToExcel()
        {
            ExcelHelper.DataTableToExcel(iomapInfo.ioMapTable);
            //throw new NotImplementedException();
        }

        internal static void SetIOmapInfo(DataTable iomapTable)
        {
            //重新映射，通过IOmapTable
        }
    }
}
