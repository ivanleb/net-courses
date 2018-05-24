using System;
using EF.Core.Repositories;
using EF.Core.Services;
using static EF.Implementations.ConsoleDraw;

namespace EF.Implementations
{
    public class Operation : IId, IGetInfo
    {
        public static readonly string Header = "------------------------------------------------------------------------------\n" +
            $"{PrintInCentre("Date")}{PrintInCentre("Seller")}{PrintInCentre("Buyer")}{PrintInCentre("Stock Type", 20)}{PrintInCentre("Stock Qty")}{PrintInCentre("Deal Value")}";

        public DateTime Date { get; set; }
        public Traider Seller { get; set; }
        public Traider Buyer { get; set; }
        public StockType StockType { get; set; }
        public int StockAmount { get; set; }
        public decimal Cash { get; set; }

        public string GetInfo()
        {
            return $"{PrintInCentre(Date.ToShortDateString())}" +
                   $"{PrintInCentre(Seller.SecondName)}" +
                   $"{PrintInCentre(Buyer.SecondName)}" +
                   $"{PrintInCentre(StockType.Type, 20)}" +
                   $"{PrintInCentre(StockAmount.ToString())}" +
                   $"{PrintInCentre(string.Format($"{Cash:N0}"))}";
        }

        public int Id { get; set; }
    }
}