namespace StandardTemplate
{
    partial class Form1
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
            this.htAnalogMeter1 = new HyTestRTDataService.Controls.Meters.HTAnalogMeter();
            this.htDataScanner1 = new HyTestRTDataService.Controls.Scopes.HTDataScanner();
            this.htUserCurve1 = new HyTestRTDataService.Controls.Scopes.HTUserCurve();
            this.htAnalogMeter2 = new HyTestRTDataService.Controls.Meters.HTAnalogMeter();
            this.htDigitalMeter1 = new HyTestRTDataService.Controls.Meters.HTDigitalMeter();
            this.hT7SegmentDisplay1 = new HyTestRTDataService.Controls.Leds.HT7SegmentDisplay();
            this.htLed1 = new HyTestRTDataService.Controls.Leds.HTLed();
            this.htButton1 = new HyTestRTDataService.Controls.Buttons.HTButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // htAnalogMeter1
            // 
            this.htAnalogMeter1.BackColor = System.Drawing.Color.Transparent;
            this.htAnalogMeter1.BodyColor = System.Drawing.Color.Transparent;
            this.htAnalogMeter1.Location = new System.Drawing.Point(12, 12);
            this.htAnalogMeter1.MaxValue = 100D;
            this.htAnalogMeter1.MeterStyle = HyTestRTDataService.Controls.Meters.HTAnalogMeter.AnalogMeterStyle.Circular;
            this.htAnalogMeter1.MinValue = 0D;
            this.htAnalogMeter1.Name = "htAnalogMeter1";
            this.htAnalogMeter1.NeedleColor = System.Drawing.Color.DimGray;
            this.htAnalogMeter1.Renderer = null;
            this.htAnalogMeter1.ScaleColor = System.Drawing.Color.Black;
            this.htAnalogMeter1.ScaleDivisions = 11;
            this.htAnalogMeter1.ScaleSubDivisions = 10;
            this.htAnalogMeter1.Size = new System.Drawing.Size(260, 247);
            this.htAnalogMeter1.TabIndex = 0;
            this.htAnalogMeter1.Value = 48D;
            this.htAnalogMeter1.VarName = null;
            this.htAnalogMeter1.ViewGlass = false;
            // 
            // htDataScanner1
            // 
            this.htDataScanner1.Location = new System.Drawing.Point(64, 231);
            this.htDataScanner1.Margin = new System.Windows.Forms.Padding(4);
            this.htDataScanner1.Name = "htDataScanner1";
            this.htDataScanner1.Size = new System.Drawing.Size(140, 28);
            this.htDataScanner1.TabIndex = 1;
            this.htDataScanner1.VarName = "AI1";
            // 
            // htUserCurve1
            // 
            this.htUserCurve1.BackColor = System.Drawing.Color.Transparent;
            this.htUserCurve1.Location = new System.Drawing.Point(34, 281);
            this.htUserCurve1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.htUserCurve1.Name = "htUserCurve1";
            this.htUserCurve1.Size = new System.Drawing.Size(887, 190);
            this.htUserCurve1.TabIndex = 2;
            this.htUserCurve1.Title = "示波器";
            this.htUserCurve1.ValueMaxLeft = 2F;
            this.htUserCurve1.ValueMinLeft = -2F;
            // 
            // htAnalogMeter2
            // 
            this.htAnalogMeter2.BackColor = System.Drawing.Color.Transparent;
            this.htAnalogMeter2.BodyColor = System.Drawing.Color.DimGray;
            this.htAnalogMeter2.Location = new System.Drawing.Point(278, 24);
            this.htAnalogMeter2.MaxValue = 500D;
            this.htAnalogMeter2.MeterStyle = HyTestRTDataService.Controls.Meters.HTAnalogMeter.AnalogMeterStyle.Circular;
            this.htAnalogMeter2.MinValue = 0D;
            this.htAnalogMeter2.Name = "htAnalogMeter2";
            this.htAnalogMeter2.NeedleColor = System.Drawing.Color.Yellow;
            this.htAnalogMeter2.Renderer = null;
            this.htAnalogMeter2.ScaleColor = System.Drawing.Color.White;
            this.htAnalogMeter2.ScaleDivisions = 11;
            this.htAnalogMeter2.ScaleSubDivisions = 10;
            this.htAnalogMeter2.Size = new System.Drawing.Size(258, 235);
            this.htAnalogMeter2.TabIndex = 3;
            this.htAnalogMeter2.Value = 444D;
            this.htAnalogMeter2.VarName = null;
            this.htAnalogMeter2.ViewGlass = false;
            // 
            // htDigitalMeter1
            // 
            this.htDigitalMeter1.BackColor = System.Drawing.Color.Black;
            this.htDigitalMeter1.ForeColor = System.Drawing.Color.Red;
            this.htDigitalMeter1.Format = "000.000";
            this.htDigitalMeter1.Location = new System.Drawing.Point(543, 24);
            this.htDigitalMeter1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.htDigitalMeter1.Name = "htDigitalMeter1";
            this.htDigitalMeter1.Renderer = null;
            this.htDigitalMeter1.Signed = false;
            this.htDigitalMeter1.Size = new System.Drawing.Size(390, 111);
            this.htDigitalMeter1.TabIndex = 4;
            this.htDigitalMeter1.Value = 123.456D;
            this.htDigitalMeter1.VarName = null;
            // 
            // hT7SegmentDisplay1
            // 
            this.hT7SegmentDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.hT7SegmentDisplay1.Location = new System.Drawing.Point(543, 174);
            this.hT7SegmentDisplay1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hT7SegmentDisplay1.Name = "hT7SegmentDisplay1";
            this.hT7SegmentDisplay1.Renderer = null;
            this.hT7SegmentDisplay1.ShowDP = false;
            this.hT7SegmentDisplay1.Size = new System.Drawing.Size(59, 75);
            this.hT7SegmentDisplay1.TabIndex = 5;
            this.hT7SegmentDisplay1.Value = 0;
            this.hT7SegmentDisplay1.VarName = null;
            // 
            // htLed1
            // 
            this.htLed1.BackColor = System.Drawing.Color.Transparent;
            this.htLed1.BlinkInterval = 500;
            this.htLed1.Label = "LED";
            this.htLed1.LabelPosition = HyTestRTDataService.Controls.Leds.HTLed.LedLabelPosition.Top;
            this.htLed1.LedColor = System.Drawing.Color.LimeGreen;
            this.htLed1.LedSize = new System.Drawing.SizeF(30F, 30F);
            this.htLed1.Location = new System.Drawing.Point(638, 190);
            this.htLed1.Name = "htLed1";
            this.htLed1.Renderer = null;
            this.htLed1.Size = new System.Drawing.Size(64, 59);
            this.htLed1.State = HyTestRTDataService.Controls.Leds.HTLed.LedState.On;
            this.htLed1.Style = HyTestRTDataService.Controls.Leds.HTLed.LedStyle.Circular;
            this.htLed1.TabIndex = 6;
            this.htLed1.VarName = null;
            // 
            // htButton1
            // 
            this.htButton1.BackColor = System.Drawing.Color.Transparent;
            this.htButton1.ButtonColor = System.Drawing.Color.Red;
            this.htButton1.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htButton1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.htButton1.Label = "Button";
            this.htButton1.Location = new System.Drawing.Point(721, 199);
            this.htButton1.Name = "htButton1";
            this.htButton1.Renderer = null;
            this.htButton1.RepeatInterval = 100;
            this.htButton1.RepeatState = false;
            this.htButton1.Size = new System.Drawing.Size(124, 50);
            this.htButton1.StartRepeatInterval = 500;
            this.htButton1.State = HyTestRTDataService.Controls.Buttons.HTButton.ButtonState.Normal;
            this.htButton1.Style = HyTestRTDataService.Controls.Buttons.HTButton.ButtonStyle.Rectangular;
            this.htButton1.TabIndex = 7;
            this.htButton1.VarName = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "仪表盘";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "模拟量显示";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "单个数字显示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(676, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "模拟量LED显示";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(647, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "LED灯";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(756, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "按钮";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 484);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.htButton1);
            this.Controls.Add(this.htLed1);
            this.Controls.Add(this.hT7SegmentDisplay1);
            this.Controls.Add(this.htDigitalMeter1);
            this.Controls.Add(this.htAnalogMeter2);
            this.Controls.Add(this.htUserCurve1);
            this.Controls.Add(this.htDataScanner1);
            this.Controls.Add(this.htAnalogMeter1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HyTestRTDataService.Controls.Meters.HTAnalogMeter htAnalogMeter1;
        private HyTestRTDataService.Controls.Scopes.HTDataScanner htDataScanner1;
        private HyTestRTDataService.Controls.Scopes.HTUserCurve htUserCurve1;
        private HyTestRTDataService.Controls.Meters.HTAnalogMeter htAnalogMeter2;
        private HyTestRTDataService.Controls.Meters.HTDigitalMeter htDigitalMeter1;
        private HyTestRTDataService.Controls.Leds.HT7SegmentDisplay hT7SegmentDisplay1;
        private HyTestRTDataService.Controls.Leds.HTLed htLed1;
        private HyTestRTDataService.Controls.Buttons.HTButton htButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}