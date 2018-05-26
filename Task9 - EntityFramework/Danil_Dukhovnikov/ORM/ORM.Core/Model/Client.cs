using System.Collections.Generic;
using System.Linq;

namespace ORM.Core.Model
{
    public sealed class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }

        public string Zone { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        
        public override string ToString()
        {
            var assets = this.Stocks.GroupBy(stock => stock.TypeId)
                .Aggregate("", (current, asset) => current + $"\t {asset.Key}: {asset.Count()} item(s)\n");

            return $"{this.Name} {this.Surname} {this.PhoneNumber}\nAssets: {this.Balance}$\n{assets}";
        }
    }
}
