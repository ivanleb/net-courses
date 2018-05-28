using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Test
{
    public class TransactionValidatorMock : ITransactionValidator
    {
        public bool IsTransactionValid(Transaction transaction)
        {
            return transaction != null;
        }
    }
}
