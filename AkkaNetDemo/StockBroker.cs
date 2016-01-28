using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo
{
    public class StockBroker : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var trade = message as Trade;
            if (trade != null)
            {
                IActorRef trader = Context.ActorOf(Props.Create(() => new Trader()), "ralph");
                trader.Tell(trade);   
            }
        }
    }
}
