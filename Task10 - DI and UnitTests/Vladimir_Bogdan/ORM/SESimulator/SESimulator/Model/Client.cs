using SESimulator.Core;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SESimulator.Model
{
    public partial class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public string Zone { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
