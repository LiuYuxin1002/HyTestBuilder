using System;
using System.Windows.Forms;
using log4net;
using HyTestRTDataService.RunningMode;
using StandardTemplate.Test;
using System.Data.Entity;
using ItemWizard;
using System.Text;
using StandardTemplate.Forms;

namespace StandardTemplate
{
    public partial class MainForm : Form
    {
        private ILog Log = LogManager.GetLogger(typeof(MainForm));

        private RunningServer server = RunningServer.getServer();

        private int flag = 0;///暂停/继续按键次数

        private delegate void Datadelegate();

        private Entity currentEntity = null;

        public MainForm()
        {
            InitializeComponent();
            //server.Run();
            
        }

        #region form event

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*设置按钮状态为不可用，直到选择了试验*/
            ForbidTest();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        #endregion

        #region button event

        // 试验配置按钮
        private void OnConfig(object sender, EventArgs e)
        {
            Log.Info("button click...");
            /*================Mark=============*/
            this.currentEntity = new Valve();//这里的实例化需要结合实际

            TestSelect selectForm = new TestSelect(currentEntity);
            if (selectForm.ShowDialog() == DialogResult.OK)
            {
                Log.Info("选择试验：");
                foreach(TestType tt in currentEntity.testList)
                {
                    Log.Info("***"+tt);
                }
                this.AllowTest();
                this.currentEntity.OnTestEnd += OnTestEnd;
            }
        }

        // 试验开始按钮
        private void OnTestStart_Click(object sender, EventArgs e)
        {
            if (this.currentEntity == null)
            {
                Log.Debug("currentTest=null");
                return;
            }

            /*开始试验*/
            this.currentEntity.StartTest();           //ForEach((entity) => { entity.StartTest(); });
            flag = 0;//将暂停继续按钮恢复到暂停状态       

            /*设置按钮状态*/
            this.toolStripButtonStart.Enabled = false;
            this.toolStripButtonPause.Enabled = true;
            this.toolStripButtonStop.Enabled = true;
            this.toolStripButtonPause.Text = "暂停";

        }

        //试验暂停按钮
        private void OnTestPause_Click(object sender, EventArgs e)
        {
            ///试验暂停          
            if (flag == 0)   //flag为0表示可暂停
            {
                this.currentEntity.SuspendTest();//将当前线程挂起
                this.toolStripButtonPause.Text = "继续";
                flag = 1;
                return;
            }
            if (flag == 1)  //flag为1表示可继续
            {
                this.toolStripButtonPause.Text = "暂停";
                this.currentEntity.ResumeTest();
                flag = 0;

            }
        }

        //试验暂停按钮
        private void OnTestStop_Click(object sender, EventArgs e)
        {
            ///试验停止
            this.currentEntity.StopTest();
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonPause.Enabled = false;
            this.toolStripButtonStart.Enabled = false;// true;
        }
        //debug按钮
        private void OnDebug_Click(object sender, EventArgs e)
        {
            //FormTest frm = new FormTest();
            //frm.ShowDialog();
            Log.Info("暂不支持调试，如需调试请联系开发人员");
        }
        //生成试验报告按钮
        private void GenerateReport_Click(object sender, EventArgs e)
        {
            Log.Info("正在处理数据，准备开始生成试验报告，这个过程可能持续几秒，请等待...");
            if (this.currentEntity == null)
            {
                Log.Error("尚未进行配置");
                return;
            }
            //解决未进行试验bug
            if (this.currentEntity.TestFinished == false)
            {
                Log.Error("未进行试验，不能生成报告");
                return;
            }
            this.currentEntity.GenerateReport();
        }

        //退出按钮
        private void OnQuit(object sender, EventArgs e)
        {
            //关闭连接释放独占请求
            System.Environment.Exit(0);
        }

        #endregion

        #region method

        private void AllowTest()
        {
            if (this.currentEntity == null)
                return;

            if (this.currentEntity.RunState == RunningState.Running)
                return;

            this.toolStripButtonStart.Enabled = true;
            this.toolStripButtonPause.Enabled = false;
            this.toolStripButtonStop.Enabled = false;
        }

        private void ForbidTest()
        {
            this.toolStripButtonStart.Enabled = false;
            this.toolStripButtonPause.Enabled = false;
            this.toolStripButtonStop.Enabled = false;
        }

        private void OnTestEnd()
        {
            /*如果直接操作线程，会导致非控件创建线程不能操作的错误。*/
            Datadelegate tmpdelegate = () =>
            {
                this.toolStripButtonStart.Enabled = false;
                this.toolStripButtonPause.Enabled = false;
                this.toolStripButtonStop.Enabled = false;
            };
            this.Invoke(tmpdelegate);    ///后台进程调用当前UI进程
            
        }

        #endregion

        private void htUserCurve1_Load(object sender, EventArgs e)
        {

        }

        private void htLed13_Load(object sender, EventArgs e)
        {

        }

        private void htLed45_Load(object sender, EventArgs e)
        {

        }

        private void htDataScanner15_Load(object sender, EventArgs e)
        {

        }
    }
}
