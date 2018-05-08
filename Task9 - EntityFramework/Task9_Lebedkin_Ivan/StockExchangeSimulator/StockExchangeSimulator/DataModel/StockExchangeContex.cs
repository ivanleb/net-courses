namespace StockExchangeSimulator.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using StockExchangeSimulator.Core.Abstractions;
    using StockExchangeSimulator.Core.DataModel;

    public class StockExchangeContex : DbContext, IDataContext
    {
        public StockExchangeContex()
            : base("name=StockExchangeContex")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        IQueryable<Client> IDataContext.Clients => this.Clients;//.AsQueryable();
        IQueryable<Stock> IDataContext.Stocks => this.Stocks;
        IQueryable<Transaction> IDataContext.Transactions => this.Transactions;

        int ICollectionsModification<Client>.Add(Client entity) => this.Clients.Add(entity).Id;
        void ICollectionsModification<Client>.Remove(Client entity) => this.Clients.Remove(entity);
        void ICollectionsModification<Client>.Update(Client entity)
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

        int ICollectionsModification<Stock>.Add(Stock entity) => this.Stocks.Add(entity).Id;
        void ICollectionsModification<Stock>.Remove(Stock entity) => this.Stocks.Remove(entity);
        void ICollectionsModification<Stock>.Update(Stock entity)
        {
            var modified = this.Stocks.First(f => f.Id == entity.Id);
            modified.Name =  entity.Name;
            modified.Price = entity.Price;
            modified.Type =  entity.Type;
        }

        int ICollectionsModification<Transaction>.Add(Transaction entity) => this.Transactions.Add(entity).Id;
        void ICollectionsModification<Transaction>.Remove(Transaction entity) => this.Transactions.Remove(entity);
        void ICollectionsModification<Transaction>.Update(Transaction entity)
        {
            var modified = this.Transactions.First(f => f.Id == entity.Id);
            modified.Seller = entity.Seller;
            modified.Buyer = entity.Buyer;
            modified.StocksQuantity = entity.StocksQuantity;
            modified.Stock = entity.Stock;
        }

        void IDataContext.SaveChanges() => this.SaveChanges();       
        

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
    }

}