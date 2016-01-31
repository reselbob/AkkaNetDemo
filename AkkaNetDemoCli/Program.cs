using System;
using System.Text.RegularExpressions;
using AkkaNetDemo;
using AkkaNetDemo.Messages;
using Newtonsoft.Json;

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
                    Console.WriteLine("Enter Ticker Symbol, Number of Shares to Trade, and Trade TradeType (B/S)");
                    Console.WriteLine("Example: HP,200,S   [Enter Q to quit]");
                    Console.Write("Enter Trade: ");
                    console = Console.ReadLine();
                }
                //TODO, put in validation for input

                //Strip out the white spaces
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

                //Call the static object, Trading System. TradingSystem will return the response
                //message of the Trade.
                Trade responseTradeMessage = TradingSystem.Trade(ticker, shares, tradeType);

                Console.WriteLine("The following is the result of your trade:\n");
                //Convert the response Trade message into a JSON string and show it
                var startColor = Console.ForegroundColor;
                //Make the console text RED upon failure
                if (responseTradeMessage.TradeStatus.Equals(TradeStatus.Fail))
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(JsonConvert.SerializeObject(responseTradeMessage,Newtonsoft.Json.Formatting.Indented));
                Console.ForegroundColor = startColor;
            } while (true);
        }
    }
}