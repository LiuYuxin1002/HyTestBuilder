using System.Collections.Generic;
using HyTestRTDataService;

namespace StandardTemplate.Test
{
    public class Valve2 : Entity
    {
        //数据库字段部分
        public string Types { get; set; }
        public double Pressure { get; set; }
        public double Flow { get; set; }


        public Valve2()
        {
            if (this.TestList == null)
            {
                this.TestList = new List<TestType>();
            }

            this.TestSupportList = new List<TestType>(){
                //这里用于添加试验类型
                TestType.耐压试验,
                TestType.内泄露试验,
                TestType.恒定阀压降输出流量输入信号特性试验,
                TestType.节流调节特性试验,
                TestType.输出流量负载压差特性试验,
                TestType.输出流量阀压降特性试验,
                TestType.极限功率特性试验,
            };
        }




        protected override OperationResult RunTest(TestType testType)
        {
            switch (testType)
            {
                //判断试验-方法匹配
                case TestType.耐压试验:
                    this.PressureTest();
                    break;
                case TestType.稳态流量压力特性试验:
                    this.FlowTest();
                    break;

                default:
                    throw new TestTypeNotSupportedException();
            }
            return new OperationResult();
        }

        public override OperationResult AfterTestCustom(object testInfo)
        {
            Log.Info("试验结束");
            return new OperationResult();
        }

        public override OperationResult BeforeTestCustom(object testInfo)
        {
            Log.Info("试验开始");
            return new OperationResult();
        }

        public override void GenerateReport()
        {
            
        }

        #region TEST

        private void PressureTest()
        {
            Log.Info("耐压试验开始");
        }
        private void FlowTest()
        {
            Log.Info("稳态压力流量试验开始");
        }


        #endregion
    }
}
