using System.Data.Entity;
using System.Linq;
using EF.Core.Abstractions;
using EF.Core.Services;
using EF.Implementations;
using EF.Implementations.Entities;
using StructureMap;

namespace EF.Core.Implementations
{
    public class StockExchangeDataContext : DbContext, IDataContext
    {
        //static StockExchangeDataContext()
        //{
        //    Database.SetInitializer(new StockExchangeDbInitializer());
        //}
        [DefaultConstructor]
        public StockExchangeDataContext(string dbConn) : base(dbConn){}

        public DbSet<Trader> Traders { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<TradableType> TradableTypes { get; set; }
        public DbSet<TradeOperation> TradeOperations { get; set; }
        public Status Status { get; set; }

        public int Add<T>(T entity) where T : class, IEntity => Set<T>().Add(entity).Id;
        public void Remove<T>(T entity) where T : class => Set<T>().Remove(entity);

        public void Update<T>(T entity) where T : class, IEntity
        {
            var modified = Set<T>().Single(x => x.Id == entity.Id);
            var entityProps = entity.GetType().GetProperties();
            foreach (var propertyInfo in entityProps)
            {
                var value = propertyInfo.GetGetMethod().Invoke(entity, null);
                propertyInfo.SetValue(modified, value, null);
            }
        }

        IQueryable<Trader> IDataContext.Traders => Traders;
        IQueryable<Stock> IDataContext.Stocks => Stocks;
        IQueryable<TradableType> IDataContext.TradableTypes => TradableTypes;
        IQueryable<TradeOperation> IDataContext.TradeOperations => TradeOperations;

        void ICollectionModificationService.SaveChanges() => SaveChanges();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<IndividualInfo>();
            base.OnModelCreating(modelBuilder);
        }
    }
}