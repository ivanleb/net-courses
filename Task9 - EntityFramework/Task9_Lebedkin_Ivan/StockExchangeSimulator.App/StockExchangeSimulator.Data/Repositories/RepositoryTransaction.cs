using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.Data.Repositories
{
    public abstract class RepositoryTransaction : DbContext, IRepositoryTransaction
    {
        protected RepositoryTransaction(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
        IQueryable<Transaction> IRepositoryTransaction.Transactions => this.Transactions;

        int IRepository<Transaction>.Add(Transaction entity) => this.Transactions.Add(entity).Id;
        void IRepository<Transaction>.Remove(Transaction entity) => this.Transactions.Remove(entity);
        void IRepository<Transaction>.Update(Transaction entity)
        {
            var modified = this.Transactions.First(f => f.Id == entity.Id);
            modified.Seller = entity.Seller;
            modified.Buyer = entity.Buyer;
            modified.StocksQuantity = entity.StocksQuantity;
            modified.Stock = entity.Stock;
        }

        void IRepositoryTransaction.SaveChanges() => this.SaveChanges();
    }
}
