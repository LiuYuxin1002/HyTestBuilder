using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HyTestRTDataService
{
    public partial class DelayCloseForm : Form
    {
        Timer formExistTimer;//延时关闭计时器
        /// <summary>
        /// 延时自动关闭窗体构造函数
        /// </summary>
        /// <param name="showWords">要显示的文字</param>
        /// <param name="interval">窗口要持续的时间（ms），默认500ms</param>
        public DelayCloseForm(String showWords ,int interval = 500)
        {
            InitializeComponent();
            //显示文字
            this.label1.Text = showWords;
            
            //计时器
            this.components = new Container();
            formExistTimer = new Timer(this.components);
            formExistTimer.Enabled = true;
            formExistTimer.Interval = interval;
            formExistTimer.Tick += formExistTimer_Tick;
            formExistTimer.Start();
        }

        private void formExistTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DelayCloseForm_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 1, AW_BLEND);//淡入
        }
        private void DelayCloseForm_FormClosing(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 1, AW_BLEND);//淡出
        }

        #region 窗体效果
        /// <summary>
        /// 该函数能在显示与隐藏窗口时能产生特殊的效果。有两种类型的动画效果：滚动动画和滑动动画。
        /// 函数原型：BOOL AnimateWindow（HWND hWnd，DWORD dwTime，DWORD dwFlags）；
        /// 在下列情况下函数将失败：窗口使用了窗口边界；窗口已经可见仍要显示窗口；窗口已经隐藏仍要隐藏窗口。若想获得更多错误信息，请调用GetLastError函数。
        /// 
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指明动画持续的时间（以微秒计），完成一个动画的标准时间为200微秒</param>
        /// <param name="dwFlags">指定动画类型。这个参数可以是一个或多个下列标志的组合</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零</returns>
        /// 备注：
        /// 1.可以将AW_HOR_POSITIVE或AW_HOR_NEGTVE与AW_VER_POSITVE或AW_VER_NEGATIVE组合来激活一个窗口。
        /// 2.可能需要在该窗口的窗口过程和它的子窗口的窗口过程中处理WM_PRINT或WM_PRINTCLIENT消息。对话框，控制，及共用控制已处理WM_PRINTCLIENT消息，缺省窗口过程也已处理WM_PRINT消息。  
        /// 速查：WIDdOWS NT：5.0以上版本：Windows：98以上版本；Windows CE：不支持；头文件：Winuser.h；库文件：user32.lib。  
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //标志描述：    
        const int AW_SLIDE = 0x40000;//使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。    
        const int AW_ACTIVATE = 0x20000;//激活窗口。在使用了AW_HIDE标志后不要使用这个标志。    
        const int AW_BLEND = 0x80000;//使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。    
        const int AW_HIDE = 0x10000;//隐藏窗口，缺省则显示窗口。(关闭窗口用)    
        const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。    
        const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。    
        const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。    
        const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。    
        const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略。    

        
        #endregion  

    }
}
