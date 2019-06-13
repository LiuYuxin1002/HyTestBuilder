using System;
using System.Collections.Generic;
using System.Linq;
using HyTestIEInterface;
using HyTestIEEntity;
using System.Data;
using HyTestEtherCAT;
using HyTestRTDataService.ConfigMode.MapEntities;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigAdapter : IConfigBase
    {
        IAdapterLoader loader;

        ConfigAdapterInfo adapterInfo;

        public Adapter[] adapterArray;
        public DataTable adapterTable;
        public Adapter currentAdapter;
        public int adapterNum;

        public ConfigAdapter(ConfigAdapterInfo adapterInfo)
        {
            loader = EtherCAT.getEtherCAT(true);

            this.adapterInfo = adapterInfo;
            ReadConfig();
        }

        public DataTable getAdapterTable()
        {
            adapterArray = loader.getAdapter();
            this.adapterNum = adapterArray.Length;

            this.adapterTable = new DataTable();
            adapterTable.Columns.Add("ID", typeof(int));
            adapterTable.Columns.Add("NAME", typeof(string));
            adapterTable.Columns.Add("DESCRIPTION", typeof(string));
            adapterTable.Columns.Add("STATE", typeof(string));
            for (int i = 0; i < this.adapterNum; i++)
            {
                DataRow row = adapterTable.NewRow();
                row[0] = i + 1;
                row[1] = adapterArray[i].name;
                row[2] = adapterArray[i].desc;
                row[3] = "OK";
                adapterTable.Rows.Add(row);
            }

            return this.adapterTable;
        }

        public void selectAdapter(int adapterId)//id是角标+1
        {
            //选取网卡，处理错误
            ErrorCode errCode = loader.setAdapter(adapterId);
            this.currentAdapter = adapterArray[adapterId - 1];
        }

        public void ReadConfig()
        {
            if (adapterInfo != null)
            {
                this.adapterTable = adapterInfo.adapterTable;
                this.currentAdapter = adapterInfo.currentAdapter;
                this.adapterNum = adapterInfo.adapterNum;
            }
        }
    }
}
