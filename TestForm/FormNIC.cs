using HyTestEtherCAT;
using HyTestIEInterface;
using HyTestIEEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class FormNIC : Form
    {
        IAdapterLoader ethercat;
        public FormNIC()
        {
            InitializeComponent();
            ethercat = EtherCAT.getEtherCAT(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Adapter[] adapters = ethercat.getAdapter();
            DataTable nicinfo = new DataTable();
            nicinfo.Columns.Add("ID", typeof(int));
            nicinfo.Columns.Add("NAME", typeof(string));
            nicinfo.Columns.Add("DESC", typeof(string));
            nicinfo.Columns.Add("STATE", typeof(string));
            for(int i=0; i<EtherCAT.adapterNum; i++)
            {
                DataRow row = nicinfo.NewRow();
                row[0] = i+1;
                row[1] = adapters[i].name;
                row[2] = adapters[i].desc;
                row[3] = "OK";
                nicinfo.Rows.Add(row);
            }
            dataGridView1.DataSource = nicinfo;
            dataGridView1.Columns[1].Width = 200;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedNic = dataGridView1.SelectedRows[0].Index;
            ErrorCode err = ethercat.setAdapter(selectedNic);
            if (err != ErrorCode.NO_ERROR)
            {
                if (err == ErrorCode.ADAPTER_SELECT_FAIL)
                {
                    MessageBox.Show("选取失败");
                }
                else if (err == ErrorCode.NO_SLAVE_CONNECTED)
                {
                    MessageBox.Show("没有从站连接，请检查所选网卡");
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
