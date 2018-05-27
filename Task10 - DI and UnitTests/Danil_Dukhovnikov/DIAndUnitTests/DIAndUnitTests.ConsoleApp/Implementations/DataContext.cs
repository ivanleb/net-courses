using System.Data.Entity;
using System.Linq;
using DIAndUnitTests.Core.Abstractions;
using DIAndUnitTests.Core.Models;

namespace DIAndUnitTests.ConsoleApp.Implementations
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() : base("myConnectionString")
        {
            Database.SetInitializer(new DataContextInitializer());
        }

        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Trader> Traders { get; set; }

        IQueryable<Deal> IDataContext.Deals => Deals;
        IQueryable<Share> IDataContext.Shares => Shares;
        IQueryable<Trader> IDataContext.Traders => Traders;

        public void Add(Deal entity) => Deals.Add(entity);
        public void Add(Share entity) => Shares.Add(entity);
        public void Add(Trader entity) => Traders.Add(entity);

        void IDataContext.SaveChanges() => this.SaveChanges();
    }
}