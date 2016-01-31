using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    /// <summary>
    /// This class is the root level System for Stock Trading. 
    /// </summary>
    public class TradingSystem
    {
        /// <summary>
        /// This method starts the trade messaging process. A
        /// top level Akka.NET <see cref="ActorSystem"/> object,
        /// tradingSystem is created. Also, a <see cref="Trade"/> message
        /// is defined. The ActorSystem creates the <see cref="StockBroker"/> to
        /// execute the trade.
        /// 
        /// NOTICE: The StockBroker uses <see cref="UntypedActor"/>.Ask(message) to
        /// execute the trade. <see cref="UntypedActor"/>.Ask(message) returns the
        /// underlying <see cref="Task"/> doing the asking. We'll Task.Wait() for the
        /// ask to complete. The response <see cref="Trade"/> message of the trade 
        /// will be reflected in the Task.Result
        /// </summary>
        /// <param name="ticker">The ticker symbol of the stock you want to trade</param>
        /// <param name="shares">The number of shares to trade</param>
        /// <param name="tradeType">Describes the trade to
        /// execute.<see cref="TradeType"/>.Buy or TradeType.Sell.</param>
        /// <returns>The response <see cref="Trade"/> message</returns>
        public static Trade Trade(string ticker, int shares, TradeType tradeType)
        {

            //Create the ActorSystem
            var tradingSystem = ActorSystem.Create("TradingSystem");
            //Create the StockBroker
            var broker = tradingSystem.ActorOf(Props.Create(() => new StockBroker()), "MyBroker");
            //Create the Trade message
            var trade = new Messages.Trade(ticker, shares, tradeType,TradeStatus.Open,Guid.NewGuid());
            //Send the Trade message to the broker by way of an Ask(message). We want the underlying Task. 
            //Notice also that in Ask we are providing the Trade message as a generic type. This
            //is how the Task knows how to pass the response Trade message out to the caller.
            var task = broker.Ask<Trade>(trade);
            //Wait for the Task to complete. The task is completed upon the Broker receiving
            //response Trade message.
            task.Wait();
            //Return the response Trade message.
            return task.Result;
        }
    }
}
