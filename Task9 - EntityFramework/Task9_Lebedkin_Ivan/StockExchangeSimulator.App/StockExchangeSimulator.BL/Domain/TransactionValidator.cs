using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Domain
{
    public class TransactionValidator : ITransactionValidator
    {
        private readonly Func<Transaction, bool> _validateFunction;

        public TransactionValidator(Func<Transaction, bool> validateFunction)
        {
            _validateFunction = validateFunction;
        }

        public bool IsTransactionValid(Transaction transaction)
        {
            return _validateFunction(transaction);
        }
    }
}
