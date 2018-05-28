using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface ITransactionValidator
    {
        bool IsTransactionValid(Transaction transaction);
    }
}
