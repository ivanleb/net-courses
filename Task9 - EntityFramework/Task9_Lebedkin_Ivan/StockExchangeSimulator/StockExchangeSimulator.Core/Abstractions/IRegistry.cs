using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Core.Abstractions
{
    public interface IRegistry
    {
        ILoggerService LoggerService { get; set; }
        ICollectionsModification<Transaction> DataContext { get; set; }
        ITransactionGenerator TransactionGenerator {get; set;}
        IEnumerable<Func<Transaction, bool>> CheckTransaction { get; set; }
    }
}
