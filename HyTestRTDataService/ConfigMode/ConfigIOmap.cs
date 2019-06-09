using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestRTDataService.Utils;

namespace HyTestRTDataService.ConfigMode
{
    /// <summary>
    /// 通过datatable初始化map
    /// </summary>
    class ConfigIOmap
    {
        private Config config = Config.getConfig();
        
        //从xml获取IOmapTable
        private void initIOmapTable()
        {

        }

        public DataTable getIOmapFromExcel()
        {
            config.ioMapTable = ExcelHelper.SelectExcelToDataTable();
            return config.ioMapTable;
        }

        //public的初始化方法
        public void initIOmap()
        {
            if (config.ioMapTable == null)
            {
                initIOmapTable();
            }
            //将table的值用来初始化map
            foreach (DataRow dr in config.ioMapTable.Rows)
            {

            }

        }

        internal void saveIOmapToExcel()
        {
            ExcelHelper.DataTableToExcel(config.ioMapTable);
            throw new NotImplementedException();
        }
    }
}
