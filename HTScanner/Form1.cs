using HyTestRTDataService.ConfigMode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTScanner
{
    public partial class Form1 : Form
    {
        private TreeNode root;
        private DataTable data;
        private ConfigManager manager;
        public Form1()
        {
            InitializeComponent();
            manager.LoadLocalConfig();
            manager.GetDeviceTreeNoRefresh();
        }

        //reflush
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                /*refresh tree view*/
                root = manager.GetDeviceTreeWithRefresh();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(root);
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    treeView1.Nodes[i].Expand();
                }
                /*refresh data grid view*/
                //TODO: finish it.
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
        }
    }
}
