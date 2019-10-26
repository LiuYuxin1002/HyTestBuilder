using HyTestRTDataService.ConfigMode;
using HyTestRTDataService.RunningMode;
using System;
using System.Data;
using System.Windows.Forms;

namespace HTScanner
{
    public partial class Form1 : Form
    {
        private TreeNode root;
        private DataTable data;
        private ConfigManager manager;
        private RunningServer server = RunningServer.getServer();
        public Form1()
        {
            InitializeComponent();

            server.Run();
            manager = new ConfigManager();

            InitDeviceDatagridView();
        }

        //reflush
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                /*refresh tree view*/
                manager.RefreshDevInfo();
                root = manager.GetDevsWithFormatTree();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(root);
                /*expand two level*/
                foreach(TreeNode coupler in treeView1.Nodes)
                {
                    coupler.Expand();
                    foreach(TreeNode slave in coupler.Nodes)
                    {
                        slave.Expand();
                    }
                }
                /*refresh data grid view*/
                //TODO: finish it.
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
        }

        private void InitDeviceDatagridView()
        {
            string[] colsName = { "ID", "Device", "Channel", "Variable Binding", "Type", "Bits", "Source Type(I/U)", "Range" };
            foreach(string colName in colsName)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = colName;
                col.HeaderText = colName;
                col.Visible = true;
                this.dataGridView1.Columns.Add(col);
            }
        }
    }
}
