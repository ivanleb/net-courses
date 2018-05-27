using EF.Core.Abstractions;

namespace EF.Implementations.Entities
{
    public class Stock : IEntity
    {
        public static readonly string Header =
            $"{Printer.PrintInCentre("ID")}" +
            $"{Printer.PrintInCentre("Owner Surname")}" +
            $"{Printer.PrintInCentre("Type")}" +
            $"{Printer.PrintInCentre("Qty")}";

        public virtual TradableType TradableType { get; set; }
        public int TradableTypeId { get; set; }
        public virtual Trader Trader { get; set; }
        public int TraderId { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        public string GetInfo()
        {
            return
                $"{Printer.PrintInCentre(Id.ToString())}" +
                $"{Printer.PrintInCentre(Trader.TraderInfo.SecondName)}" +
                $"{Printer.PrintInCentre(TradableType.Type)}" +
                $"{Printer.PrintInCentre(Quantity.ToString())}";
        }
    }
}