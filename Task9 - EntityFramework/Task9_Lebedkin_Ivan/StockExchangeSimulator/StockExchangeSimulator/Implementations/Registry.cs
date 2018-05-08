using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Implementations
{
    public class Registry : IRegistry
    {
        public Registry()
        {

        }

        public ILoggerService LoggerService { get ; set; }
        public ICollectionsModification<Transaction> DataContext { get; set; }
        public ITransactionGenerator TransactionGenerator { get; set ; }
        public IEnumerable<Func<Transaction, bool>> CheckTransaction { get; set; }
    }
}
