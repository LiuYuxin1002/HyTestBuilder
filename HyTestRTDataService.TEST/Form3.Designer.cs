namespace HyTestRTDataService.TEST
{
    partial class Form3
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_ao1 = new System.Windows.Forms.Button();
            this.htUserCurve1 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.htUserCurve1);
            this.groupBox3.Location = new System.Drawing.Point(11, 11);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(572, 241);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EL5151输入";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_ao1);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(11, 257);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(572, 59);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "EL5151输出";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(136, 20);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(266, 25);
            this.textBox1.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(38, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 22);
            this.label8.TabIndex = 9;
            this.label8.Text = "设定计数器";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ao1
            // 
            this.btn_ao1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ao1.Location = new System.Drawing.Point(406, 19);
            this.btn_ao1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ao1.Name = "btn_ao1";
            this.btn_ao1.Size = new System.Drawing.Size(56, 26);
            this.btn_ao1.TabIndex = 20;
            this.btn_ao1.Text = "写入";
            this.btn_ao1.UseVisualStyleBackColor = true;
            this.btn_ao1.Click += new System.EventHandler(this.btn_ao1_Click);
            // 
            // htUserCurve1
            // 
            this.htUserCurve1.BackColor = System.Drawing.Color.Transparent;
            this.htUserCurve1.Location = new System.Drawing.Point(-15, 34);
            this.htUserCurve1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve1.Name = "htUserCurve1";
            this.htUserCurve1.Size = new System.Drawing.Size(528, 216);
            this.htUserCurve1.TabIndex = 0;
            this.htUserCurve1.ValueMaxLeft = 32767F;
            this.htUserCurve1.ValueMinLeft = -32768F;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::HyTestRTDataService.TEST.Properties.Resources.begin;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(509, 50);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 58);
            this.button1.TabIndex = 21;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 321);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form3";
            this.Text = "Form3";
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_ao1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private Controls.Scopes.HTUserCurve htUserCurve1;
        private System.Windows.Forms.Button button1;
    }
}