# AkkaNetDemo
Demonstration code for Akka.NET demo

The AkkaNetDemo program, the demonstration program for this article, emulates a trade scenario as follows:
A customer (console) declares a trade that he or she wants to make. The trade is defined as a comma delimited string, with the structure:

IBM,100,B

**Where**

- **IBM** is the ticker symbol
- **100** is the number of shares
- **B** is a symbol indicating Buy. (**S** indicates Sell)

The trade declaration is submitted to an object, TradingSystem via TradingSystem.Trade(), (1, in Figure 2 below). The TradingSystem converts the trade declaration into a Trade message (2).  The TradingSystem passes the message to the StockBroker as an “ask”. The StockBroker determines whether to create a BuyFloorTrader, an instance of the FloorTrader class or a SellFloorTrader, depending on whether the Trade message declares TradeType.Buy or TradeType.Sell. Then the StockBroker forwards the message onto the identified FloorTrader (3). The FloorTrader determines whether the number of shares in the Trade is over or under the trade limit. If under the trade limit, the FloorTrader  makes the trade, then creates a new Trade message indicating success. However, if the number of shares in the trade exceed the trade limit, the FloorTrader creates a new Trade message, setting Trade.TradeStatus to TradeStatus.Fail. (4)
The new Trade me
