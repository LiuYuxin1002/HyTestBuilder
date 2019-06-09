using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.ConfigMode
{
    /// <summary>
    /// 通过datatable初始化map
    /// </summary>
    class ConfigIOmap
    {
        private Dictionary<Port, string> mapPortToName;
        private Dictionary<string, Port> mapNameToPort;

        private DataTable IOmapTable;
        
        //从xml获取IOmapTable
        private void initIOmapTable()
        {

        }

        //public的初始化方法
        public void initIOmap()
        {
            if (IOmapTable == null)
            {
                initIOmapTable();
            }
            //将table的值用来初始化map
            foreach (DataRow dr in IOmapTable.Rows)
            {

            }

        }

    }
}
