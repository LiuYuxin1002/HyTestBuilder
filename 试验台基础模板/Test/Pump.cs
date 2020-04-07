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
    class Pump : Entity
    {
        //数据库字段部分
        public int Id { get; set; }
        public string Name { get; set; }


        public Pump()
        {
            if (this.TestList == null)
            {
                this.TestList = new List<TestType>();
            }

            this.TestSupportList = new List<TestType>(){
                //这里用于添加试验类型
                TestType.试运行试验,
                TestType.启动试验,
            };
        }



        protected override OperationResult RunTest(TestType testType)
        {
            switch (testType)
            {
                //判断试验-方法匹配
                case TestType.试运行试验:
                    this.XXXTest();
                    break;
                case TestType.启动试验:
                    this.XXXXTest();
                    break;

                default:
                    throw new TestTypeNotSupportedException();
            }
            return new OperationResult();
        }

        public override OperationResult AfterTestCustom(object testInfo)
        {
            return new OperationResult();
        }

        public override OperationResult BeforeTestCustom(object testInfo)
        {
            return new OperationResult();
        }

        public override void GenerateReport()
        {
            throw new NotImplementedException();
        }

        #region TEST

        private void XXXTest()
        {
            //试验1流程
        }
        private void XXXXTest()
        {
            //试验2流程
        }


        #endregion
    }
}
