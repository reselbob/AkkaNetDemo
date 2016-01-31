using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    /// <summary>
    ///     An object the describes a Stock Broker. A StockBroker will
    ///     use an <see cref="FloorTrader" /> to execute a trade.
    /// </summary>
    public class StockBroker : UntypedActor
    {
        /// <summary>
        ///     This overrideen method provides the behavior a StockBroker executes
        ///     upon receipt of a message. If the <see cref="Trade" /> message's
        ///     <see cref="TradeType" /> = TradeType.Sell, the StockBroker
        ///     creates a Sell <see cref="FloorTrader" /> and calls
        ///     <see cref="UntypedActor" />.Forward() to pass the Trade message
        ///     on.
        ///     If the Trade is TradeType.Buy, a Buy Floor Trader is created and
        ///     the Trade message is forwarded.
        /// </summary>
        /// <param name="message">The <see cref="Trade" /> message to process</param>
        protected override void OnReceive(object message)
        {
            var trade = message as AbstractTrade;

            if (trade != null)
            {
                //Make sure you are processing only open trades
                if (trade.TradeStatus.Equals(TradeStatus.Open))
                {
                    if (trade.TradeType.Equals(TradeType.Sell))
                    {
                        //create the Sell FloorTrader and forward the message
                        var sellTrader = Context.ActorOf(Props.Create(() => new FloorTrader()), "SellFloorTrader");
                        sellTrader.Forward(trade);
                    }
                    else
                    {
                        //create the Buy FloorTrader and forward the message
                        var buyTrader = Context.ActorOf(Props.Create(() => new FloorTrader()), "BuyFloorTrader");
                        buyTrader.Forward(trade);
                    }
                }
            }
        }
    }
}