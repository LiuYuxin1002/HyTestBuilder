using HyTestRTDataService.ConfigMode.MapEntities;
using System.Data;
using HyTestRTDataService.Utils;
using System;

namespace HyTestRTDataService.ConfigMode
{
    /* 最主要的数据： 
     * iomapTable[]     Excel直接获取到的，主要展示手段
     * Maps             table转化而来，主要计算工具
     * ioNum            table计算得到，主要计算辅助
     */
    public class ConfigIOmap : IConfigBase
    {
        private ConfigIOmapInfo iomapInfo;

        public ConfigIOmap(ConfigIOmapInfo iomapInfo)
        {
            ReadSubConfig(iomapInfo);
        }

        //从xml获取IOmapTable
        private void initIOmapTable()
        {
            DataTable iomapTable = new DataTable();
            iomapTable.TableName = "IOmapTable";
            DataColumn colId = iomapTable.Columns.Add("ID", typeof(int));
            DataColumn colName = iomapTable.Columns.Add("变量名", typeof(string));
            DataColumn colType = iomapTable.Columns.Add("变量类型", typeof(string));
            DataColumn colIO = iomapTable.Columns.Add("IO类型", typeof(string));
            DataColumn colPort = iomapTable.Columns.Add("端口号", typeof(string));
            iomapInfo.ioMapTable = iomapTable;
        }

        //table结构：ID，变量名，数据类型，输入输入，端口
        private void RefreshMap()
        {
            iomapInfo.ioMapTable.TableName = "IOmapTable";
            iomapInfo.inputVarNum = iomapInfo.outputVarNum = 0;
            DataTable mapTable = iomapInfo.ioMapTable;
            //通过IOmapTable建立映射
            int index = 0;
            foreach (DataRow row in mapTable.Rows)
            {
                int id = index++;
                string name = (string)row["变量名"];
                string type = (string)row["变量类型"];
                string iotype = (string)row["IO类型"];
                string port = (string)row["端口号"];

                iomapInfo.mapIndexToName[id] = name;
                iomapInfo.mapNameToIndex[name] = id;
                iomapInfo.mapNameToPort[name] = port;
                iomapInfo.mapPortToName[port] = name;
                iomapInfo.mapNameToType[name] = type;

                if (iotype.Contains("I"))
                {
                    iomapInfo.inputVarNum++;
                }
                else if (iotype.Contains("O"))
                {
                    iomapInfo.outputVarNum++;
                }
            }
        }

        public DataTable GetIOmapTable(bool refresh)
        {
            if (refresh)
            {
                ScanSubConfig();
            }
            else if (iomapInfo.ioMapTable == null)
            {
                initIOmapTable();
            }
            return this.iomapInfo.ioMapTable;
        }

        #region 公共方法
        public void ReadSubConfig(object configInfo)
        {
            this.iomapInfo = (ConfigIOmapInfo)configInfo;
        }

        public object GetSubConfig()
        {
            return this.iomapInfo;
        }

        public void ScanSubConfig()
        {
            iomapInfo.ioMapTable = ExcelHelper.SelectExcelToDataTable();
            RefreshMap();
        }

        public void SaveSubConfig(object var)
        {
            this.iomapInfo.ioMapTable = (DataTable)var;
            RefreshMap();
        }
        #endregion


        public void saveIOmapToExcel()
        {
            ExcelHelper.DataTableToExcel(iomapInfo.ioMapTable);
        }
    }
}
