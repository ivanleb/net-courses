using System;

namespace EF.Implementations.Entities
{
    public class TradeOperationBuilder
    {
        private TradeOperation tradeOperation;

        public TradeOperationBuilder()
        {
            tradeOperation = new TradeOperation();
        }

        public TradeOperationBuilder SetTime(DateTime time)
        {
            tradeOperation.Time = time;
            return this;
        }

        public TradeOperationBuilder SetBuyer(Trader traider)
        {
            tradeOperation.Buyer = traider;
            return this;
        }

        public TradeOperationBuilder SetSeller(Trader traider)
        {
            tradeOperation.Seller = traider;
            return this;
        }

        public TradeOperationBuilder SetTradableType(TradableType tradableType)
        {
            tradeOperation.TradableType = tradableType;
            return this;
        }

        public TradeOperationBuilder SetTradableAmount(int tradableAmount)
        {
            tradeOperation.TradableAmount = tradableAmount;
            return this;
        }

        public TradeOperationBuilder SetTradeAmount(decimal tradeAmount)
        {
            tradeOperation.TradeAmount = tradeAmount;
            return this;
        }

        public static implicit operator TradeOperation(TradeOperationBuilder builder)
        {
            return builder.tradeOperation;
        }
    }
}