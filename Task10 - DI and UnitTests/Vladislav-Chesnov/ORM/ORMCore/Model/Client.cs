using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Model
{
    public enum Zone
    {
        Green = 1,
        Orange,
        Black
    }

    public class Client : IClient
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public Zone ClientZone { get; set; } = Zone.Green;

        public ICollection<Stock> ClientStocks { get; set; } = new List<Stock>();

        public ICollection<Stock> ClientStocksForSale { get { return ClientStocks; } }

        public override string ToString()
        {
            string clientStocks = "";

            foreach (var stockType in ClientStocks.Select(s => s.Type).Distinct())
            {
                clientStocks += $"{ClientStocks.Count(s => s.Type == stockType)} stocks of {stockType.Name} company\n";
            }

            return $"{Name} {Surname}, phone number: {PhoneNumber}, \nclient stocks:\n{clientStocks}";
        }
    }
}
