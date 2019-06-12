using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

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
                return Application.StartupPath + @"\\opcserver.xml";
            }
        }

        public ConfigManager()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            this.configAdapter = new ConfigAdapter();
            this.configDevice = new ConfigDevice();
            this.configIOmap = new ConfigIOmap();
        }

        public void SaveConfig()
        {
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

                }
                xs.Serialize(xw, config);
                fs.Close();
            }
        }

        public void SaveDeviceConfig()
        {

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

        //具体配置过程：上面都是加载旧的XML的方法，但是每个配置过程都有获取新的配置的途径
        //下面就是新配置的获取和保存，具体的实现都在各个配置类里面封装

        //AdapterConfig
        public DataTable GetAdapterDT()
        {
            return configAdapter.getAdapterTable();
        }

        public void SetAdapterSelected(int id)
        {

        }

        //DeviceConfig
        public TreeNode GetDeviceTree()
        {
            return configDevice.getDeviceTree();
        }

        public void SetDeviceTree(TreeNode tn)
        {

        }

        //MapConfig
        public void GetIOmapFromExcel()
        {
            configIOmap.getIOmapFromExcel();

            //然后把configIOmap里面的内容倒腾到Config里面
            //最后的显示是界面的事情了
        }

        public void SaveIOmapToExcel()
        {
            //可选路径
            configIOmap.saveIOmapToExcel();
        }

        public void SetIOmapConfig()
        {

        }
    }
}
