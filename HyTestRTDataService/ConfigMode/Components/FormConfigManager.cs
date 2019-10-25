using System;
using System.Data;
using System.Windows.Forms;
using HyTestRTDataService.ConfigMode;
using HyTestRTDataService.Entities;

namespace HyTestRTDataService.ConfigMode.Components
{
    /// <summary>
    /// Work-flow:
    /// Load Local Config -> Show All Config In Form ->
    /// (When Config Changed) -> Save Config Which Changed To TmpConfig in ConfigManager ->
    /// (When Save Click) -> Save TmpConfig to XML file.
    /// </summary>
    public partial class FormConfigManager : Form
    {
        public ConfigManager manager;         //正经ConfigManager

        private bool isSavedConfig;

        public FormConfigManager(ConfigManager manager)
        {
            InitializeComponent();

            BindingChangedEvent();

            this.manager = manager;
            isSavedConfig = true;

            ShowConfigOnForm();
        }

        #region method

        public void BindingChangedEvent()
        {
            /*set all controls in form bind with OnConfigChanged Event, so that when these controls'
              value changed, state of confirm button and apply button will be set to enable=true */
            foreach (Control group in this.splitContainer1.Panel1.Controls)
            {
                if (group is GroupBox)
                {
                    foreach (Control item in group.Controls)
                    {
                        if (item is TextBox)
                        {
                            item.TextChanged += OnConfigChanged;
                        }
                        else if (item is ComboBox)
                        {
                            ((ComboBox)item).SelectedIndexChanged += OnConfigChanged;
                        }
                        else if (item is CheckBox)
                        {
                            ((CheckBox)item).CheckedChanged += OnConfigChanged;
                        }
                        else if (item is Button)
                        {
                            ((Button)item).Click += OnConfigChanged;
                        }
                    }
                }
            }

            /*set button init state.*/
            Cancel_btn.Enabled = true;      //取消
            Confirm_btn.Enabled = false;    //确认
            Apply_btn.Enabled = false;      //应用

            /*bind need-nic check box to its button and combo box.*/
            this.checkBox_needNIC.CheckedChanged += JudgeNicNeeded;
            /*bind need-redis check box to its button and combo box.*/
            this.checkBox_needRedis.CheckedChanged += JudgeRedisNeeded;
            /*bind need-database check box to its button and combo box.*/
            this.checkBox_db.CheckedChanged += JudegDBNeeded;

            this.checkBox_db.Checked = false;
            this.checkBox_needNIC.Checked = true;
            this.checkBox_needRedis.Checked = false;
        }

        //将Config显示出来
        private void ShowConfigOnForm()
        {
            /*show nic setting*/
            this.comboBox_NIC.DataSource = manager.GetNicsWithFormatComboBox();
            this.comboBox_NIC.SelectedIndex = manager.Config.AdapterInfo.Selected;
            /*show iomap setting*/
            this.textBox_iomapPath.Text = manager.GetIOmapPath();
        }

        //配置一旦发生更改触发
        private void OnConfigChanged(object sender, EventArgs e)
        {
            this.isSavedConfig = false;
            this.Confirm_btn.Enabled = true;
            this.Apply_btn.Enabled = true;
        }

        #endregion

        #region EVENT
        //应用按钮
        private void btn_ApplyConfig_Click(object sender, EventArgs e)
        {
            /*save all info to xml*/
            manager.SaveConfig();
            /*set btn state*/
            this.Confirm_btn.Enabled = false;
            this.Apply_btn.Enabled = false;
            isSavedConfig = true;
        }
        //取消按钮
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
        //确认按钮
        private void Confirm_btn_Click(object sender, EventArgs e)
        {
            /*保存配置到xml*/
            manager.SaveConfig();
            /*close form*/
            this.Close();
        }

        /* 
         * Judge nic setting controls enable properties when checkbox changed.
         */
        private void JudgeNicNeeded(object sender, EventArgs e)
        {
            bool state = this.checkBox_needNIC.Checked;
            this.comboBox_NIC.Enabled = state;
            this.button_NicRefresh.Enabled = state;
        }

        /* 
         * Judge redis setting controls enable properties when checkbox changed.
         */
        private void JudgeRedisNeeded(object sender, EventArgs e)
        {
            bool state = this.checkBox_needRedis.Checked;
            this.textBox_redisIP.Enabled = state;
            this.textBox_redisPort.Enabled = state;
        }

        /* 
         * Judge redis setting controls enable properties when checkbox changed.
         */
        private void JudegDBNeeded(object sender, EventArgs e)
        {
            bool state = this.checkBox_db.Checked;
            foreach (Control item in this.groupBox_database.Controls)
            {
                /*we need set items in group box enable=false but check box itself.*/
                if (!(item is CheckBox)) item.Enabled = state;
            }
        }

        #endregion

        private void Button_NicRefresh_Click(object sender, EventArgs e)
        {
            manager.RefreshNicInfo();
            
            this.comboBox_NIC.DataSource = manager.GetNicsWithFormatComboBox();
        }

        private void ComboBox_NIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox_NIC.SelectedIndex;
            manager.Config.AdapterInfo.Selected = index;
        }

        private void Button_loadMap_Click(object sender, EventArgs e)
        {
            string path = manager.SetIoMap();
            this.textBox_iomapPath.Text = path;
            manager.Config.IomapInfo.FileName = path;
        }

        private void Button_mapConfig_Click(object sender, EventArgs e)
        {
            if (manager.Config.IomapInfo.FileName == null)
            {
                MessageBox.Show("尚未加载Excel");
            }
            FormIoMap form = new FormIoMap(manager);
            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

    }
}
