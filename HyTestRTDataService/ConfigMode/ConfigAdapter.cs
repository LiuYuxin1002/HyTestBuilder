using HyTestIEInterface;
using System.Data;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestIEInterface.Entity;
using System.Net.NetworkInformation;

namespace HyTestRTDataService.ConfigMode
{
    /* 最主要的数据： 
     * adapter[] 底层直接获取到的
     * datatable adapter转化来的，主要流通手段
     * current adapter 所选adapter对象
     * adapter number adapter的个数
     */
    public class ConfigAdapter : IConfigBase
    {
        private IAdapterLoader loader;

        private ConfigAdapterInfo adapterInfo;
        
        private Adapter[] adapterArray;

        public ConfigAdapter(ConfigAdapterInfo adapterInfo)
        {
            ReadSubConfig(adapterInfo);

        }

        public DataTable GetAdapterTable(bool refresh)
        {
            if (refresh)
            {
                ScanSubConfig();
                adapterInfo.adapterNum = adapterArray.Length;

                adapterInfo.adapterTable = new DataTable();
                adapterInfo.adapterTable.TableName = "AdapterTable";
                adapterInfo.adapterTable.Columns.Add("ID", typeof(int));
                adapterInfo.adapterTable.Columns.Add("NAME", typeof(string));
                adapterInfo.adapterTable.Columns.Add("DESCRIPTION", typeof(string));
                adapterInfo.adapterTable.Columns.Add("STATE", typeof(string));
                for (int i = 0; i < adapterInfo.adapterNum; i++)
                {
                    DataRow row = adapterInfo.adapterTable.NewRow();
                    row[0] = i + 1;                         //ID
                    row[1] = adapterArray[i].name;          //Name
                    row[2] = adapterArray[i].desc;          //description
                    row[3] = "OK";                          //state
                    adapterInfo.adapterTable.Rows.Add(row);
                }
            }
            return adapterInfo.adapterTable;
        }

        public void ReadSubConfig(object configInfo)
        {
            this.adapterInfo = (ConfigAdapterInfo)configInfo;
        }

        public object GetSubConfig()
        {
            return this.adapterInfo;
        }

        public void ScanSubConfig()
        {
            //NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
            //this.adapterArray = new Adapter[networks.Length];
            //for (int i = 0; i < networks.Length; i++)
            //{
            //    this.adapterArray[i] = new Adapter();
            //    this.adapterArray[i].name = networks[i].Description;
            //    this.adapterArray[i].desc = networks[i].Id;
            //}
            if (loader == null) loader = ConfigProtocol.GetAdapterLoader();

            this.adapterArray = loader.GetAdapter();
        }

        public void SaveSubConfig(object var)
        {
            if(loader==null) loader = ConfigProtocol.GetAdapterLoader();
            int adapterId = (int)var;
            int errCode = loader.SetAdapter(adapterId);
            adapterInfo.currentAdapter = adapterArray[adapterId];
            adapterInfo.currentAdapterId = adapterId;
        }
    }
}
