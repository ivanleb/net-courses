using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface ITransactionGenerator
    {
        //void Run(IEnumerable<Func<Transaction, bool>> isTransactionValid);

        Transaction GenerateTransaction(IEnumerable<ITransactionValidator> transactionValidators);
    }
}
