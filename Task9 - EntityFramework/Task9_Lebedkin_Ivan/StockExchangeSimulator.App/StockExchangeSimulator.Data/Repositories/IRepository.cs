using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public interface IRepository<in T> where T : IAggregateRoot
    {
        int Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
