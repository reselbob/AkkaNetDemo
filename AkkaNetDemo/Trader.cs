using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class Trader : UntypedActor
    {
        private const int TradingLimit = 200;

        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null )
            {
                if (trade.Shares > TradingLimit)
                {
                    var msg =
                        string.Format(
                            "You want to trade {0} Shares. The trade exceeds the trading limit of {1}. The system will not {2} {0} of {3}.",
                            trade.Shares, TradingLimit, trade.Type, trade.Ticker);

                    Sender.Tell(new Trade(trade.Ticker, trade.Shares, trade.Type, TradeStatus.Fail, msg), Self);
                }
                else
                {
                    var msg = string.Format("I am {0}ing {1} shares of {2}", trade.Type, trade.Shares, trade.Ticker);
                    Sender.Tell(new Trade(trade.Ticker, trade.Shares, trade.Type, TradeStatus.Success, msg), Self);
                }
            }
        }
    }
}