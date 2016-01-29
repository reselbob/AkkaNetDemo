using AkkaNetDemo;
using AkkaNetDemo.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaNetDemoTest
{
    [TestClass]
    public class AkkaNetTests
    {
        [TestMethod]
        public void BuyTest()
        {
            FinancialPlanner.Trade("IBM", 125, TradeType.Buy);
        }

        [TestMethod]
        public void OverTradeLimitTest()
        {
            FinancialPlanner.Trade("IBM", 500, TradeType.Buy);
        }
    }
}