using System.Data.Entity;
using System.Linq;
using ORM.Core.Abstractions;
using ORM.Core.Model;

namespace ORM.ConsoleApp
{
    public abstract class BaseDbContext : DbContext, IDataContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockType> StockTypes { get; set; }

        public DbSet<Deal> Deals { get; set; }

        IQueryable<Client> IDataContext.Clients => this.Clients.Include(cl=>cl.Stocks);

        IQueryable<Stock> IDataContext.Stocks => this.Stocks;

        IQueryable<Deal> IDataContext.Deals => this.Deals;

        IQueryable<StockType> IDataContext.StockTypes => this.StockTypes;

        int ICollectionModification<StockType>.Add(StockType entity) => this.StockTypes.Add(entity).Id;

        void ICollectionModification<StockType>.Update(StockType entity)
        {
            var modified = this.StockTypes.First(st => st.Id == entity.Id);
            
            modified.Name = entity.Name;
            modified.Cost = entity.Cost;
        }

        void ICollectionModification<StockType>.Remove(StockType entity) => this.StockTypes.Remove(entity);

        int ICollectionModification<Stock>.Add(Stock entity) => this.Stocks.Add(entity).Id;

        void ICollectionModification<Stock>.Update(Stock entity)
        {
            var modified = this.Stocks.First(s => s.Id == entity.Id);
            
            modified.Type = entity.Type;
        }

        void ICollectionModification<Stock>.Remove(Stock entity) => this.Stocks.Remove(entity);

        int ICollectionModification<Client>.Add(Client entity) => this.Clients.Add(entity).Id;

        void ICollectionModification<Client>.Update(Client entity)
        {
            var modified = this.Clients.First(c => c.Id == entity.Id);
            
            modified.Name = entity.Name;
            modified.Surname = entity.Surname;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;
        }

        void ICollectionModification<Client>.Remove(Client entity)
        {
            this.Clients.Remove(entity);
        }

        int ICollectionModification<Deal>.Add(Deal entity) => this.Deals.Add(entity).Id;

        void ICollectionModification<Deal>.Update(Deal entity)
        {
            var modified = this.Deals.First(d => d.Id == entity.Id);
            
            modified.Buyer = entity.Buyer;
            modified.Seller = entity.Seller;
            modified.Stock = entity.Stock;
            modified.Cost = entity.Cost;
        }

        void ICollectionModification<Deal>.Remove(Deal entity) => this.Deals.Remove(entity);

        void IDataContext.SaveChanges()
        {
            this.SaveChanges();
        }

        protected BaseDbContext(string connectionString) : base(connectionString) { }
    }    
}
