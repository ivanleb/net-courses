using System.Linq;
using System.Data.Entity;
using ORM.Core.Abstractions;
using ORM.Core.Model;

namespace ORM.ConsoleApp.Implementations
{
    public class Repository : DbContext, IRepository
    {
        public DbSet<Trader> Traders { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Deal> Deals { get; set; }

        IQueryable<Trader> IRepository.Traders => Traders.Include(t => t.Portfolio.Select(s => s.Listing));
        IQueryable<Listing> IRepository.Listings => Listings;
        IQueryable<Share> IRepository.Shares => Shares;
        IQueryable<Deal> IRepository.Deals => Deals;

        public Repository(): base("name=TradeDB")
        {
            Database.SetInitializer(new RepositoryInitializer());
        }

        public void Add(Trader trader) => Traders.Add(trader);
        public void Add(Listing listing) => Listings.Add(listing);
        public void Add(Share share) => Shares.Add(share);
        public void Add(Deal deal) => Deals.Add(deal);
        void IRepository.SaveChanges() => this.SaveChanges();
    }
}
