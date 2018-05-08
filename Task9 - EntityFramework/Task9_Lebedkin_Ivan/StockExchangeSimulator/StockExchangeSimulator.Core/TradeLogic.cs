using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.Abstractions;

namespace StockExchangeSimulator.Core
{
    public class TradeLogic
    {
        public void Run(IRegistry registry)
        {
                registry.TransactionGenerator.Run(registry.CheckTransaction);
        }
    }
}
