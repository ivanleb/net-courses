using System;
using System.Collections.Generic;

namespace EntityCore.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public ClientZoneOfBalance Zone { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} balance {Balance} ";
        }
    }

    public enum ClientZoneOfBalance
    {
        Green, Orange, Black
    }
}
