namespace AkkaNetDemo.Messages
{
    public class Trade
    {
        public enum TradeType{Buy, Sell}
        public Trade(string ticker, int shares, TradeType tradeType)
        {
            Ticker = ticker;
            Shares = shares;
            Type = tradeType;
        }
        public string Ticker { get; private set; }
        public int Shares { get; private set; }
        public TradeType Type { get; private set; }
       }
    }
