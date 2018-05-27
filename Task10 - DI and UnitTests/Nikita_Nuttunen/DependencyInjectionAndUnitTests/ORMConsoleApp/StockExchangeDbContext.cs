namespace ORMConsoleApp
{
    using ORMConsoleApp.Implementations;
    using ORMCore.Abstractions;
    using ORMCore.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class StockExchangeDbContext : DbContext, IDataContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public StockExchangeDbContext()
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

        int ICollectionModification<Client>.Add(Client entity) => Clients.Add(entity).Id;

        void ICollectionModification<Client>.Remove(Client entity) => Clients.Remove(entity);

        void ICollectionModification<Client>.Update(Client entity)
        {
            var modified = Clients.First(c => c.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Surname = entity.Surname;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;
            modified.Area = entity.Area;
            var stocks = new List<Stock>();
            foreach (var stock in entity.Stocks)
            {
                stocks.Add(stock);
            }
            modified.Stocks = stocks;
        }

        int ICollectionModification<Stock>.Add(Stock entity) => Stocks.Add(entity).Id;

        void ICollectionModification<Stock>.Remove(Stock entity) => Stocks.Remove(entity);

        void ICollectionModification<Stock>.Update(Stock entity)
        {
            var modified = Stocks.First(s => s.Id == entity.Id);
            modified.Type = entity.Type;
        }

        int ICollectionModification<Deal>.Add(Deal entity) => Deals.Add(entity).Id;

        void ICollectionModification<Deal>.Remove(Deal entity) => Deals.Remove(entity);

        void ICollectionModification<Deal>.Update(Deal entity)
        {
            var modified = Deals.First(d => d.Id == entity.Id);
            modified.Purchaser = entity.Purchaser;
            modified.Seller = entity.Seller;
            modified.SelledStock = entity.SelledStock;
            modified.Cost = entity.Cost;
        }
    }
}