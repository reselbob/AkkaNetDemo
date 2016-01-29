using System;
using Akka.Actor;
using AkkaNetDemo.Exceptions;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class Trader : UntypedActor
    {
  
        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null)
            {
                if (trade.Shares > 200)
                {
                    trade.TradeStatus = TradeStatus.Fail;
                    trade.LastActive = DateTime.UtcNow;
                    
                    var exception =  new TradeLimitException {Trade = trade, TradeTime = DateTime.UtcNow};
                    trade.TradeHistory.Add(new TradeHistoryItem(trade.LastActive, exception.ToString(),trade.TradeStatus));
                    throw exception;
                }
                trade.Message = string.Format("I am {0}ing {1} shares of {2}", trade.Type, trade.Shares, trade.Ticker);
                Console.WriteLine(trade.Message);
    
                trade.TradeStatus = TradeStatus.Success;
                trade.LastActive = DateTime.UtcNow;
                trade.TradeHistory.Add(new TradeHistoryItem(trade.LastActive, trade.Message, trade.TradeStatus));

                Context.Parent.Tell(trade);
            }
        }
    }
}