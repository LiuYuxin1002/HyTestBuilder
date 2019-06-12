using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;

namespace HyTestRTDataService.ConfigMode.Component
{
    public partial class FormConfigManager : Form
    {
        private ConfigManager1 confman;

        public ConfigManager configManager;         //正经ConfigManager
        public static Config config = new Config();

        private bool isSavedConfig;

        public FormConfigManager()
        {
            InitializeComponent();

            configManager = new ConfigManager();
            
            ReadXmlConfigInfoIfExist();
        }

        public FormConfigManager(ConfigManager1 confman) : this()
        {
            this.confman = confman;
        }

        #region method

       
        //判断配置文件是否存在
        private bool IsExist(string filePath)
        {
            return false;
        }

        //从xml文件读入config信息，写到config里面去，显示出来
        private void ReadXmlConfigInfoIfExist()
        {
            if (IsExist(configManager.ConfigFile))
            {
                configManager.LoadConfig();
            }

            ShowConfigOnForm();
        }

        //将Config显示出来
        private void ShowConfigOnForm()
        {

        }

        //配置一旦发生更改触发
        private void OnConfigChanged()
        {
            this.isSavedConfig = false;
            this.btn_SaveConfig.Enabled = true;
        }

        private void OnConfigSaved()
        {
            this.isSavedConfig = false;
            this.btn_SaveConfig.Enabled = false;
        }

        internal static Port GetPort(string v)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 事件

        private void btn_ScanAdapter_Click(object sender, EventArgs e)
        {
            DataTable adapterTable = configManager.GetAdapterDT();
            this.dataGridView1.DataSource = adapterTable;
        }

        private void btn_SelectAdapter_Click(object sender, EventArgs e)
        {
            int selectedAdapter = this.dataGridView1.SelectedRows[0].Index;
            try
            {
                configManager.SetAdapterSelected(selectedAdapter);
            }
            catch (System.Exception ex)
            {
            	
            }

            OnConfigChanged();
        }

        private void btn_ScanDevices_Click(object sender, EventArgs e)
        {
            TreeNode rootDeviceTree = configManager.GetDeviceTree();
            treeView1.Nodes.Add(rootDeviceTree);
        }

        private void btn_SaveDeviceConfig_Click(object sender, EventArgs e)
        {
            configManager.SaveDeviceConfig();
            OnConfigChanged();
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            configManager.SaveConfig();
            OnConfigSaved();
        }

        private void btn_ReloadConfig_Click(object sender, EventArgs e)
        {

        }

        //导入变量表
        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            configManager.GetIOmapFromExcel();
            this.dataGridView2.DataSource = config.ioMapTable;
        }
        //导出变量表
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            configManager.SaveIOmapToExcel();
        }
        //保存映射文件的更改
        private void btn_SaveIOmapChange_Click(object sender, EventArgs e)
        {
            configManager.SetIOmapConfig();
            //OnConfigChanged();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!isSavedConfig)
            {
                if (MessageBox.Show("配置还没有保存，确定关闭？", "Confirm Message", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            this.Close();
        }

        #endregion
    }
}
