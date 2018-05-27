using System.Collections.Generic;
using EF.Core.Abstractions;

namespace EF.Implementations.Entities
{
    public class Trader : IEntity
    {
        public static readonly string Header = "--------------------------------------------------------\n" +
                                               $"{Printer.PrintInCentre("Second Name")}" +
                                               $"{Printer.PrintInCentre("First Name")}" +
                                               $"{Printer.PrintInCentre("Phone", 15)}" +
                                               $"{Printer.PrintInCentre("Balance", 8)}" +
                                               $"{Printer.PrintInCentre("Status")}";

        public string Status { get; set; }
        public decimal Balance { get; set; }

        public IndividualInfo TraderInfo { get; set; }

        public ICollection<Stock> Assets { get; set; }

        public int Id { get; set; }

        public string GetInfo()
        {
            return
                $"{Printer.PrintInCentre(TraderInfo.SecondName)}" +
                $"{Printer.PrintInCentre(TraderInfo.FirstName)}" +
                $"{Printer.PrintInCentre(TraderInfo.ContactPhoneNumber, 15)}" +
                $"{Printer.PrintInCentre(string.Format($"{Balance:N0}"), 8)}" +
                $"{Printer.PrintInCentre(Status)}";
        }
    }
}