﻿using HyTestRTDataService.ConfigMode.MapEntities;
using System.Data;
using HyTestRTDataService.Utils;

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

        //get IOmapTable from .xml file
        private void initIOmapTable()
        {
            DataTable iomapTable = new DataTable();
            iomapTable.TableName = "IOmapTable";
            DataColumn colId     = iomapTable.Columns.Add("ID", typeof(int));
            DataColumn colName   = iomapTable.Columns.Add("变量名", typeof(string));
            DataColumn colType   = iomapTable.Columns.Add("变量类型", typeof(string));
            DataColumn colIO     = iomapTable.Columns.Add("IO类型", typeof(string));
            DataColumn colPort   = iomapTable.Columns.Add("端口号", typeof(string));
            DataColumn colMax    = iomapTable.Columns.Add("变量上限", typeof(int));
            DataColumn colMin    = iomapTable.Columns.Add("变量下限", typeof(int));
            iomapInfo.ioMapTable = iomapTable;
        }

        /// <summary>
        /// Refresh iomaps with datatable which is input now.
        /// </summary>
        /// <!--table structure：ID，var name，data type，I/O，port number-->
        private void RefreshMap()
        {
            if (this.iomapInfo.ioMapTable == null) return;

            iomapInfo.ioMapTable.TableName = "IOmapTable";
            iomapInfo.inputVarNum = iomapInfo.outputVarNum = 0;
            DataTable mapTable = iomapInfo.ioMapTable;
            /*build map with dataTable.*/
            int index = 0;
            foreach (DataRow row in mapTable.Rows)
            {
                int id = index++;
                string name     = (string)row["变量名"];
                string type     = (string)row["变量类型"];
                string iotype   = (string)row["IO类型"];
                string port     = (string)row["端口号"];
                int vmax        = int.Parse((string)row["变量上限"]);
                int vmin        = int.Parse((string)row["变量下限"]);

                iomapInfo.mapIndexToName[id] = name;
                iomapInfo.mapNameToIndex[name] = id;
                iomapInfo.mapNameToPort[name] = port;
                iomapInfo.mapPortToName[port] = name;
                iomapInfo.mapNameToType[name] = type;
                iomapInfo.mapNameToMax[name] = vmax;
                iomapInfo.mapNameToMin[name] = vmin;

                /*count the num of input and output vars*/
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
