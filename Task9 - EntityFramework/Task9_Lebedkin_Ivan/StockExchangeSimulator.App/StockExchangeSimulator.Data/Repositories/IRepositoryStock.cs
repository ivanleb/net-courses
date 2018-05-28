using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public interface IRepositoryStock : IRepository<Stock> 
    {
        IQueryable<Stock> Stocks { get; }
        void SaveChanges();
    }
}
