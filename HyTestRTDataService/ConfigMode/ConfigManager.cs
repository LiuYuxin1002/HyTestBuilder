using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigManager
    {
        public static Config config = new Config();

        private ConfigAdapter configAdapter;
        private ConfigDevice configDevice;
        private ConfigIOmap configIOmap;
        private ConfigTestEnvInfo configTestEnvInfo;

        public string ConfigFile//如果处于设计模式需要重写
        {
            get
            {
                return Application.StartupPath + @"\\..\\..\\..\\bin\\config.xml";
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
            if (!File.Exists(ConfigFile))
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
                MessageBox.Show(ex.Message);    //debug
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

        /// <summary>
        /// save config to local xml file
        /// </summary>
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
                    //debug sth. or throw exception
                }
                fs.Close();
            }
        }

        #region AdapterConfig

        /// <summary>
        /// Read from underlying.
        /// </summary>
        /// <returns>The data table of real info of this computer.</returns>
        public DataTable GetAdapterTableWithRefresh()
        {
            return configAdapter.GetAdapterTable(true);
        }

        /// <summary>
        /// Read from local config.xml
        /// </summary>
        /// <returns>The data table of old adapter info</returns>
        public DataTable GetAdapterTableNoRefresh()
        {
            return configAdapter.GetAdapterTable(false);
        }

        /// <summary>
        /// To save adapter config to the subConfig object.
        /// <br>(NOT CONFIG OBJ) If you want to save in config object, you should call "SaveConfig"</br>
        /// </summary>
        /// <param name="id">the adapter selected</param>
        public void SaveAdapterConfig(int id)
        {
            configAdapter.SaveSubConfig(id);
        }

        #endregion

        #region DeviceConfig

        /// <summary>
        /// Read from underlying.
        /// </summary>
        /// <returns>The TreeNode of real info of currnet devices connected.</returns>
        public TreeNode GetDeviceTreeWithRefresh()
        {
            return configDevice.GetDeviceTree(true);
        }

        /// <summary>
        /// Read from local config.xml
        /// </summary>
        /// <returns>The TreeNode of old info</returns>
        public TreeNode GetDeviceTreeNoRefresh()
        {
            return configDevice.GetDeviceTree(false);
        }

        /// <summary>
        /// To save device config to the subConfig object.
        /// <br>(NOT CONFIG OBJ) If you want to save in config object, you should call "SaveConfig"</br>
        /// </summary>
        /// <param name="tn">The TreeNode Now.</param>
        public void SaveDeviceConfig(TreeNode tn)
        {
            configDevice.SaveSubConfig(tn);
        }

        #endregion

        #region MapConfig
        /// <summary>
        /// Read from local config.xml
        /// </summary>
        /// <returns>The DataTable of old info</returns>
        public DataTable GetIOmapTableNoRefresh()
        {
            return configIOmap.GetIOmapTable(false);
        }

        /// <summary>
        /// Read from a Excel File. Call this method means user have to select a
        /// Excel File from Local File System.
        /// </summary>
        /// <returns>The DataTable of a Excel of io map .</returns>
        public DataTable GetIOmapWithRefresh()
        {
            return configIOmap.GetIOmapTable(true);
        }

        /// <summary>
        /// Export current IOmap in subConfig to a <c>Excel</c>. You should notice that 
        /// you have to call "SaveIOmapConfig" before this method if you changed
        /// the iomap. Otherwise, the saved result will be the old info.
        /// </summary>
        public void SaveIOmapToExcel()
        {
            configIOmap.saveIOmapToExcel();
        }

        
        public void SaveIOmapConfig(DataTable iomapTable)   //保存1
        {
            configIOmap.SaveSubConfig(iomapTable);//table一变全都要变
        }

        #endregion
    }
}
