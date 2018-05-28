using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public abstract class RepositoryStock : DbContext, IRepositoryStock
    {
        protected RepositoryStock(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        IQueryable<Stock> IRepositoryStock.Stocks => this.Stocks;

        int IRepository<Stock>.Add(Stock entity) => this.Stocks.Add(entity).Id;
        void IRepository<Stock>.Remove(Stock entity) => this.Stocks.Remove(entity);
        void IRepository<Stock>.Update(Stock entity)
        {
            var modified = this.Stocks.First(f => f.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Price = entity.Price;
            modified.Type = entity.Type;
        }

        void IRepositoryStock.SaveChanges() => this.SaveChanges();
    }
}
