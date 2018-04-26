using ORMCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMConsoleApp.Implementations
{
    public class Client //: IClient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public string Area { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
