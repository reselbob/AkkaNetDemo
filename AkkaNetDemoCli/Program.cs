using System;
using System.Text.RegularExpressions;
using AkkaNetDemo;
using AkkaNetDemo.Messages;

namespace AkkaNetDemoCli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var shares = 0;
            var tradeType = TradeType.Buy;

            do
            {
                var console = string.Empty;
                while (string.IsNullOrEmpty(console))
                {
                    Console.WriteLine("Enter Ticker Symbol, Number of Shares to Trade, and Trade Type (B/S)");
                    Console.WriteLine("Example: HP,200,S   [Enter Q to quit]");
                    Console.Write("Enter Trade: ");
                    console = Console.ReadLine();
                }
                //TODO, put in validation for input

                console = Regex.Replace(console, @"\s+", "");
                if (console.ToLower().Equals("q")) break;
                var tradeInfo = console.Split(',');

                var ticker = tradeInfo[0];
                int i;
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

                TradingSystem.Trade(ticker, shares, tradeType);
            } while (true);
        }
    }
}