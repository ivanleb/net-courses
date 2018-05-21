using SESimulator.Core;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SESimulator.Model
{
    public class Client
    {
        public decimal Balance { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Stock> Stocks { get; set; } = new HashSet<Stock>();

        public StockExchangeClientZone Zone
        {
            get
            {
                if (this.Balance == 0) return StockExchangeClientZone.Orange;
                if (this.Balance < 0) return StockExchangeClientZone.Black;
                return StockExchangeClientZone.White;
            }
        }

        public ICollection<Stock> StocksForSale { get { return this.Stocks; } }

        public override string ToString()
        {
            string stockList = "";
            foreach (var stockType in this.Stocks.Select(s=>s.Type).Distinct())
            {
                stockList += $"\t {stockType.Name}: {this.Stocks.Count(s => s.Type == stockType)}item(s)\n";
            }
            return $"{this.Name} {this.Surname} {this.PhoneNumber} \n Assets: {this.Balance}$\n{stockList}";
        }
    }
}
