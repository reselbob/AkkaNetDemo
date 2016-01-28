using System;
using AkkaNetDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaNetDemoTest
{
    [TestClass]
    public class AkkaNetTests
    {
        [TestMethod]
        public void BuyTest()
        {
            var house = new BrokerageHouse();
            house.Buy("IBM", 125);
        }
    }
}
