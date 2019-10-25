/*---------------------------------------------------------------------------
 * File: LBLed.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System.ComponentModel;
using HyTestRTDataService.Controls.Base;

namespace HyTestRTDataService.Controls.Leds
{
    /// <summary>
    /// Description of LB7SegmentDisplay.
    /// </summary>
    public partial class HT7SegmentDisplay : HTIndustrialCtrlBase
	{
		#region (* Constructor *)
		public HT7SegmentDisplay()
		{
			InitializeComponent();
		}
		#endregion

		#region (* Properties *)
        public int val = 0;
        [
            Category("Display"),
            Description("Value of the display")
        ]
        public int Value
        {
            set { this.val = value; this.Invalidate(); }
            get { return this.val; }
        }

        private bool showDp = false;
        [
            Category("Display"),
            Description("Show the point of the display")
        ]
        public bool ShowDP
        {
            set { this.showDp = value; this.Invalidate(); }
            get { return this.showDp; }
        }
		#endregion

        #region (* Overrided methods *)
        protected override IHTRenderer CreateDefaultRenderer()
        {
            return new HT7SegmentDisplayRenderer();
        }
        #endregion
    }
}
