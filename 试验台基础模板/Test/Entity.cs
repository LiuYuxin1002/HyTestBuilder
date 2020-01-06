using HyTestRTDataService;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StandardTemplate.Test
{
    public abstract class Entity
    {
        #region Protect

        protected RunningState runningState;

        protected Thread threadTest;                //试验线程

        public List<TestType> TestSupportList;   //支持的试验

        public List<TestType> testList;            //要做的试验

        protected ILog Log
        {
            get
            {
                return LogManager.GetLogger(this.GetType());
            }
        }

        protected bool testFinished;

        #endregion

        #region Event

        public delegate void TestEndEventHandler();
        public TestEndEventHandler OnTestEnd;

        #endregion

        #region Property

        /// <summary>
        /// 获取试验运行状态。停止、正在运行和暂停。用于主窗体判断试验是否运行。
        /// </summary>
        /// <returns>停止、正在运行和暂停。</returns>
        public RunningState RunState
        {
            get
            {
                if (threadTest == null)
                {
                    runningState = RunningState.Stop;
                }
                else
                    switch (threadTest.ThreadState)
                    {
                        case ThreadState.Running:           //未被阻塞
                            runningState = RunningState.Running;
                            break;
                        case ThreadState.WaitSleepJoin:     //sleep/join
                            runningState = RunningState.Running;
                            break;
                        case ThreadState.Suspended:
                            runningState = RunningState.Suspend;
                            break;
                        default:
                            runningState = RunningState.Stop;
                            break;
                    }
                return runningState;
            }
            set
            {
                this.runningState = value;
            }
        }

        public bool TestFinished
        {
            get { return this.TestFinished; }
        }

        public int Id { get; set; }

        public List<TestType> TestList
        {
            get
            {
                if (testList == null) return null;
                return testList;
            }

            set
            {
                testList = value;
            }
        }

        #endregion

        #region Test Thread Operation

        /// <summary>
        /// 试验操作-开始。
        /// </summary>
        /// <returns></returns>
        public OperationResult StartTest()
        {
            if (this.threadTest == null)
            {
                BeforeTest();
                threadTest = new Thread(this.Run);
            }
            if (threadTest.ThreadState != ThreadState.Running)
            {
                threadTest.Start();
            }
            return new OperationResult();
        }

        /// <summary>
        /// 试验操作-暂停。
        /// </summary>
        /// <returns></returns>
        public OperationResult SuspendTest()
        {
            if ((threadTest.ThreadState == ThreadState.Unstarted) || 
                (threadTest.ThreadState == ThreadState.Stopped))
            {
                Log.Debug("当前试验尚未开始或已停止，暂停无效!");
                throw new Exception("当前试验尚未开始或已停止，暂停无效!");
            }

            threadTest.Suspend();

            /*休眠,挂起需要时间*/
            System.Threading.Thread.Sleep(300);
            return new OperationResult();
        }

        /// <summary>
        /// 试验操作-恢复暂停。
        /// </summary>
        /// <returns></returns>
        public OperationResult ResumeTest()
        {
            if (threadTest.ThreadState == ThreadState.Suspended)
            {
                threadTest.Resume();
                /*需要时间*/
                System.Threading.Thread.Sleep(300);
            }
            else
            {
                Log.Debug("当前试验尚未进入暂停状态，无法继续！");
                throw new Exception("当前试验尚未进入暂停状态，无法继续！");
            }

            return new OperationResult();
        }

        /// <summary>
        /// 试验操作-停止。
        /// </summary>
        /// <returns></returns>
        public OperationResult StopTest()
        {
            try
            {
                threadTest.Abort();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                Log.Debug("试验中止");
                AfterTest();
            }
            return new OperationResult();
        }

        #endregion

        #region Test Method Enhancement

        /// <summary>
        /// 这个方法用于指示试验开始前的准备工作。比如试验前的手动准备弹窗提醒或仪器校准。
        /// </summary>
        public OperationResult BeforeTest()
        {
            Log.Info("试验准备开始...");
            
            /*试验开始前总需要做点什么*/

            OperationResult res2 = BeforeTestCustom(new object());
            return res2;
        }

        public abstract OperationResult BeforeTestCustom(Object testInfo);

        /// <summary>
        /// 试验开始线程。
        /// </summary>
        protected void Run()
        {
            Log.Info("试验开始...");
            try
            {
                foreach (TestType testType in this.testList)
                {
                    Log.Info(testType.ToString() + " 开始");
                    RunTest(testType);
                    Log.Info(testType.ToString() + " 结束");
                }
            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw e;
            }
        }

        /// <summary>
        /// 开始进行试验，不同设备实现不同试验。比如：耐压试验、外泄露试验、稳态压力流量试验。
        /// </summary>
        /// <example>
        /// protected override void RunTest(TestType testType)
        /// {
        ///    if (!this.testTypes.Contains(testType))
        ///        this.testTypes.Add(testType);
        ///    switch (testType)
        ///    {
        ///        case TestType.耐压试验:
        ///            this.EndurationTest();
        ///            break;
        ///        case TestType.外泄漏试验:
        ///            this.ExternalLeakTest();     ///实验程序
        ///            break;
        ///        case TestType.稳态流量压力特性试验:
        ///            this.StaticFlowPressureTest();
        ///            break;
        ///        default:
        ///            throw new TestTypeNotSupportedException();
        ///    }
        /// }
        /// </example>
        protected abstract OperationResult RunTest(TestType testType);

        /// <summary>
        /// 这个方法用于指示试验结束后应该执行的操作。比如试验结束后停止电机或泵的操作。
        /// </summary>
        public OperationResult AfterTest()
        {
            OperationResult res = null;
            try
            {
                res = AfterTestCustom(new object());
            }
            catch (Exception ex)
            {
                Log.Info("试验结束,发生异常！");
                throw ex;
            }
            finally
            {
                OnTestEnd();

            }
            Log.Info("试验结束,请打印实验报告...");
            testFinished = true;
            return res;
        }

        public abstract OperationResult AfterTestCustom(object testInfo);

        #endregion

        #region Report Generation

        /// <summary>
        /// 生成试验报告。
        /// </summary>
        public abstract void GenerateReport();

        #endregion

        public class TestTypeNotSupportedException : Exception
        {

        }
    }

    public enum TestType
    {
        耐压试验,
        外泄漏试验,
        稳态流量压力特性试验,
        /* $mark_testtype$ */
    }

    public enum CircuitState
    {
        /* $mark_circuit$ */
    }

    /// <summary>
    /// 表示试验运行状态：停止、正在运行和暂停。
    /// </summary>
    public enum RunningState
    {
        Stop,
        Running,
        Suspend,
    }
}
