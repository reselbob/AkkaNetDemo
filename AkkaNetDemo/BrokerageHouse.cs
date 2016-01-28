using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class BrokerageHouse
    {
        private ActorSystem _tradingSystem;

        public BrokerageHouse()
        {
             _tradingSystem = ActorSystem.Create("TradingSystem");
        }

        public void Buy(string ticker, int shares)
        {
            var broker = _tradingSystem.ActorOf(Props.Create(() => new StockBroker()), "goldman-sachs");
            broker.Tell(new Messages.Trade(ticker,shares, Trade.TradeType.Buy ));
        }


        public void Sell(string ticker, int shares)
        {
            var broker = _tradingSystem.ActorOf(Props.Create(() => new StockBroker()), "jp-morgan");
            broker.Tell(new Messages.Trade(ticker, shares, Trade.TradeType.Sell));
        }
        
    }
}
