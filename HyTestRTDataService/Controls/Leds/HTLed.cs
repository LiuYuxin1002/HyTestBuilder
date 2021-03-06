/*---------------------------------------------------------------------------
 * File: LBLed.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HyTestRTDataService.RunningMode;
using HyTestRTDataService.Controls.Base;

namespace HyTestRTDataService.Controls.Leds
{
    /// <summary>
    /// Class for the Led control.
    /// </summary>
    public partial class HTLed : HTIndustrialCtrlBase
	{
        #region(* My Alter *)
        public override void OnDataChanged(object sender, EventArgs e)
        {
            FetchDataAndShow();
        }

        private void FetchDataAndShow()
        {
            if (varName == null || varName == "") return;

            RunningServer server = RunningServer.getServer();      
            bool value = server.NormalRead<bool>(varName);
            this.State = value ? LedState.On : LedState.Off;
        }

        private void OnConnected(object sender, EventArgs e)
        {
            RunningServer server = RunningServer.getServer();
            server.DataRefresh += OnDataChanged;
        }

        #endregion
        #region (* Enumeratives *)
        public enum LedState
		{
			Off	= 0,
			On,
			Blink,
		}
		
		public enum LedLabelPosition
		{
			Left = 0,
			Top,
			Right,
			Bottom,
		}

        public enum LedStyle
        {
            Circular = 0,
            Rectangular,
        }

        #endregion
		
		#region (* Properties variables *)
		private Color				ledColor;
		private LedState			state;
		private LedStyle			style;
		private LedLabelPosition	labelPosition;
		private	String				label = "Led";
		private SizeF				ledSize;
		private int					blinkInterval = 500;
		#endregion
		
		#region (* Class variables *)
		private	Timer 				tmrBlink;
		private	bool 				blinkIsOn = false;
		#endregion
		
		#region (* Constructor *)
		public HTLed()
		{
			InitializeComponent();

            this.Size           = new Size(20, 20);
			this.ledColor		= Color.Red;
			this.state 			= HTLed.LedState.Off;
            this.style          = HTLed.LedStyle.Circular;
			this.blinkIsOn		= false;
			this.ledSize		= new SizeF ( 10F, 10F );
			this.labelPosition = LedLabelPosition.Top;

            //data subjection
            RunningServer server = RunningServer.getServer();
            server.Connected += OnConnected;
        }
		#endregion
		
		#region (* Properties *)
        [
            Category("Led"),
            Description("Style of the led")
        ]
        public LedStyle Style
        {
            get { return style; }
            set
            {
                style = value;
                this.CalculateDimensions();
            }
        }
        [
			Category("Led"),
			Description("Color of the led")
		]
		public Color LedColor
		{
			get { return ledColor; }
			set
			{
				ledColor = value;
				Invalidate();
			}
		}
		
		
		[
			Category("Led"),
			Description("State of the led")
		]
		public LedState State
		{
			get { return state; }
			set
			{
				state = value;
				if ( state == LedState.Blink )
				{
					this.blinkIsOn = true;
					this.tmrBlink.Interval = this.BlinkInterval;
					this.tmrBlink.Start();
				}
				else
				{
					this.blinkIsOn = true;
					this.tmrBlink.Stop();
				}
				
				Invalidate();
			}
		}
		
		
		[
			Category("Led"),
			Description("Size of the led")
		]
		public SizeF LedSize
		{
			get { return this.ledSize; }
			set
			{
				this.ledSize = value;
				this.CalculateDimensions();
				Invalidate();
			}
		}
		
		
		[
			Category("Led"),
			Description("Label of the led")
		]
		public String Label
		{
			get { return this.label; }
			set
			{
				this.label = value;
				Invalidate();
			}
		}
		
				
		[
			Category("Led"),
			Description("Position of the label of the led")
		]
		public LedLabelPosition LabelPosition
		{
			get { return this.labelPosition; }
			set
			{
				this.labelPosition = value;
				this.CalculateDimensions();
				Invalidate();
			}
		}
		
				
		[
			Category("Led"),
			Description("Interval for the blink state of the led")
		]
		public int BlinkInterval
		{
			get { return this.blinkInterval; }
			set { this.blinkInterval = value; }
		}
		
		[Browsable(false)]
		public bool BlinkIsOn
		{
			get { return this.blinkIsOn; }
		}
		#endregion
		
		#region (* Events delegates *)
		void OnBlink(object sender, EventArgs e)
		{
			if ( this.State == LedState.Blink )
			{
				if ( this.blinkIsOn == false )
					this.blinkIsOn = true;
				else
					this.blinkIsOn = false;
				
				this.Invalidate();
			}
		}
		#endregion
		
		#region (* Overrided methods *)
        protected override IHTRenderer CreateDefaultRenderer()
        {
            return new HTLedRenderer();
        }
		#endregion
	}
}
