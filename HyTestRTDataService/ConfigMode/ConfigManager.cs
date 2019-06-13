using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestIEEntity;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigManager
    {
        public static Config config = new Config();

        private ConfigAdapter configAdapter;
        private ConfigDevice configDevice;
        private ConfigIOmap configIOmap;

        public string ConfigFile//如果处于设计模式需要重写
        {
            get
            {
                return Application.StartupPath + @"\\config.xml";
            }
        }

        public ConfigManager()
        {
            LoadConfig();

            this.configAdapter = new ConfigAdapter(config.adapterInfo);
            this.configDevice = new ConfigDevice(config.deviceInfo);
            this.configIOmap = new ConfigIOmap(config.iomapInfo);
        }

        /// <summary>
        /// 把本地配置文件读取到Config对象中，会抛出System.Exception异常
        /// </summary>
        public void LoadConfig()
        {
            if (!File.Exists(ConfigFile))   //安全检查
                return;

            try
            {
                using (FileStream fs = new FileStream(ConfigFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlReader xr = XmlReader.Create(fs);
                    XmlSerializer xs = new XmlSerializer(typeof(Config));
                    config = xs.Deserialize(xr) as Config;
                    fs.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception();
            }
        }

        private void MoveSubconfigToConfig()
        {
            //adapter
            config.adapterInfo.adapterNum = configAdapter.adapterNum;
            config.adapterInfo.adapterTable = configAdapter.adapterTable;
            config.adapterInfo.currentAdapter = configAdapter.currentAdapter;
            //device
            config.deviceInfo.deviceArr = configDevice.deviceArray;
            config.deviceInfo.deviceNum = configDevice.deviceNum;
            config.deviceInfo.deviceTable = configDevice.deviceTable;
            //iomap
            config.iomapInfo.inputVarNum = configIOmap.inputVarNum;
            config.iomapInfo.outputVarNum = configIOmap.outputVarNum;
            config.iomapInfo.ioMapTable = configIOmap.ioMapTable;
            config.iomapInfo.mapIndexToName = configIOmap.mapIndexToName;
            config.iomapInfo.mapNameToIndex = configIOmap.mapNameToIndex;
            config.iomapInfo.mapNameToPort = configIOmap.mapNameToPort;
            config.iomapInfo.mapPortToName = configIOmap.mapPortToName;
            config.iomapInfo.mapNameToType = configIOmap.mapNameToType;

            //database
            //environment
            //...
        }

        public void SaveConfig()
        {
            //将所有配置集中到Config对象
            MoveSubconfigToConfig();

            using (FileStream fs = new FileStream(ConfigFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                XmlWriter xw = XmlWriter.Create(fs);
                XmlSerializer xs = null;
                try
                {
                    xs = new XmlSerializer(typeof(Config));
                }
                catch (System.Exception ex)
                {
                    //debug
                }
                xs.Serialize(xw, config);
                fs.Close();
            }
        }

        

        public void SaveIOmapConfig()
        {

        }

        public void SaveEnvironmentConfig()
        {

        }

        public void SaveDatabaseConfig()
        {

        }

        //AdapterConfig

        //从底层获取真实adapter列表
        public DataTable GetAdapterTable()
        {
            return configAdapter.getAdapterTable();
        }

        public void SetAdapterSelected(int id)
        {
            configAdapter.selectAdapter(id);
        }

        //DeviceConfig
        public TreeNode GetDeviceTree(bool needNew)
        {
            IOdevice[] deviceArr;
            if (needNew)
            {
                deviceArr = configDevice.ScanDeviceArr();
            }
            else
            {
                deviceArr = config.deviceInfo.deviceArr;
            }
            return configDevice.ArrToTreeNode(deviceArr);
        }

        public void SetDeviceTree(TreeNode tn)
        {
            configDevice.SetDeviceTree(tn);
        }

        //MapConfig
        //获取本地配置的IOmap，由于已经从Config加载到本地了，所以直接返回就行
        public DataTable GetIOmapTable()
        {
            return configIOmap.ioMapTable;
        }

        public DataTable GetIOmapFromExcel()
        {
            return configIOmap.getIOmapFromExcel();

            //然后把configIOmap里面的内容倒腾到Config里面
            //最后的显示是界面的事情了
        }

        public void SaveIOmapToExcel()
        {
            //可选路径
            configIOmap.saveIOmapToExcel();
        }

        public void SetIOmapConfig(DataTable iomapTable)
        {
            ConfigIOmap.SetIOmapInfo(iomapTable);//table一变全都要变
        }
    }
}
