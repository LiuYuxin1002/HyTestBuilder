using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestRTDataService.Utils;
using HyTestRTDataService.ConfigMode.Component;

namespace HyTestRTDataService.ConfigMode
{
    /// <summary>
    /// 通过datatable初始化map
    /// </summary>
    class ConfigIOmap
    {
        public SerializableDictionary<string, string> mapPortToName;
        public SerializableDictionary<string, string> mapNameToPort;
        public SerializableDictionary<int, string> mapIndexToName;
        public SerializableDictionary<string, int> mapNameToIndex;
        public SerializableDictionary<string, string> mapNameToType;
        public DataTable ioMapTable;
        public int inputVarNum, outputVarNum;
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
    }
}
