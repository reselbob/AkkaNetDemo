using System;
using AkkaNetDemo.Messages;

namespace AkkaNetDemo.Exceptions
{
    public class TradeLimitException: Exception
    {
        public Trade Trade { get; set; }
        public DateTime TradeTime { get; set; }
    }
}