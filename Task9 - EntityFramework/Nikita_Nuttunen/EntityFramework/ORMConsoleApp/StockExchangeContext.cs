namespace ORMConsoleApp
{
    using ORMConsoleApp.Implementations;
    using ORMCore.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class StockExchangeContext : DbContext//, IDataContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Deal> Deals { get; set; }

        public StockExchangeContext()
            : base("name=StockExchangeContext")
        {
        }
        /*
        IQueryable<IClient> IDataContext.Clients => throw new NotImplementedException(); // Clients

        IQueryable<IStock> IDataContext.Stocks => throw new NotImplementedException(); // Stocks

        IQueryable<IDeal> IDataContext.Deals => throw new NotImplementedException(); // Deals

       

        void IDataContext.SaveChanges()
        {
            SaveChanges();
        }

        void IClientsModification<IClient>.Add(IClient entity) => Clients.Add(entity as Client);

        void IClientsModification<IClient>.Remove(IClient entity) => Clients.Remove(entity as Client);

        void IClientsModification<IClient>.Update(IClient entity)
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
                stocks.Add(stock as Stock);
            }
            modified.Stocks = stocks;
        }

        void IStockModification<IStock>.Add(IStock entity) => Stocks.Add(entity as Stock);

        void IStockModification<IStock>.Remove(IStock entity) => Stocks.Remove(entity as Stock);

        void IStockModification<IStock>.Update(IStock entity)
        {
            var modified = Stocks.First(s => s.Id == entity.Id);
            modified.Type = entity.Type;
        }

        void IDealMofication<IDeal>.Add(IDeal entity) => Deals.Add(entity as Deal);

        void IDealMofication<IDeal>.Remove(IDeal entity) => Deals.Remove(entity as Deal);

        void IDealMofication<IDeal>.Update(IDeal entity)
        {
            var modified = Deals.First(d => d.Id == entity.Id);
            modified.Purchaser = entity.Purchaser as Client;
            modified.Seller = entity.Seller as Client;
            modified.SelledStock = entity.SelledStock as Stock;
            modified.Cost = entity.Cost;
        }*/
    }
}