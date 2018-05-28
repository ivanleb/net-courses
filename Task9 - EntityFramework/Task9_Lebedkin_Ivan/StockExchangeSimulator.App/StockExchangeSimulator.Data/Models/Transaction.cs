using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeSimulator.Data.Models
{
    public class Transaction : IAggregateRoot
    {
        public Int32 Id { get; set; }

        public Client Seller { get; set; }

        public Client Buyer { get; set; }

        public Int32 StocksQuantity { get; set; }

        public Stock Stock { get; set; }
    }
}
