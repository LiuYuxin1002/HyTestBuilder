using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;
using System;
using System.ComponentModel;
using HyTestRTDataService.Entities;
using System.Collections.Generic;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigManager
    {
        private static Config config;

        private ConfigOperator configOperator;

        private string configFile = null;
        public string ConfigFile
        {
            get
            {
                if (configFile==null || configFile=="")
                {
                    configFile = Environment.CurrentDirectory + "\\..\\..\\..\\bin\\config.xml";
                }
                return configFile;
            }
            set
            {
                this.configFile = value;
            }
        }

        public Config Config
        {
            get
            {
                return config;
            }

            set
            {
                config = value;
            }
        }

        /// <summary>
        /// When configFile is cirtainly, load config.xml from this path.
        /// </summary>
        public ConfigManager(string configFile)
        {
            if (!string.IsNullOrEmpty(configFile))
            {
                //MessageBox.Show("将把路径设置为" + configFile);
                this.configFile = configFile;
            }
            LoadLocalConfig();
        }

        /// <summary>
        /// Or, using the path default.
        /// </summary>
        public ConfigManager()
        {
            LoadLocalConfig();
        }

        /// <summary>
        /// Load local config.xml to Config-obj.<br/>
        /// The param "ConfigFile" here is provided by config manager.
        /// </summary>
        private OperationResult LoadLocalConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                string errmsg = "路径不存在文件:" + ConfigFile;
                //MessageBox.Show(errmsg);
                Console.WriteLine(errmsg);
                Config = new Config();
                return this.SaveConfig();
            }
                
            try
            {
                using (FileStream fs = new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlReader xr = XmlReader.Create(fs);

                    //if (xr.AttributeCount == 0) return new OperationResult();

                    XmlSerializer xs = new XmlSerializer(typeof(Config));
                    Config = xs.Deserialize(xr) as Config;
                    fs.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                //OnConfigChanged();
            }

            return new OperationResult();
        }

        /// <summary>
        /// save config to local xml file
        /// </summary>
        public OperationResult SaveConfig()
        {
            //MessageBox.Show("现在程序的运行目录是："+Application.StartupPath);

            using (FileStream fs = new FileStream(configFile, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                XmlWriter xw = XmlWriter.Create(fs);
                XmlSerializer xs = null;
                try
                {
                    xs = new XmlSerializer(typeof(Config));
                    xs.Serialize(xw, Config);
                }
                catch (Exception ex)
                {
                    throw ex;   //debug sth. or throw exception
                }
                fs.Close();
            }
            return new OperationResult();
        }

        #region AdapterConfig

        public void RefreshNicInfo()
        {
            AdapterConfigOperator op = new AdapterConfigOperator();
            op.ScanAdapterList(Config);
        }

        public List<ListItem> GetNicsWithFormatComboBox()
        {
            Adapter[] adapters = this.Config.AdapterInfo.Adapters;
            List<ListItem> listItems = new List<ListItem>();

            for(int i=0; i<adapters.Length; i++)
            {
                listItems.Add(new ListItem(i+"", adapters[i].Name));
            }

            return listItems;
        }

        #endregion

        #region DeviceConfig

        public void RefreshDevInfo()
        {
            using(DevConfigOperator op = new DevConfigOperator()){
                op.ScanSubConfig(Config);
            }
        }

        /// <summary>
        /// Read from underlying.
        /// </summary>
        /// <returns>The TreeNode of real info of currnet devices connected.</returns>
        public TreeNode GetDevsWithFormatTree()
        {
            using(DevConfigOperator op = new DevConfigOperator())
            {
                return op.ListToTree(Config);
            }
        }

        public DataTable GetDevsWithFormatTable()
        {
            using(DevConfigOperator op = new DevConfigOperator())
            {
                return op.ListToTable(Config.DeviceInfo.DeviceList);
            }
        }

        #endregion

        #region MapConfig
        /// <summary>
        /// Read from local config.xml
        /// </summary>
        /// <returns>The DataTable of old info</returns>
        public DataTable GetIOmap()
        {
            return Config.IomapInfo.IoMapTable;
        }

        public string GetIOmapPath()
        {
            return Config.IomapInfo.FileName;
        }

        public string SetIoMap()
        {
            string path = null;
            using (IoMapConfigOperator op = new IoMapConfigOperator())
            {
                path = op.SetIoMap(Config);
            }
            return path;
        }

        public Config RefreshIOInfo(DataTable dt, string fileName)
        {
            using(IoMapConfigOperator op = new IoMapConfigOperator())
            {
                this.Config.IomapInfo.IoMapTable = dt;
                this.Config.IomapInfo.FileName = fileName;
                op.RefreshMap(Config);
            }

            return Config;
        }

        /// <summary>
        /// Export current IOmap in subConfig to a <c>Excel</c>. You should notice that 
        /// you have to call "SaveIOmapConfig" before this method if you changed
        /// the iomap. Otherwise, the saved result will be the old info.
        /// </summary>
        public void SaveIOmapToExcel()
        {
            using (IoMapConfigOperator op = (IoMapConfigOperator)configOperator)
            {
                op.saveIOmapToExcel(Config);
            }
        }

        #endregion

        #region EnvConfig

        public OperationResult SetTestInfo(ConfigTestEnvInfo envInfo)
        {
            if (envInfo != null)
            {
                this.Config.TestInfo = envInfo;
                return new OperationResult();
            }
            else return new OperationResult(-4, "传入试验信息为空，无法初始化");
        }

        #endregion
    }
}
