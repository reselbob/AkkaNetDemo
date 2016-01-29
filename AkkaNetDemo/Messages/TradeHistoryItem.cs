using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AkkaNetDemo.Messages
{
    public class TradeHistoryItem
    {
        public TradeHistoryItem(DateTime timestamp, string message, TradeStatus status)
        {
            Timestamp = timestamp;
            Message = message;
            Status = status;
        }
        
        public DateTime Timestamp { get; private set; }
        public string Message { get; private set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TradeStatus Status { get; private set; }
    }
}
