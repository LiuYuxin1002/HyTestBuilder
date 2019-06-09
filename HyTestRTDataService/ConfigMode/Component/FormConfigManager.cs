using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Component
{
    public partial class FormConfigManager : Form
    {
        private string xmlPath = "";    //文件存放路径

        private ConfigManager confman;
        private Config config;

        private ConfigAdapter configAdapter;
        private ConfigDevice configDevice;
        private ConfigIOmap configIOmap;

        public FormConfigManager()
        {
            InitializeComponent();
            
            ReadXmlConfigInfoIfExist();

            this.configAdapter = new ConfigAdapter();
        }
        //从xml文件读入config信息，写到config里面去，显示出来
        private void ReadXmlConfigInfoIfExist()
        {
            if (IsExist(""))
            {
                //将xml信息读入config
                //显示config
            }
        }

        private bool IsExist(string filePath)
        {
            return false;
        }

        public FormConfigManager(ConfigManager confman) : this()
        {
            this.confman = confman;
            this.config = Config.getConfig();
        }

        private void btn_ScanAdapter_Click(object sender, EventArgs e)
        {
            DataTable adapterTable = configAdapter.getAdapterTable();
            this.dataGridView1.DataSource = adapterTable;
        }

        private void btn_SelectAdapter_Click(object sender, EventArgs e)
        {
            int selectedAdapter = this.dataGridView1.SelectedRows[0].Index;
            try
            {
                configAdapter.selectAdapter(selectedAdapter);
            }
            catch (System.Exception ex)
            {
            	
            }
        }
    }
}
