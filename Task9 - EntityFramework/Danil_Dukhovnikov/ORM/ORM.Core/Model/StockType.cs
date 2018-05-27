using System.Collections.Generic;

namespace ORM.Core.Model
{
    public sealed class StockType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
