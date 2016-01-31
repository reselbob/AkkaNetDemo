using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaNetDemo.Messages;
using Newtonsoft.Json;

namespace AkkaNetDemo
{

    public class TradingSystem
    {
        //private static readonly IActorRef Manager;

        static TradingSystem()
        {
           
        }

        public static void Trade(string ticker, int shares, TradeType tradeType)
        {

            var tradingSystem = ActorSystem.Create("TradingSystem");
            var broker = tradingSystem.ActorOf(Props.Create(() => new StockBroker()), "MyBroker");
            
            var trade = new Messages.Trade(ticker, shares, tradeType,TradeStatus.Open);
            var task = broker.Ask<Trade>(trade);
            task.Wait();
            Console.WriteLine("The following is the result of your trade:\n");
            Console.WriteLine(JsonConvert.SerializeObject(task.Result, Formatting.Indented));
        }


    }


}
