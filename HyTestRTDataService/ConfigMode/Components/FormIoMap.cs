using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Components
{
    public partial class FormIoMap : Form
    {
        public FormIoMap()
        {
            InitializeComponent();
        }

        public FormIoMap(ConfigManager manager)
        {

        }
    }
}

//初始化Datagridview2
//private void InitializeIOmapDataGridView()
//{
//    DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
//    ID.DataPropertyName = "ID";
//    ID.HeaderText = "ID";
//    ID.Visible = true;
//    this.dataGridView2.Columns.Add(ID);

//    DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
//    Name.DataPropertyName = "变量名";
//    Name.HeaderText = "变量名";
//    Name.Visible = true;
//    this.dataGridView2.Columns.Add(Name);

//    DataGridViewComboBoxColumn VarType = new DataGridViewComboBoxColumn();
//    VarType.DataPropertyName = "变量类型";
//    VarType.HeaderText = "变量类型";
//    VarType.DataSource = new string[]
//    {
//        "System.Int32",
//        "System.Double",
//        "System.Boolean",
//        "System.String",
//    };
//    VarType.Visible = true;
//    this.dataGridView2.Columns.Add(VarType);

//    DataGridViewComboBoxColumn IOType = new DataGridViewComboBoxColumn();
//    IOType.DataPropertyName = "IO类型";
//    IOType.HeaderText = "IO类型";
//    IOType.DataSource = new string[]
//    {
//        "DI",
//        "DO",
//        "AI",
//        "AO",
//    };
//    IOType.Visible = true;
//    this.dataGridView2.Columns.Add(IOType);

//    DataGridViewTextBoxColumn Port = new DataGridViewTextBoxColumn();
//    Port.DataPropertyName = "端口号";
//    Port.HeaderText = "端口号";
//    Port.Visible = true;
//    this.dataGridView2.Columns.Add(Port);

//    DataGridViewTextBoxColumn MaxValue = new DataGridViewTextBoxColumn();
//    MaxValue.DataPropertyName = "变量上限";
//    MaxValue.HeaderText = "变量上限";
//    MaxValue.Visible = true;
//    this.dataGridView2.Columns.Add(MaxValue);

//    DataGridViewTextBoxColumn MinValue = new DataGridViewTextBoxColumn();
//    MinValue.DataPropertyName = "变量下限";
//    MinValue.HeaderText = "变量下限";
//    MinValue.Visible = true;
//    this.dataGridView2.Columns.Add(MinValue);
//}

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
//this.dataGridView2.DataSource = configManager.GetIOmapTableNoRefresh();

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
//    this.dataGridView2.DataSource = configManager.GetIOmapWithRefresh();
//}
////导出变量表
//private void btn_ExportExcel_Click(object sender, EventArgs e)
//{
//    configManager.SaveIOmapToExcel();
//}
////保存映射文件的更改
//private void btn_SaveIOmapChange_Click(object sender, EventArgs e)
//{
//    configManager.SaveIOmapConfig(this.dataGridView2.DataSource as DataTable);
//    //OnConfigChanged();
//}