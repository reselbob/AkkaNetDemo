using System;
using System.Threading;
using System.Threading.Tasks;
using AkkaNetDemo;
using AkkaNetDemo.Messages;

namespace AkkaNetDemoCli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ticker = string.Empty;
            var shares = 0;
            var tradeType = TradeType.Buy;

            do
            {
                Console.WriteLine("Enter Ticker Symbol, Number of Shares to Trade, and Trade Type (B/S)");
                Console.WriteLine("Example: HP,200,S   [Enter Q to quit]" );
                Console.Write("Enter Trade: ");
                var console = Console.ReadLine();
                if (console.ToLower().Equals("q")) break;
                var tradeInfo = console.Split(',');
                //At some point validate the input

                //Tra
                ticker = tradeInfo[0];
                var i = 0;
                if (int.TryParse(tradeInfo[1], out i))
                {
                    shares = i;
                }

                if (tradeInfo[2].ToLower().Equals("b"))
                {
                    tradeType = TradeType.Buy;
                }
                else if (tradeInfo[2].ToLower().Equals("s"))
                {
                    tradeType = TradeType.Sell;
                }

                Console.WriteLine("I will now try to {0} {1} shares of {2}", tradeType, shares, ticker);

                FinancialPlanner.Trade(ticker, shares, tradeType);

                //if (tradeType.Equals(TradeType.Buy))
                //{
                //    house.Buy(ticker, shares);
                //}
                //else
                //{
                //    var task = house.Sell(ticker, shares);
                //    //task.Wait(2000);
                //    Console.WriteLine("-----Selling done------");
                //    task.ContinueWith((t) =>
                //    {
                //        //task.Wait(2000);s
                //        Console.WriteLine("---xxxx--Selling done---xxxx---");
                //    });
                   
                //}  
                //Thread.Sleep(2000);
            } while (true);
        }
    }
}