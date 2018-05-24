using EF.Core.Repositories;
using EF.Core.Services;
using static EF.Implementations.ConsoleDraw;

namespace EF.Implementations
{
    public class Stock : IId, IGetInfo
    {
        public virtual StockType StockType { get; set; }
        public int StockTypeId { get; set; }
        public virtual Traider Traider { get; set; }
        public int TraiderId { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        public static readonly string Header =
            $"{PrintInCentre("ID")}{PrintInCentre("Owner Surname")}{PrintInCentre("Type")}{PrintInCentre("Qty")}";

        public string GetInfo()
        {
            return
                $"{PrintInCentre(Id.ToString())}{PrintInCentre(Traider.SecondName)}{PrintInCentre(StockType.Type)}{PrintInCentre(Quantity.ToString())}";
        }
    }
}