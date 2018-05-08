using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Core
{
    public interface ITransactionGenerator
    {
        void Run(IEnumerable<Func<Transaction, bool>> isTransactionValid);//ICollectionsModification<Transaction> transactionList);
    }
}
