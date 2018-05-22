using SESimulator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESimulatorConsoleApp.Implementations
{
    class DealInfo : IDealInfo
    {
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public decimal Amount { get; set; }
        public string Stock { get; set; }

        public override string ToString()
        {
            return $"From: {this.Seller}; To: {this.Buyer}; Deal Subject: {this.Stock}; Deal Amount: {this.Amount}";
        }
    }
}
