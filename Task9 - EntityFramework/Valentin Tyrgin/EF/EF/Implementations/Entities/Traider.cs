using System.Collections.Generic;
using System.Linq;
using EF.Core.Repositories;
using EF.Core.Services;
using static EF.Implementations.ConsoleDraw;

namespace EF.Implementations
{
    public class Traider : IId, IGetInfo
    {
        public static readonly string Header = "--------------------------------------------------------\n"+
            $"{PrintInCentre("Second Name")}{PrintInCentre("First Name")}{PrintInCentre("Phone", 15)}{PrintInCentre("Balance", 8)}{PrintInCentre("Status")}";

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

        public string GetInfo()
        {
            return
                $"{PrintInCentre(SecondName)}{PrintInCentre(FirstName)}{PrintInCentre(Phone, 15)}{PrintInCentre(string.Format($"{Balance:N0}"), 8)}{PrintInCentre(Status)}";
        }

        public int Id { get; set; }
    }
}