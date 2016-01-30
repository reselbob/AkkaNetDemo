using System;
using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{

 
    public class BrokerManager: UntypedActor
    {

        private readonly IActorRef _sellBroker;
        private readonly IActorRef _buyBroker;
        
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
                    Console.WriteLine("****Trading****\n");
                //    if (trade.Type == TradeType.Buy) Buy(trade.Ticker, trade.Shares);
                //    if (trade.Type == TradeType.Sell) Sell(trade.Ticker, trade.Shares);
                    Sender.Tell(trade);
                }
                else
                {
                    Console.WriteLine("\n[{0}] {1} for {2}", trade.TradeStatus, trade.Message,trade.Type);
                    Console.WriteLine("****History****\n");
                    Console.WriteLine(trade.TradeHistoryAsJson); Context.Parent.Forward(trade);
                }

                

            }
        }
    }
}
