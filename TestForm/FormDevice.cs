using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EtherCATImpl;
using IndustrialEthernetEntity;

namespace TestForm
{
    public partial class FormDevice : Form
    {
        public EtherCAT ethercat;

        private string[] type = { "DI", "DO", "AI", "AO", };

        //public IOdevice[] devices = {
        //        new IOdevice(1, 1, "EL1004", 4),
        //        new IOdevice(2, 2, "EL2004", 4),
        //        new IOdevice(3, 3, "EL3008", 8),
        //        new IOdevice(4, 4, "EL4002", 2),
        //    };
        public FormDevice()
        {
            InitializeComponent();
            ethercat = EtherCAT.getEtherCAT(true);
            //临时的
            ethercat.getAdapter();
            ethercat.setAdapter(2);
        }
        //扫描从站
        private void button1_Click(object sender, EventArgs e)
        {
            ethercat.getDevice();//然后就可以用ethercat.devices来进行操作了

            TreeNode rootNode = new TreeNode("Devices");
            treeView1.Nodes.Add(rootNode);
            TreeNode[] typeNode = new TreeNode[4];
            for(int i=0; i<4; i++)
            {
                typeNode[i] = new TreeNode(type[i]);
            }
            
            rootNode.Nodes.AddRange(typeNode);
            
            for (int i = 0; i < EtherCAT.devices.Length; i++)
            {
                IOdevice device = EtherCAT.devices[i];
                TreeNode slave = new TreeNode(device.name);
                int slaveType = device.type;
                typeNode[slaveType-1].Nodes.Add(slave);
                for (int channel=0; channel<device.channelNum; channel++)
                {
                    TreeNode ch = new TreeNode("channel" + (channel+1));
                    slave.Nodes.Add(ch);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormNIC nicform = new FormNIC();
            nicform.ShowDialog();
            if (nicform.DialogResult == DialogResult.OK)
            {
                Console.WriteLine("LaLaLa");
            }
            else
            {
                MessageBox.Show("取消");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
