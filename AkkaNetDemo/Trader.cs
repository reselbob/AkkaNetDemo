using System;
using Akka.Actor;
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
                Console.WriteLine(string.Format("I need to {0} {1} shares of {2}", trade.Type, trade.Shares, trade.Ticker));
            }
        }


    }
}
