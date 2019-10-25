/*---------------------------------------------------------------------------
 * File: LBButton.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 * Time: 13.36
 *-------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HyTestRTDataService.Controls.Base;

namespace HyTestRTDataService.Controls.Buttons
{
    /// <summary>
    /// Description of LBButton.
    /// </summary>
    public partial class HTButton : HTIndustrialCtrlBase
	{
		#region (* Enumeratives *)
		/// <summary>
		/// Button styles
		/// </summary>
		public enum ButtonStyle
		{
			Circular = 0,
            Rectangular = 1,
            Elliptical = 2,
		}
		
		/// <summary>
		/// Button states
		/// </summary>
		public enum ButtonState
		{
			Normal = 0,
			Pressed,
		}
		#endregion
		
		#region (* Properties variables *)
		private ButtonStyle					buttonStyle = ButtonStyle.Circular;
		private ButtonState					buttonState = ButtonState.Normal;
		private Color						buttonColor = Color.Red;
		private string						label = String.Empty;
        private bool                        enableRepeatState = false;
        private int                         startRepeatInterval = 500;
        private int                         repeatInterval = 100;
		#endregion

        #region (* Variables *)
        private Timer tmrRepeat = null;
        #endregion

        #region (* Constructor *)
        public HTButton()
		{
			// Initialization
			InitializeComponent();
			
			// Properties initialization
			this.buttonColor = Color.Red;
            this.Size = new Size(50, 50);

            // Timer 
            this.tmrRepeat = new Timer();
            this.tmrRepeat.Enabled = false;
            this.tmrRepeat.Interval = this.startRepeatInterval;
            this.tmrRepeat.Tick += this.Timer_Tick;
        }
		#endregion

        #region (* Overrided methods *)
        protected override IHTRenderer CreateDefaultRenderer()
        {
            return new HTButtonRenderer();
        }
        #endregion

        #region (* Properties *)
        [
			Category("Button"),
			Description("Style of the button")
		]
		public ButtonStyle Style
		{
			set 
			{ 
				this.buttonStyle = value;
                this.CalculateDimensions();
			}
			get { return this.buttonStyle; }
		}
		
		[
			Category("Button"),
			Description("Color of the body of the button")
		]
		public Color ButtonColor
		{
			get { return buttonColor; }
			set
			{
				buttonColor = value;
				Invalidate();
			}
		}
		
		[
			Category("Button"),
			Description("Label of the button"),
		]
		public string Label
		{
			get { return this.label; }
			set
			{
				this.label = value;
				Invalidate();
			}
		}
		
		[
			Category("Button"),
			Description("State of the button")
		]
		public ButtonState State
		{
			set 
			{ 
				this.buttonState = value; 
				this.Invalidate();
			}
			get { return this.buttonState; }
		}
		
		[
			Category("Button"),
			Description("Enable/Disable the repetition of the event if the button is pressed")
		]
		public bool RepeatState
		{
			set { this.enableRepeatState = value; }
            get { return this.enableRepeatState; }
		}
		
		[
			Category("Button"),
			Description("Interval to wait in ms for start the repetition")
		]
        public int StartRepeatInterval
		{
            set { this.startRepeatInterval = value; }
            get { return this.startRepeatInterval; }
		}
		
		[
			Category("Button"),
			Description("Interva in ms for the repetition")
		]
        public int RepeatInterval
		{
            set { this.repeatInterval = value; }
            get { return this.repeatInterval; }
		}
		#endregion
		
		#region (* Events delegates *)
        /// <summary>
        /// Timer event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Tick(object sender, EventArgs e)
        {
            this.tmrRepeat.Enabled = false;

            // Update the interval
            if (tmrRepeat.Interval == this.startRepeatInterval)
                this.tmrRepeat.Interval = this.repeatInterval;
			
			// Call the delagate
			HTButtonEventArgs ev = new HTButtonEventArgs();
			ev.State = this.State;
            this.OnButtonRepeatState(ev);

            this.tmrRepeat.Enabled = true;
        }

		/// <summary>
		/// Mouse down event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMouseDown(object sender, MouseEventArgs e)
		{
			// Change the state
			this.State = ButtonState.Pressed;
			this.Invalidate();
			
			// Call the delagates
			HTButtonEventArgs ev = new HTButtonEventArgs();
			ev.State = this.State;
			this.OnButtonChangeState ( ev );

            // Enable the repeat timer
            if (this.RepeatState != false)
            {
                this.tmrRepeat.Interval = this.StartRepeatInterval;
                this.tmrRepeat.Enabled = true;
            }
		}
		
		/// <summary>
		/// Mouse up event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMuoseUp(object sender, MouseEventArgs e)
		{
			// Change the state
			this.State = ButtonState.Normal;
			this.Invalidate();
			
			// Call the delagates
			HTButtonEventArgs ev = new HTButtonEventArgs();
			ev.State = this.State;
			this.OnButtonChangeState ( ev );

            // Disable the timer
            this.tmrRepeat.Enabled = false;
		}
		#endregion
		
		#region (* Fire events *)
		/// <summary>
		/// Event for the state changed
		/// </summary>
		public event ButtonChangeState ButtonChangeState;
		
		/// <summary>
		/// Method for call the delagetes
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnButtonChangeState( HTButtonEventArgs e )
	    {
	        if( this.ButtonChangeState != null )
	            this.ButtonChangeState( this, e );
	    }		

		/// <summary>
		/// Event for the repetition of state
		/// </summary>
        public event ButtonRepeatState ButtonRepeatState;
		
		/// <summary>
		/// Method for call the delagetes
		/// </summary>
		/// <param name="e"></param>
        protected virtual void OnButtonRepeatState(HTButtonEventArgs e)
	    {
            if (this.ButtonRepeatState != null)
                this.ButtonRepeatState(this, e);
	    }		
		#endregion
	}

	#region (* Classes for event and event delagates args *)
	
	#region (* Event args class *)
	/// <summary>
	/// Class for events delegates
	/// </summary>
	public class HTButtonEventArgs : EventArgs
	{
		private HTButton.ButtonState state;
			
		public HTButtonEventArgs()
		{			
		}
	
		public HTButton.ButtonState State
		{
			get { return this.state; }
			set { this.state = value; }
		}
	}
	#endregion
	
	#region (* Delegates *)
	public delegate void ButtonChangeState ( object sender, HTButtonEventArgs e );
	public delegate void ButtonRepeatState ( object sender, HTButtonEventArgs e );
	#endregion
	
	#endregion
}
