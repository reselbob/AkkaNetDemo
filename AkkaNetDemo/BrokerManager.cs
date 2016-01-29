using System;
using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{

    public class FinancialPlanner
    {
        private static readonly IActorRef Manager;

        static FinancialPlanner()
        {
            var tradingSystem = ActorSystem.Create("TradingSystem");
            Manager = tradingSystem.ActorOf(Props.Create(() => new BrokerManager()), "MyManager");
        }

        public static void Trade(string ticker, int shares, TradeType tradeType)
        {
            var trade = new Messages.Trade(ticker, shares,tradeType);
            trade.TradeStatus = TradeStatus.Open;
            trade.TradeHistory.Add(new TradeHistoryItem(DateTime.UtcNow, "Starting Trade", TradeStatus.Open));
            Manager.Tell(trade);
        }
        
        
    }
    
    public class BrokerManager: UntypedActor
    {

        private IActorRef _sellBroker;
        private IActorRef _buyBroker;
        
        public BrokerManager()
        {
            _buyBroker = Context.ActorOf(Props.Create(() => new StockBroker()), "goldman-sachs");
            _sellBroker = Context.ActorOf(Props.Create(() => new StockBroker()), "jp-morgan");
        }

        public void Buy(string ticker, int shares)
        {
            _buyBroker.Tell(new Messages.Trade(ticker, shares, TradeType.Buy)); ;
        }


        public void Sell(string ticker, int shares)
        {
            _sellBroker.Tell(new Messages.Trade(ticker, shares, TradeType.Sell));
        }

        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null)
            {
                if (trade.TradeStatus == TradeStatus.Open)
                {
                    if (trade.Type == TradeType.Buy) Buy(trade.Ticker, trade.Shares);
                    if (trade.Type == TradeType.Sell) Sell(trade.Ticker, trade.Shares);
                }
                else
                {
                    Console.WriteLine("\n[{0}] {1} for {2}", trade.TradeStatus, trade.Message,trade.Type);
                    Console.WriteLine("****History****\n");
                    Console.WriteLine(trade.TradeHistoryAsJson);
                }

            }
        }
    }
}
