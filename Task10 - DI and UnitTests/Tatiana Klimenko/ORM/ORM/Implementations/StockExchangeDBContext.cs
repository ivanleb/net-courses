using System.Collections.Generic;
using System.Linq;
using ORM.Core.Abstractions;
using ORM.Core.Entities;
using System.Data.Entity;

namespace ORM.Implementations
{
    class StockExchangeDBContext : DbContext, IDataContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public StockExchangeDBContext()
            : base("name=StockExchangeContext")
        {
        }

        IQueryable<Client> IDataContext.Clients => Clients.Include(c => c.Stocks);

        IQueryable<Stock> IDataContext.Stocks => Stocks;

        IQueryable<Deal> IDataContext.Deals => Deals;

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }

        int ICollectionModifier<Client>.Add(Client entity) => Clients.Add(entity).Id;

        void ICollectionModifier<Client>.Remove(Client entity) => Clients.Remove(entity);

        void ICollectionModifier<Client>.Update(Client entity)
        {
            var modified = Clients.First(c => c.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Surname = entity.Surname;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;

            List<Stock> stocks = new List<Stock>();
            foreach (var stock in entity.Stocks)
            {
                stocks.Add(stock as Stock);
            }
            modified.Stocks = stocks;
        }

        int ICollectionModifier<Stock>.Add(Stock entity) => Stocks.Add(entity).Id;

        void ICollectionModifier<Stock>.Remove(Stock entity) => Stocks.Remove(entity);

        void ICollectionModifier<Stock>.Update(Stock entity)
        {
            var modified = Stocks.First(s => s.Id == entity.Id);
            modified.Type = entity.Type;
        }

        int ICollectionModifier<Deal>.Add(Deal entity) => Deals.Add(entity).Id;

        void ICollectionModifier<Deal>.Remove(Deal entity) => Deals.Remove(entity);

        void ICollectionModifier<Deal>.Update(Deal entity)
        {
            var modified = Deals.First(d => d.Id == entity.Id);
            modified.Purchaser = entity.Purchaser;
            modified.Seller = entity.Seller;
            modified.SelledStock = entity.SelledStock;
            modified.Cost = entity.Cost;
        }
    }
}
