using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORMCore.Model;
using ORMCore;

namespace ORM
{
    class BaseDbContext: DbContext, IDataContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Deal> Deals { get; set; }

        IQueryable<Client> IDataContext.Clients => Clients.Include(s => s.ClientStocks);

        IQueryable<Stock> IDataContext.Stocks => Stocks.Include(st => st.Type);

        IQueryable<StockType> IDataContext.StockTypes => StockTypes;

        IQueryable<Deal> IDataContext.Deals => Deals;

        public int Add(Client entity) => Clients.Add(entity).Id;

        public void Update(Client entity)
        {
            var modified = Clients.First(c => c.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Surname = entity.Surname;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;
        }

        public void Remove(Client entity) => Clients.Remove(entity);

        public int Add(Stock entity) => Stocks.Add(entity).Id;

        public void Update(Stock entity)
        {
            var modified = Stocks.First(s => s.Id == entity.Id);
            modified.Type = entity.Type;
        }

        public void Remove(Stock entity) => Stocks.Remove(entity);

        public int Add(StockType entity) => StockTypes.Add(entity).Id;

        public void Update(StockType entity)
        {
            var modified = StockTypes.First(st => st.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Cost = entity.Cost;
        }

        public void Remove(StockType entity) => StockTypes.Remove(entity);

        public int Add(Deal entity) => Deals.Add(entity).Id;

        public void Update(Deal entity)
        {
            var modified = Deals.First(d => d.Id == entity.Id);
            modified.Buyer = entity.Buyer;
            modified.Seller = entity.Seller;
            modified.Stock = entity.Stock;
            modified.Sum = entity.Sum;
        }

        public void Remove(Deal entity) => Deals.Remove(entity);

        void IDataContext.SaveChanges()
        {
            this.SaveChanges();
        }

        public BaseDbContext(string connectionString) : base(connectionString)
        {

        }
    }
}
