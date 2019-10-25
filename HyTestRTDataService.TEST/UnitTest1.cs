using System;
using System.Threading;
using HyTestRTDataService.ConfigMode;
using HyTestRTDataService.ConfigMode.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HyTestRTDataService.TEST
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new FormConfigManager(new ConfigManager()).Show();
        }
    }
}
