using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORMCore.Model;
using ORMCore;
using ORMCore.Repositories;

namespace ORM
{
    class BaseDbContext: DbContext, IModelRepository
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Deal> Deals { get; set; }

        IQueryable<Client> IModelRepository.Clients => Clients.Include(s => s.ClientStocks);

        IQueryable<Stock> IModelRepository.Stocks => Stocks.Include(st => st.Type);

        IQueryable<StockType> IModelRepository.StockTypes => StockTypes;

        IQueryable<Deal> IModelRepository.Deals => Deals;

        public void Add(Client entity) => Clients.Add(entity);

        public void Update(Client entity)
        {
            var modified = Clients.First(c => c.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Surname = entity.Surname;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;
        }

        public void Remove(Client entity) => Clients.Remove(entity);

        public void Add(Stock entity) => Stocks.Add(entity);

        public void Update(Stock entity)
        {
            var modified = Stocks.First(s => s.Id == entity.Id);
            modified.Type = entity.Type;
        }

        public void Remove(Stock entity) => Stocks.Remove(entity);

        public void Add(StockType entity) => StockTypes.Add(entity);

        public void Update(StockType entity)
        {
            var modified = StockTypes.First(st => st.Id == entity.Id);
            modified.Name = entity.Name;
            modified.Cost = entity.Cost;
        }

        public void Remove(StockType entity) => StockTypes.Remove(entity);

        public void Add(Deal entity) => Deals.Add(entity);

        public void Update(Deal entity)
        {
            var modified = Deals.First(d => d.Id == entity.Id);
            modified.Buyer = entity.Buyer;
            modified.Seller = entity.Seller;
            modified.Stock = entity.Stock;
            modified.Sum = entity.Sum;
        }

        public void Remove(Deal entity) => Deals.Remove(entity);

        void IModelRepository.SaveChanges()
        {
            this.SaveChanges();
        }

        public BaseDbContext(string connectionString) : base(connectionString)
        {

        }
    }
}
