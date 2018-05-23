using System.Linq;
using System.Data.Entity;
using ORM.Core.Abstractions;
using ORM.Core.Model;

namespace ORM.ConsoleApp.Implementations
{
    public class TradeDBContext : DbContext, IDataContext
    {
        public DbSet<Trader> Traders { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Deal> Deals { get; set; }

        IQueryable<Trader> IDataContext.Traiders => Traders.Include(t => t.Portfolio.Select(s => s.Listing));
        IQueryable<Listing> IDataContext.Listings => Listings;
        IQueryable<Share> IDataContext.Shares => Shares;
        IQueryable<Deal> IDataContext.Deals => Deals;

        public TradeDBContext(): base("name=TradeDB")
        {
            Database.SetInitializer(new TradeDBInitializer());
        }

        public void Add(Trader trader) => Traders.Add(trader);
        public void Add(Listing listing) => Listings.Add(listing);
        public void Add(Share share) => Shares.Add(share);
        public void Add(Deal deal) => Deals.Add(deal);
        void IDataContext.SaveChanges() => this.SaveChanges();
    }
}
