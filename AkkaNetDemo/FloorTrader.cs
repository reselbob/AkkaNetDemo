using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    /// <summary>
    /// This class describes a Floor Trader, someone who buys and sells
    /// stocks on the trading floor of a Stock Exchange
    /// </summary>
    public class FloorTrader : UntypedActor
    {
        private const int TradingLimit = 200;

        /// <summary>
        /// This overrideen method provides the behavior a Floor Trader executes
        /// upon receipt of a message. If the number of shares
        /// in the <see cref="Trade"/> message is under the TradingLimit, the Floor
        /// Trader executes the trade, then creates a new Trade message with
        /// a <see cref="TradeStatus"/> = TradeStatus.Success.
        /// 
        /// Otherwise, a new Trade message is created with a TradeStatus = TradeStatus.Fail.
        /// </summary>
        /// <param name="message">The message for the Floor Trader to process upon receipt</param>
        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null)
            {
                Trade responseTrade;
                if (trade.Shares > TradingLimit)
                {
                    var msg =
                        string.Format(
                            "You want to trade {0} Shares. The trade exceeds the trading limit of {1}. The system will not {2} {0} of {3}.",
                            trade.Shares, TradingLimit, trade.TradeType, trade.Ticker);
                    responseTrade = new Trade(trade.Ticker, trade.Shares, trade.TradeType, TradeStatus.Fail, trade.TadingSessionId,msg);
                }
                else
                {
                    var msg = string.Format("I am {0}ing {1} shares of {2}", trade.TradeType, trade.Shares, trade.Ticker);
                    responseTrade = new Trade(trade.Ticker, trade.Shares, trade.TradeType, TradeStatus.Success, trade.TadingSessionId,msg);
                }
                Sender.Tell(responseTrade, Self);
            }
        }
    }
}