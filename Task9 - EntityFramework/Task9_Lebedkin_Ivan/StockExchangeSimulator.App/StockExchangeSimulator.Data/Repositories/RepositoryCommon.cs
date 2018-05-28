using StockExchangeSimulator.Data.Models;
using System.Data.Entity;
using System.Linq;

namespace StockExchangeSimulator.Data.Repositories
{
    public abstract class RepositoryCommon : DbContext, IRepositoryClient, IRepositoryStock, IRepositoryTransaction
    {
        protected  RepositoryCommon(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Client> Clients { get; set; }

        IQueryable<Client> IRepositoryClient.Clients => this.Clients;

        int IRepository<Client>.Add(Client entity) => this.Clients.Add(entity).Id;
        void IRepository<Client>.Remove(Client entity) => this.Clients.Remove(entity);
        void IRepository<Client>.Update(Client entity)
        {
            var modified = this.Clients.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.SurName = entity.SurName;
            modified.TelephonNumber = entity.TelephonNumber;
            modified.Balance = entity.Balance;
            //modified.Zone = modified.Zone;
            modified.Stock = modified.Stock;
            modified.ClientStocksQuantity = modified.ClientStocksQuantity;
        }

        void IRepositoryClient.SaveChanges() => this.SaveChanges();

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