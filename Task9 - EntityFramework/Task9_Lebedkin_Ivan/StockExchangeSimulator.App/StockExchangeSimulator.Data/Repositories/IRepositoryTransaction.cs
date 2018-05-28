using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public interface IRepositoryTransaction: IRepository<Transaction>
    {
        IQueryable<Transaction> Transactions { get; }
        void SaveChanges();
    }
}
