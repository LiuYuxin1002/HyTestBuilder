using HyTestRTDataService.ConfigMode;
using System;
using System.Data;
using System.Windows.Forms;

namespace HTConfigSystem.View
{
    public partial class FormIOMapExplorer : Form
    {
        private ConfigManager cfm;
        public FormIOMapExplorer(ConfigManager configManager)
        {
            InitializeComponent();
            this.cfm = configManager;

            ShowConfigOnForm();
        }

        private void ShowConfigOnForm()
        {
            this.dataGridView1.DataSource = cfm.GetIOmapTableNoRefresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = cfm.GetIOmapWithRefresh();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            cfm.SaveIOmapToExcel();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                cfm.SaveIOmapConfig(this.dataGridView1.DataSource as DataTable);
                cfm.SaveConfig();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Close();
        }
    }
}
