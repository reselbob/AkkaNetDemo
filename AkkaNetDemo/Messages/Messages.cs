using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AkkaNetDemo.Messages
{
    /// <summary>
    /// This is the abtract class from which all Trade messages
    /// are to inherit.
    /// </summary>
    public abstract class AbstractTrade
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="ticker">The stock ticker symbol</param>
        /// <param name="shares">The number of shares to trade</param>
        /// <param name="tradeTradeType">TradeType.Buy or TradeType.Sell</param>
        /// <param name="tradeStatus">
        ///     A TradeStatus enum that describes the status of the
        ///     Trade
        /// </param>
        /// <param name="tadingSessionId">
        ///     Each trading session is given a unique identifier of type, Guid.
        ///     Either create a TradingSessionId if you are starting a trade. Or pass in the existing
        ///     TradingSessionId if you are part of a trade.
        /// </param>
        /// <param name="message">An arbitrary message</param>
        protected AbstractTrade(string ticker, int shares, TradeType tradeTradeType, TradeStatus tradeStatus,
            Guid tadingSessionId, string message = null)
        {
            Ticker = ticker;
            Shares = shares;
            TradeType = tradeTradeType;
            TadingSessionId = tadingSessionId;
            CreateTime = DateTime.UtcNow;
            TradeStatus = tradeStatus;
            Message = message;
        }


        /// <summary>
        ///The stock ticker symbol
        /// </summary>
        public string Ticker { get; private set; }

        /// <summary>
        /// The number of shares to trade
        /// </summary>
        public int Shares { get; private set; }
        /// <summary>
        /// The type of trade, TradeType.Buy or TradeType.Sell
        /// </summary>
        [JsonConverter(typeof (StringEnumConverter))]
        public TradeType TradeType { get; private set; }
        /// <summary>
        /// The status of the trade, for example, TradeStatus.Open
        /// </summary>
        [JsonConverter(typeof (StringEnumConverter))]
        public TradeStatus TradeStatus { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid TadingSessionId { get; private set; }
        /// <summary>
        /// The time the TrademMessage was created,
        /// </summary>
        public DateTime CreateTime { get; private set; }
        /// <summary>
        /// An arbitrary string that provides additional or descriptive
        /// information about the message
        /// </summary>
        public string Message { get; private set; }
    }

    /// <summary>
    /// </summary>
    public class Trade : AbstractTrade
    {
        public Trade(string ticker, int shares, TradeType tradeTradeType, TradeStatus tradeStatus, Guid tadingSessionId,
            string message = null) : base(ticker, shares, tradeTradeType, tradeStatus, tadingSessionId, message)
        {
        }
    }


    /// <summary>
    /// </summary>
    public enum TradeType
    {
        Buy,
        Sell
    }

    /// <summary>
    /// </summary>
    public enum TradeStatus
    {
        Initialize,
        Open,
        Success,
        Fail
    }
}