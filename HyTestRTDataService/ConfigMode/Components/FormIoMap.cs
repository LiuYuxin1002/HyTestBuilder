using HyTestRTDataService.Utils;
using System;
using System.Data;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Components
{
    public partial class FormIoMap : Form
    {
        private ConfigManager manager;

        public FormIoMap()
        {
            InitializeComponent();
        }

        public FormIoMap(ConfigManager manager) : this()
        {
            this.manager = manager;
            /*Init data grid view with config data*/
            InitIOmapDataGridView();
            this.dataGridView1.DataSource = this.manager.GetIOmap();
            /*bind event when data changed*/
            Import_btn.Click += EnableBtnState;
            dataGridView1.CellValueChanged += EnableBtnState;
        }

        private void EnableBtnState(object sender, EventArgs e)
        {
            this.Confirm_btn.Enabled = true;
            this.Apply_btn.Enabled = true;
        }

        //初始化dataGridView1
        private void InitIOmapDataGridView()
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Visible = true;
            this.dataGridView1.Columns.Add(ID);

            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            Name.DataPropertyName = "变量名";
            Name.HeaderText = "变量名";
            Name.Visible = true;
            this.dataGridView1.Columns.Add(Name);

            DataGridViewComboBoxColumn VarType = new DataGridViewComboBoxColumn();
            VarType.DataPropertyName = "变量类型";
            VarType.HeaderText = "变量类型";
            VarType.DataSource = new string[]
            {
                "System.Int32",
                "System.Double",
                "System.Boolean",
                "System.String",
            };
            VarType.Visible = true;
            this.dataGridView1.Columns.Add(VarType);

            DataGridViewComboBoxColumn IOType = new DataGridViewComboBoxColumn();
            IOType.DataPropertyName = "IO类型";
            IOType.HeaderText = "IO类型";
            IOType.DataSource = new string[]
            {
                "DI",
                "DO",
                "AI",
                "AO",
            };
            IOType.Visible = true;
            this.dataGridView1.Columns.Add(IOType);

            DataGridViewTextBoxColumn Port = new DataGridViewTextBoxColumn();
            Port.DataPropertyName = "端口号";
            Port.HeaderText = "端口号";
            Port.Visible = true;
            this.dataGridView1.Columns.Add(Port);

            DataGridViewTextBoxColumn MaxValue = new DataGridViewTextBoxColumn();
            MaxValue.DataPropertyName = "变量上限";
            MaxValue.HeaderText = "变量上限";
            MaxValue.Visible = true;
            this.dataGridView1.Columns.Add(MaxValue);

            DataGridViewTextBoxColumn MinValue = new DataGridViewTextBoxColumn();
            MinValue.DataPropertyName = "变量下限";
            MinValue.HeaderText = "变量下限";
            MinValue.Visible = true;
            this.dataGridView1.Columns.Add(MinValue);
        }

        private void Confirm_btn_Click(object sender, System.EventArgs e)
        {
            this.manager.SaveConfig();
            this.Close();
        }

        private void Cancel_btn_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("确定关闭？", "Confirm Message", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            this.Close();
        }

        private void Apply_btn_Click(object sender, System.EventArgs e)
        {
            this.manager.SaveConfig();
        }

        private void Import_btn_Click(object sender, System.EventArgs e)
        {
            try
            {
                string fileName = "";

                manager.Config.IomapInfo.IoMapTable = ExcelHelper.SelectExcelToDataTable(out fileName);
                if (manager.Config.IomapInfo.IoMapTable != null && !string.IsNullOrEmpty(fileName))
                {
                    manager.Config = manager.RefreshIOInfo(manager.Config.IomapInfo.IoMapTable, fileName);
                }
                dataGridView1.DataSource = manager.Config.IomapInfo.IoMapTable;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Export_btn_Click(object sender, System.EventArgs e)
        {

        }


    }
}



//判断配置文件是否存在
//private bool IsExist(string filePath)
//{
//    return false;
//}

//从xml文件读入config信息，写到config里面去，显示出来
//private void ReadXmlConfigInfoIfExist()
//{
//    if (IsExist(configManager.ConfigFile))
//    {
//        configManager.LoadLocalConfig();
//        ShowConfigOnForm();
//    }
//}

////adapter
//this.dataGridView1.DataSource = configManager.GetAdapterTableNoRefresh();
////device
//TreeNode tn = configManager.GetDeviceTreeNoRefresh();
//if (tn!=null)
//{
//    this.treeView1.Nodes.Add(tn);
//}
////iomap
//this.dataGridView1.DataSource = configManager.GetIOmapTableNoRefresh();

//private void btn_ScanAdapter_Click(object sender, EventArgs e)
//{
//    DataTable adapterTable = null;
//    try
//    {
//        adapterTable = configManager.GetAdapterTableWithRefresh();
//    }
//    catch (System.Exception ex)
//    {
//        MessageBox.Show(ex.Message);
//    }

//    if(adapterTable!=null) this.dataGridView1.DataSource = adapterTable;
//}

//private void btn_SelectAdapter_Click(object sender, EventArgs e)
//{
//    int selectedAdapter = this.dataGridView1.SelectedRows[0].Index;
//    try
//    {
//        configManager.SaveAdapterConfig(selectedAdapter);
//    }
//    catch (System.Exception ex)
//    {

//    }

//    OnConfigChanged();
//}

//private void btn_ScanDevices_Click(object sender, EventArgs e)
//{
//    TreeNode rootDeviceTree = configManager.GetDeviceTreeWithRefresh();
//    treeView1.Nodes.Clear();
//    treeView1.Nodes.Add(rootDeviceTree);
//    for(int i=0; i<treeView1.Nodes.Count; i++)
//    {
//        treeView1.Nodes[i].Expand();
//    }
//}

//private void btn_SaveDeviceConfig_Click(object sender, EventArgs e)
//{
//    configManager.SaveDeviceConfig(treeView1.Nodes[0]);
//    OnConfigChanged();
//}

////导入变量表
//private void btn_ImportExcel_Click(object sender, EventArgs e)
//{
//    this.dataGridView1.DataSource = configManager.GetIOmapWithRefresh();
//}
////导出变量表
//private void btn_ExportExcel_Click(object sender, EventArgs e)
//{
//    configManager.SaveIOmapToExcel();
//}
////保存映射文件的更改
//private void btn_SaveIOmapChange_Click(object sender, EventArgs e)
//{
//    configManager.SaveIOmapConfig(this.dataGridView1.DataSource as DataTable);
//    //OnConfigChanged();
//}