using System.Data;
using System.IO;
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

            this.configAdapter = new ConfigAdapter();
            this.configDevice = new ConfigDevice();
            this.configIOmap = new ConfigIOmap();
        }

        public void LoadConfig()
        {
            if (!File.Exists(ConfigFile))
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
                MessageBox.Show(ex.StackTrace + "\n" + ex.Message);
            }
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
                    //debug
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
        public DataTable ScanAdapter()
        {
            return configAdapter.getAdapterTable();
        }

        public void SetAdapterSelected(int id)
        {
            configAdapter.selectAdapter(id);
        }

        //DeviceConfig
        public TreeNode GetDeviceTree()
        {
            return configDevice.GetDeviceTree();
        }

        public void SetDeviceTree(TreeNode tn)
        {
            configDevice.SetDeviceTree(tn);
        }

        //MapConfig
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

        public void SetIOmapConfig()
        {

        }
    }
}
