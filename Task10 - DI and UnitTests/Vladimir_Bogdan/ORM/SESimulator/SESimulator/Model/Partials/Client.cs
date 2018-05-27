using SESimulator.Core;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SESimulator.Model
{
    public partial class Client
    {
        public override string ToString()
        {
            string assets = "";
            foreach (var asset in this.Stocks.GroupBy(stock=>stock.TypeId))
            {
                assets += $"\t {asset.Key}: {asset.Count()} item(s)\n";
            }
            return $"{this.Name} {this.Surname} {this.PhoneNumber} \n Assets: {this.Balance}$\n{assets}";
        }
    }
}
