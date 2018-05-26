using System.Linq;
using EntityCore.Abstractions;
using EntityCore.Model;
using System.Data.Entity;

namespace EntityConsoleApp
{
    public abstract class BaseDbContext : DbContext, IDataContext
    {
        protected BaseDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Trade> Trades { get; set; }
        IQueryable<Client> IDataContext.Clients => this.Clients;

        IQueryable<Stock> IDataContext.Stocks => this.Stocks;

        IQueryable<Trade> IDataContext.Trades => this.Trades;

        public void Add(Client entity) => Clients.Add(entity);
        public void Remove(Client entity) => Clients.Remove(entity);
        public void Update(Client entity)
        {
            Client modified = Clients.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
            modified.PhoneNumber = entity.PhoneNumber;
            modified.Balance = entity.Balance;
        }

        public void Add(Stock entity) => Stocks.Add(entity);
        public void Remove(Stock entity) => Stocks.Remove(entity);
        public void Update(Stock entity)
        {
            Stock modified = Stocks.First(f => f.Id == entity.Id);
            modified.TypeOfStock = entity.TypeOfStock;
        }

        public void Add(Trade entity) => Trades.Add(entity);
        public void Remove(Trade entity) => Trades.Remove(entity);
        public void Update(Trade entity)
        {
            Trade modified = Trades.First(f => f.Id == entity.Id);
            modified.Seller = entity.Seller;
            modified.Buyer = entity.Buyer;
            modified.StockFromSeller = entity.StockFromSeller;
        }

        void IDataContext.SaveChanges() => SaveChanges();
    }
}
