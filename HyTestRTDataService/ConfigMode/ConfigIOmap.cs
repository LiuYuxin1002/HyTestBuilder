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
        public SerializableDictionary<string, string> mapPortToName;
        public SerializableDictionary<string, string> mapNameToPort;
        public SerializableDictionary<int, string> mapIndexToName;
        public SerializableDictionary<string, int> mapNameToIndex;
        public SerializableDictionary<string, string> mapNameToType;
        public DataTable ioMapTable;
        public int inputVarNum, outputVarNum;

        public ConfigIOmap(ConfigIOmapInfo iomapInfo)
        {

        }

        //从xml获取IOmapTable
        private void initIOmapTable()
        {

        }

        public DataTable getIOmapFromExcel()
        {
            this.ioMapTable = ExcelHelper.SelectExcelToDataTable();
            return this.ioMapTable;
        }

        //public的初始化方法
        public void initIOmap()
        {
            if (this.ioMapTable == null)
            {
                initIOmapTable();
            }
            //将table的值用来初始化map
            foreach (DataRow dr in this.ioMapTable.Rows)
            {

            }

        }

        internal void saveIOmapToExcel()
        {
            ExcelHelper.DataTableToExcel(this.ioMapTable);
            //throw new NotImplementedException();
        }

        internal static void SetIOmapInfo(DataTable iomapTable)
        {
            //重新映射，通过IOmapTable
        }
    }
}
