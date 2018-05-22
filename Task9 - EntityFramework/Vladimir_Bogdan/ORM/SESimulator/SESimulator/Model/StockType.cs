using System.Collections.Generic;

namespace SESimulator.Model
{
    public class StockType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
