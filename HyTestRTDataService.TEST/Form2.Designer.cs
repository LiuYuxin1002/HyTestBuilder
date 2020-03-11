namespace HyTestRTDataService.TEST
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.htUserCurve4 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.htUserCurve3 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.htUserCurve1 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.htUserCurve2 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "开始采集";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(172, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "停止采集";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // htUserCurve4
            // 
            this.htUserCurve4.BackColor = System.Drawing.Color.Black;
            this.htUserCurve4.ColorLinesAndText = System.Drawing.Color.White;
            this.htUserCurve4.Location = new System.Drawing.Point(13, 600);
            this.htUserCurve4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve4.Name = "htUserCurve4";
            this.htUserCurve4.Size = new System.Drawing.Size(781, 175);
            this.htUserCurve4.TabIndex = 4;
            this.htUserCurve4.TextAddFormat = "mm:ss";
            this.htUserCurve4.Title = "AI4实时数据";
            this.htUserCurve4.ValueMaxLeft = 10F;
            this.htUserCurve4.ValueMinLeft = -10F;
            // 
            // htUserCurve3
            // 
            this.htUserCurve3.BackColor = System.Drawing.Color.Black;
            this.htUserCurve3.ColorLinesAndText = System.Drawing.Color.White;
            this.htUserCurve3.Location = new System.Drawing.Point(13, 417);
            this.htUserCurve3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve3.Name = "htUserCurve3";
            this.htUserCurve3.Size = new System.Drawing.Size(781, 175);
            this.htUserCurve3.TabIndex = 3;
            this.htUserCurve3.TextAddFormat = "mm:ss";
            this.htUserCurve3.Title = "AI3实时数据";
            this.htUserCurve3.ValueMaxLeft = 10F;
            this.htUserCurve3.ValueMinLeft = -10F;
            // 
            // htUserCurve1
            // 
            this.htUserCurve1.BackColor = System.Drawing.Color.Black;
            this.htUserCurve1.ColorLinesAndText = System.Drawing.Color.White;
            this.htUserCurve1.Location = new System.Drawing.Point(13, 51);
            this.htUserCurve1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve1.Name = "htUserCurve1";
            this.htUserCurve1.Size = new System.Drawing.Size(781, 175);
            this.htUserCurve1.StrechDataCountMax = 200;
            this.htUserCurve1.TabIndex = 2;
            this.htUserCurve1.TextAddFormat = "mm:ss";
            this.htUserCurve1.Title = "AI1实时数据";
            this.htUserCurve1.ValueMaxLeft = 10F;
            this.htUserCurve1.ValueMinLeft = -10F;
            // 
            // htUserCurve2
            // 
            this.htUserCurve2.BackColor = System.Drawing.Color.Black;
            this.htUserCurve2.ColorLinesAndText = System.Drawing.Color.White;
            this.htUserCurve2.Location = new System.Drawing.Point(12, 234);
            this.htUserCurve2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve2.Name = "htUserCurve2";
            this.htUserCurve2.Size = new System.Drawing.Size(781, 175);
            this.htUserCurve2.TabIndex = 1;
            this.htUserCurve2.TextAddFormat = "mm:ss";
            this.htUserCurve2.Title = "AI2实时数据";
            this.htUserCurve2.ValueMaxLeft = 10F;
            this.htUserCurve2.ValueMinLeft = -10F;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(784, 51);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(310, 713);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 776);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.htUserCurve4);
            this.Controls.Add(this.htUserCurve3);
            this.Controls.Add(this.htUserCurve1);
            this.Controls.Add(this.htUserCurve2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Scopes.HTUserCurve htUserCurve2;
        private Controls.Scopes.HTUserCurve htUserCurve1;
        private Controls.Scopes.HTUserCurve htUserCurve3;
        private Controls.Scopes.HTUserCurve htUserCurve4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}