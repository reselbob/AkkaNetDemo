using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AkkaNetDemo.Messages
{
    public abstract class AbstractTrade
    {
        protected AbstractTrade(string ticker, int shares, TradeType tradeType, TradeStatus tradeStatus, string message = null)
        {
            Ticker = ticker;
            Shares = shares;
            Type = tradeType;
            Id = Guid.NewGuid();
            CreateTime = DateTime.UtcNow;
            TradeStatus = tradeStatus;
            Message = message;
        }


        public string Ticker { get; private set; }
        public int Shares { get; private set; }

        [JsonConverter(typeof (StringEnumConverter))]
        public TradeType Type { get; private set; }
         [JsonConverter(typeof(StringEnumConverter))]
        public TradeStatus TradeStatus { get; private set; }
        public Guid Id { get; private set; }
        public DateTime CreateTime { get; private set; }
        //public AbstractTrade OrginatingTrade { get; private set; }
        public string Message { get; private set; }

        //public string AsJson
        //{
        //    get { return JsonConvert.SerializeObject(this, Formatting.Indented); }
        //}
    }


    public class Trade : AbstractTrade
    {
        public Trade(string ticker, int shares, TradeType tradeType, TradeStatus tradeStatus, string message = null) : base(ticker, shares, tradeType, tradeStatus, message)
        {
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