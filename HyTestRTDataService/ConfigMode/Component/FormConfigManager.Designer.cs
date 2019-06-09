namespace HyTestRTDataService.ConfigMode.Component
{
    partial class FormConfigManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_SelectAdapter = new System.Windows.Forms.Button();
            this.btn_ScanAdapter = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_SaveDeviceConfig = new System.Windows.Forms.Button();
            this.btn_ScanDevices = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_ExportExcel = new System.Windows.Forms.Button();
            this.btn_SaveIOmapChange = new System.Windows.Forms.Button();
            this.btn_ImportExcel = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_ReloadConfig = new System.Windows.Forms.Button();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 488);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_SelectAdapter);
            this.tabPage1.Controls.Add(this.btn_ScanAdapter);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(676, 451);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网络配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_SelectAdapter
            // 
            this.btn_SelectAdapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SelectAdapter.Location = new System.Drawing.Point(189, 20);
            this.btn_SelectAdapter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SelectAdapter.Name = "btn_SelectAdapter";
            this.btn_SelectAdapter.Size = new System.Drawing.Size(170, 45);
            this.btn_SelectAdapter.TabIndex = 13;
            this.btn_SelectAdapter.Text = "选定网卡";
            this.btn_SelectAdapter.UseVisualStyleBackColor = true;
            this.btn_SelectAdapter.Click += new System.EventHandler(this.btn_SelectAdapter_Click);
            // 
            // btn_ScanAdapter
            // 
            this.btn_ScanAdapter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ScanAdapter.Location = new System.Drawing.Point(9, 20);
            this.btn_ScanAdapter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ScanAdapter.Name = "btn_ScanAdapter";
            this.btn_ScanAdapter.Size = new System.Drawing.Size(170, 45);
            this.btn_ScanAdapter.TabIndex = 12;
            this.btn_ScanAdapter.Text = "扫描网卡";
            this.btn_ScanAdapter.UseVisualStyleBackColor = true;
            this.btn_ScanAdapter.Click += new System.EventHandler(this.btn_ScanAdapter_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 74);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(655, 370);
            this.dataGridView1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.treeView1);
            this.tabPage2.Controls.Add(this.btn_SaveDeviceConfig);
            this.tabPage2.Controls.Add(this.btn_ScanDevices);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(676, 455);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "硬件设备";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(9, 74);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(655, 374);
            this.treeView1.TabIndex = 11;
            // 
            // btn_SaveDeviceConfig
            // 
            this.btn_SaveDeviceConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveDeviceConfig.Location = new System.Drawing.Point(187, 23);
            this.btn_SaveDeviceConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SaveDeviceConfig.Name = "btn_SaveDeviceConfig";
            this.btn_SaveDeviceConfig.Size = new System.Drawing.Size(170, 45);
            this.btn_SaveDeviceConfig.TabIndex = 10;
            this.btn_SaveDeviceConfig.Text = "保存配置";
            this.btn_SaveDeviceConfig.UseVisualStyleBackColor = true;
            this.btn_SaveDeviceConfig.Click += new System.EventHandler(this.btn_SaveDeviceConfig_Click);
            // 
            // btn_ScanDevices
            // 
            this.btn_ScanDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ScanDevices.Location = new System.Drawing.Point(9, 23);
            this.btn_ScanDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ScanDevices.Name = "btn_ScanDevices";
            this.btn_ScanDevices.Size = new System.Drawing.Size(170, 45);
            this.btn_ScanDevices.TabIndex = 9;
            this.btn_ScanDevices.Text = "扫描从站";
            this.btn_ScanDevices.UseVisualStyleBackColor = true;
            this.btn_ScanDevices.Click += new System.EventHandler(this.btn_ScanDevices_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_ExportExcel);
            this.tabPage3.Controls.Add(this.btn_SaveIOmapChange);
            this.tabPage3.Controls.Add(this.btn_ImportExcel);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(676, 455);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "变量管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_ExportExcel
            // 
            this.btn_ExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ExportExcel.Location = new System.Drawing.Point(187, 23);
            this.btn_ExportExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.Size = new System.Drawing.Size(170, 45);
            this.btn_ExportExcel.TabIndex = 18;
            this.btn_ExportExcel.Text = "导入变量";
            this.btn_ExportExcel.UseVisualStyleBackColor = true;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // btn_SaveIOmapChange
            // 
            this.btn_SaveIOmapChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveIOmapChange.Location = new System.Drawing.Point(494, 23);
            this.btn_SaveIOmapChange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SaveIOmapChange.Name = "btn_SaveIOmapChange";
            this.btn_SaveIOmapChange.Size = new System.Drawing.Size(170, 45);
            this.btn_SaveIOmapChange.TabIndex = 16;
            this.btn_SaveIOmapChange.Text = "保存修改";
            this.btn_SaveIOmapChange.UseVisualStyleBackColor = true;
            this.btn_SaveIOmapChange.Click += new System.EventHandler(this.btn_SaveIOmapChange_Click);
            // 
            // btn_ImportExcel
            // 
            this.btn_ImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ImportExcel.Location = new System.Drawing.Point(9, 23);
            this.btn_ImportExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ImportExcel.Name = "btn_ImportExcel";
            this.btn_ImportExcel.Size = new System.Drawing.Size(170, 45);
            this.btn_ImportExcel.TabIndex = 15;
            this.btn_ImportExcel.Text = "导入变量";
            this.btn_ImportExcel.UseVisualStyleBackColor = true;
            this.btn_ImportExcel.Click += new System.EventHandler(this.btn_ImportExcel_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(9, 74);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(655, 374);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(676, 454);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "数据库配置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(676, 454);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "测试环境配置";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_Cancel);
            this.splitContainer1.Panel2.Controls.Add(this.btn_ReloadConfig);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SaveConfig);
            this.splitContainer1.Size = new System.Drawing.Size(684, 561);
            this.splitContainer1.SplitterDistance = 488;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Location = new System.Drawing.Point(499, 12);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(172, 40);
            this.btn_Cancel.TabIndex = 19;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_ReloadConfig
            // 
            this.btn_ReloadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ReloadConfig.Location = new System.Drawing.Point(184, 12);
            this.btn_ReloadConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ReloadConfig.Name = "btn_ReloadConfig";
            this.btn_ReloadConfig.Size = new System.Drawing.Size(172, 40);
            this.btn_ReloadConfig.TabIndex = 19;
            this.btn_ReloadConfig.Text = "重新载入配置";
            this.btn_ReloadConfig.UseVisualStyleBackColor = true;
            this.btn_ReloadConfig.Click += new System.EventHandler(this.btn_ReloadConfig_Click);
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SaveConfig.Location = new System.Drawing.Point(4, 12);
            this.btn_SaveConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(172, 40);
            this.btn_SaveConfig.TabIndex = 14;
            this.btn_SaveConfig.Text = "保存配置";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // FormConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormConfigManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统配置面板";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btn_SelectAdapter;
        private System.Windows.Forms.Button btn_ScanAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btn_SaveDeviceConfig;
        private System.Windows.Forms.Button btn_ScanDevices;
        private System.Windows.Forms.Button btn_SaveIOmapChange;
        private System.Windows.Forms.Button btn_ImportExcel;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btn_ExportExcel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_ReloadConfig;
        private System.Windows.Forms.Button btn_SaveConfig;
    }
}