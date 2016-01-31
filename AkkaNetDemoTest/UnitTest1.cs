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
            TradingSystem.Trade("IBM", 125, TradeType.Buy);
        }

        [TestMethod]
        public void OverTradeLimitTest()
        {
            TradingSystem.Trade("IBM", 500, TradeType.Buy);
        }
    }
}