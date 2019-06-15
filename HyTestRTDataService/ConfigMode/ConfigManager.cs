using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestIEEntity;
using System;

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
            InitializeComponent();
        }

        //加载xmlconfig，初始化subconfig
        private void InitializeComponent()
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

                    //if (xr.AttributeCount == 0) return;

                    XmlSerializer xs = new XmlSerializer(typeof(Config));
                    config = xs.Deserialize(xr) as Config;
                    fs.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            OnConfigChanged();
        }

        //当config变化时触发，更新所有SubConfig
        private void OnConfigChanged()
        {
            if(configAdapter!=null && configDevice!=null && configIOmap != null)
            {
                configAdapter.ReadSubConfig(config.adapterInfo);
                configDevice.ReadSubConfig(config.deviceInfo);
                configIOmap.ReadSubConfig(config.iomapInfo);
            }
            
        }

        private void MoveSubconfigToConfig()
        {
            //adapter
            config.adapterInfo = (ConfigAdapterInfo)configAdapter.GetSubConfig();
            //device
            config.deviceInfo = (ConfigDeviceInfo)configDevice.GetSubConfig();
            //iomap
            config.iomapInfo = (ConfigIOmapInfo)configIOmap.GetSubConfig();
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
                    xs.Serialize(xw, config);
                }
                catch (System.Exception ex)
                {
                    //debug
                }
                fs.Close();
            }
        }

        //AdapterConfig

        public DataTable GetAdapterTableWithRefresh()   //刷新
        {
            return configAdapter.getAdapterTable(true);
        }

        public DataTable GetAdapterTableNoRefresh()     //不刷新
        {
            return configAdapter.getAdapterTable(false);
        }

        public void SaveAdapterConfig(int id)           //保存
        {
            configAdapter.SaveSubConfig(id);
        }

        //DeviceConfig
        public TreeNode GetDeviceTreeWithRefresh()      //刷新
        {
            return configDevice.GetDeviceTree(true);
        }
        public TreeNode GetDeviceTreeNoRefresh()        //不刷新
        {
            return configDevice.GetDeviceTree(false);
        }

        public void SaveDeviceConfig(TreeNode tn)       //保存
        {
            configDevice.SaveSubConfig(tn);
        }

        //MapConfig
        //获取本地配置的IOmap，由于已经从Config加载到本地了，所以直接返回就行
        public DataTable GetIOmapTableNoRefresh()       //不刷新
        {
            return configIOmap.GetIOmapTable(false);
        }

        public DataTable GetIOmapWithRefresh()          //刷新
        {
            return configIOmap.GetIOmapTable(true);
        }

        public void SaveIOmapToExcel()                  //保存Excel
        {
            configIOmap.saveIOmapToExcel();
        }

        public void SaveIOmapConfig(DataTable iomapTable)   //保存1
        {
            configIOmap.SaveSubConfig(iomapTable);//table一变全都要变
        }
    }
}
