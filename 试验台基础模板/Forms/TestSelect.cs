using StandardTemplate.Test;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StandardTemplate.Forms
{
    public partial class TestSelect : Form
    {
        public List<TestType> testList;
        private Entity entity;

        public TestSelect(Entity entity)
        {
            InitializeComponent();
            this.entity = entity;
            testList = new List<TestType>();
            checkedListBox1.DataSource = entity.TestSupportList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            TestType tt = (TestType)this.checkedListBox1.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (!this.entity.TestList.Contains(tt))
                {
                    this.entity.TestList.Add(tt);
                }
            }
            else
            {
                testList.Remove(tt);
            }
        }
    }
}
