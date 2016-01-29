using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AkkaNetDemo.Messages
{
    
    public class Trade
    {
        public Trade(string ticker, int shares, TradeType tradeType)
        {
            Ticker = ticker;
            Shares = shares;
            Type = tradeType;
            Id = Guid.NewGuid();
            TradeStatus = TradeStatus.Open;
            LastActive = DateTime.UtcNow;
            TradeHistory = new List<TradeHistoryItem>
            {
                new TradeHistoryItem(DateTime.UtcNow, string.Empty, TradeStatus.Initialize)
            };
        }

        public string Ticker { get; private set; }
        public int Shares { get; private set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType Type { get; private set; }
       [JsonConverter(typeof(StringEnumConverter))]
        public TradeStatus TradeStatus { get; set; }
        public Guid Id { get; private set; }
        public DateTime LastActive { get; set; }
        public string Message { get; set; }
        public List<TradeHistoryItem> TradeHistory;

        public string TradeHistoryAsJson
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(TradeHistory, Formatting.Indented); }
        }
    }

    public enum TradeType
    {
        Buy,
        Sell
    }

    public enum TradeStatus
    {
        Initialize,
        Open,
        Success,
        Fail
    }

}