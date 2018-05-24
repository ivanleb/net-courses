using System;
using System.Data.Entity;
using System.Linq;
using EF.Core.Repositories;
using EF.Core.Services;
using EF.Implementations;

namespace EF.Core
{
    public class StockExchangeDataContext : DbContext, IDataContext
    {
        static StockExchangeDataContext()
        {
            Database.SetInitializer(new StockExchangeDbInitializer());
        }

        public StockExchangeDataContext(string dbConn) : base(dbConn)
        {
        }

        public DbSet<Traider> Traiders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public Status Status { get; set; }

        public int Add<T>(T entity) where T : class, IId => Set<T>().Add(entity).Id;
        public void Remove<T>(T entity) where T : class => Set<T>().Remove(entity);

        public void Update(Traider entity)
        {
            var modified = Traiders.Single(x => x.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.Balance = entity.Balance;
            modified.SecondName = entity.SecondName;
            modified.Status = Status.ApplyStatus(modified);
        }

        public void Update(Stock entity)
        {
            if (entity.Quantity == 0)
                Stocks.Remove(entity);
        }

        public void Update(StockType entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Operation entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<Traider> IDataContext.Traiders => Traiders;
        IQueryable<Stock> IDataContext.Stocks => Stocks;
        IQueryable<StockType> IDataContext.StockTypes => StockTypes;
        IQueryable<Operation> IDataContext.Operations => Operations;

        void ICollectionModification.SaveChanges() => SaveChanges();
    }
}