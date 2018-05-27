using System;
using EF.Core.Abstractions;

namespace EF.Implementations.Entities
{
    public class TradeOperation : IEntity
    {
        public static readonly string Header =
            "------------------------------------------------------------------------------\n" +
            $"{Printer.PrintInCentre("Time")}" +
            $"{Printer.PrintInCentre("Seller")}" +
            $"{Printer.PrintInCentre("Buyer")}" +
            $"{Printer.PrintInCentre("Stocks Type", 20)}" +
            $"{Printer.PrintInCentre("Stocks Qty")}" +
            $"{Printer.PrintInCentre("Deal Value")}";

        public DateTime Time { get; set; }
        public Trader Seller { get; set; }
        public Trader Buyer { get; set; }
        public TradableType TradableType { get; set; }
        public int TradableAmount { get; set; }
        public decimal TradeAmount { get; set; }

        public int Id { get; set; }

        public string GetInfo()
        {
            return $"{Printer.PrintInCentre(Time.ToShortDateString())}" +
                   $"{Printer.PrintInCentre(Seller.TraderInfo.SecondName)}" +
                   $"{Printer.PrintInCentre(Buyer.TraderInfo.SecondName)}" +
                   $"{Printer.PrintInCentre(TradableType.Type, 20)}" +
                   $"{Printer.PrintInCentre(TradableAmount.ToString())}" +
                   $"{Printer.PrintInCentre(string.Format($"{TradeAmount:N0}"))}";
        }

        public static TradeOperationBuilder CreateBuilder()
        {
            return new TradeOperationBuilder();
        }
    }
}