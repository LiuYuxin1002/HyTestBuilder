using System;
using System.Collections.Generic;
using System.Text;
using HyTestRTDataService;
using HyTestRTDataService;
using StandardTemplate.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardTemplate.Test
{
    class Cyclinder : Entity
    {
        //数据库字段部分
        public string 油缸型号 { get; set; }
        public double 试运行速度 { get; set; }
        public double 试运行压力 { get; set; }
        public int 试运行次数 { get; set; }
        public double 起动压力变化速度 { get; set; }
        public double 起动最高压力 { get; set; }
        public double 耐压压力 { get; set; }
        public double 耐压时间 { get; set; }
        public double 缓冲速度 { get; set; }
        public double 缓冲压力 { get; set; }
        public double 冲击压力 { get; set; }
        public double 冲击频率 { get; set; }
        public double 冲击时间 { get; set; }


        public Cyclinder()
        {
            if (this.TestList == null)
            {
                this.TestList = new List<TestType>();
            }

            this.TestSupportList = new List<TestType>(){
                //这里用于添加试验类型
                TestType.试运行试验,
                TestType.起动试验,
                TestType.耐压泄露试验,
                TestType.油缸缓冲试验,
                TestType.冲击试验,

            };
        }



        protected override OperationResult RunTest(TestType testType)
        {
            switch (testType)
            {
                //判断试验-方法匹配
                case TestType.试运行试验:
                    this.TrialTest();
                    break;
                case TestType.起动试验:
                    this.FirstStartupTest();
                    break;
                case TestType.耐压泄露试验:
                    this.PressureLeakTest();
                    break;
                case TestType.油缸缓冲试验:
                    this.SnubberTest();
                    break;
                case TestType.冲击试验:
                    this.ImpulseTest();
                    break;

                default:
                    throw new TestTypeNotSupportedException();
            }
            return new OperationResult();
        }

        public override OperationResult AfterTestCustom(object testInfo)
        {
            throw new NotImplementedException();
        }

        public override OperationResult BeforeTestCustom(object testInfo)
        {
            throw new NotImplementedException();
        }

        public override void GenerateReport()
        {
            throw new NotImplementedException();
        }

        #region TEST

        private void TrialTest()
        {

        }
        private void FirstStartupTest()
        {

        }
        private void PressureLeakTest()
        {

        }
        private void SnubberTest()
        {

        }
        private void ImpulseTest()
        {

        }


        #endregion
    }
}
