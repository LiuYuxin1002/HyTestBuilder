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
    class Class1 : Entity
    {
        //数据库字段部分
        public string Name { get; set; }
        public string Types { get; set; }


        public Class1()
        {
            if (this.TestList == null)
            {
                this.TestList = new List<TestType>();
            }

            this.TestSupportList = new List<TestType>(){
                //这里用于添加试验类型
                $TestType.耐压试验,
$
            }
        }



        protected override OperationResult RunTest(TestType testType)
        {
            switch (testType)
            {
                //判断试验-方法匹配
                case TestType.耐压试验:
                    this.PressureTest();
                    break;

                default:
                    throw new TestTypeNotSupportedException();
            }

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

        private void PressureTest()
        {

        }


        #endregion
    }
}
