using System;
using Akka.Actor;
using AkkaNetDemo.Exceptions;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class StockBroker : UntypedActor
    {
        private readonly IActorRef _trader;
        
        public StockBroker()
        {
            _trader = Context.ActorOf(Props.Create(() => new Trader()), "MyFloorTrader");
        }
       
        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            
            if (trade != null)
            {
                if (trade.TradeStatus == TradeStatus.Open)
                {
                    trade.Message =
                       string.Format("I am a Broker, {0}. I am making a {1}.", Self.Path, trade.Type);
                    trade.LastActive = DateTime.UtcNow;
                    trade.TradeHistory.Add(new TradeHistoryItem(trade.LastActive, trade.Message, trade.TradeStatus));


                    _trader.Tell(trade);
                }
                if (trade.TradeStatus == TradeStatus.Success)
                {
                    trade.Message =
                        string.Format("Congratulations! Your trade of {0} shares of {1} completed successfully",
                            trade.Shares, trade.Ticker);
                    trade.LastActive = DateTime.UtcNow;
                    trade.TradeHistory.Add(new TradeHistoryItem(trade.LastActive, trade.Message, trade.TradeStatus));
                    Context.Parent.Tell(trade);
                }

            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            {
                return new OneForOneStrategy(2, TimeSpan.FromSeconds(30), x =>
                {
                    if (x is TradeLimitException)
                    {
                        var trade = ((TradeLimitException) x).Trade;
                        trade.TradeStatus = TradeStatus.Fail;
                        trade.Message =
                            string.Format(
                                "The trader says that there is a trade limit violation. You cannot trade {0} shares",
                                ((TradeLimitException) x).Trade.Shares);
                        Context.Parent.Tell(trade);
                        //return Directive.Resume;
                    }

                    return Directive.Resume;
                });
            }
        }
    }
}