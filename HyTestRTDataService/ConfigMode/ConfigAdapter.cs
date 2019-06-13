﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestIEEntity;
using System.Data;
using HyTestEtherCAT;
using HyTestRTDataService.ConfigMode.Component;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigAdapter:IConfigBase
    {
        IAdapterLoader loader;

        private IList<Adapter> adapterList;
        private IList<Adapter> ignoreList;  //忽略列表

        public DataTable adapterTable;
        public Adapter currentAdapter;
        public int adapterNum;

        public ConfigAdapter()
        {
            adapterList = new List<Adapter>();
            loader = EtherCAT.getEtherCAT(true);

            getAdapterList();
        }
        
        public IList<Adapter> getAdapterList()
        {
            if (this.adapterList != null)
            {
                //debug adapterList不为空
            }
            Adapter[] adapterArr = loader.getAdapter();
            this.adapterList = adapterArr.ToList();
            this.adapterNum = adapterList.Count;
            return this.adapterList;
        } 

        public DataTable getAdapterTable()
        {
            Adapter[] adapterArr = loader.getAdapter();

            this.adapterTable = new DataTable();
            adapterTable.Columns.Add("ID", typeof(int));
            adapterTable.Columns.Add("NAME", typeof(string));
            adapterTable.Columns.Add("DESCRIPTION", typeof(string));
            adapterTable.Columns.Add("STATE", typeof(string));
            for (int i = 0; i < this.adapterNum; i++)
            {
                DataRow row = adapterTable.NewRow();
                row[0] = i + 1;
                row[1] = adapterArr[i].name;
                row[2] = adapterArr[i].desc;
                row[3] = "OK";
                adapterTable.Rows.Add(row);
            }

            return this.adapterTable;
        }

        public void refreshAdapterList()
        {

        }

        public void selectAdapter(int adapterId)//id是角标+1
        {
            //选取网卡，处理错误
            ErrorCode errCode = loader.setAdapter(adapterId);
            this.currentAdapter = adapterList[adapterId - 1];
        }

        public void RefreshLocalConfigFromConfig()
        {
            throw new NotImplementedException();
        }
    }
}
