﻿using HyTestRTDataService;
using log4net;
using StandardTemplate.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardTemplate.Test
{
    public class Valve : Entity
    {
        private ILog Log = LogManager.GetLogger(typeof(Valve)); //日志定义
        Dictionary<string, string> Pict = new Dictionary<string, string>();

        public Valve()
        {
            if (this.TestList == null)
            {
                this.TestList = new List<TestType>();
            }
            this.TestSupportList = new List<TestType>
            {
                TestType.外泄漏试验,
                TestType.稳态流量压力特性试验,
                TestType.耐压试验,
            };
            Log.Info("想写的日志");  //日志使用
        }

        protected override OperationResult RunTest(TestType testType)
        {
            switch (testType)
            {
                case TestType.耐压试验:
                    this.EndurationTest();
                    break;
                case TestType.外泄漏试验:
                    this.ExternalLeakTest();     ///实验程序
                    break;
                case TestType.稳态流量压力特性试验:
                    this.StaticFlowPressureTest();
                    break;
                default:
                    throw new TestTypeNotSupportedException();
            }
            return new OperationResult();
        }

        /// <summary>
        /// 耐压试验
        /// </summary>
        public void EndurationTest()
        {

        }

        /// <summary>
        /// 外泄漏试验
        /// </summary>
        public void ExternalLeakTest()
        {
            
        }

        /// <summary>
        /// 稳态流量压力特性试验
        /// </summary>
        public void StaticFlowPressureTest()
        {

        }

        public override void GenerateReport()
        {
            
        }

        public override OperationResult BeforeTestCustom(object testInfo)
        {
            throw new NotImplementedException();
        }

        public override OperationResult AfterTestCustom(object testInfo)
        {
            throw new NotImplementedException();
        }
    }
}
