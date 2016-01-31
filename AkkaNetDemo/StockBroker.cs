using System;
using Akka.Actor;
using AkkaNetDemo.Exceptions;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class StockBroker : UntypedActor
    {
  
 
        protected override void OnReceive(object message)
        {
            var trade = message as AbstractTrade;

            if (trade != null)
            {
                if (trade.TradeStatus.Equals(TradeStatus.Open))
                {
                    if (trade.Type.Equals(TradeType.Sell))
                    {
                        var sellTrader = Context.ActorOf(Props.Create(() => new Trader()), "MySellTrader");
                        sellTrader.Forward(trade);
                    }
                    else
                    {
                        var buyTrader = Context.ActorOf(Props.Create(() => new Trader()), "buyTrader");
                        buyTrader.Forward(trade);
                    }
                }
                //if (trade.TradeStatus.Equals(TradeStatus.Success) ||trade.TradeStatus.Equals(TradeStatus.Fail))
                //{
                //    Sender.Tell(trade);
                //}
                
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            {
                return new OneForOneStrategy(2, TimeSpan.FromSeconds(30), x =>
                {
                    if (x is TradeLimitException)
                    {
                        //var trade = ((TradeLimitException) x).Trade;
                        //trade.TradeStatus = TradeStatus.Fail;
                        //trade.Message =
                        //    string.Format(
                        //        "The trader says that there is a trade limit violation. You cannot trade {0} shares",
                        //        ((TradeLimitException) x).Trade.Shares);
                        //trade.LastActive = DateTime.UtcNow;
                        //trade.TradeHistory.Add(new TradeHistoryItem(trade.LastActive, trade.Message, trade.TradeStatus,
                        //    Self.Path.ToStringWithAddress()));
                        ////Context.Parent.Tell(trade);
                        ////return Directive.Stop;
                    }

                    return Directive.Resume;
                });
            }
        }
    }
}