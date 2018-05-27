using EntityCore.Abstractions;
using System.Data.Entity;
using EntityCore.Model;

namespace EntityConsoleApp.Implementations
{
    public class TablePerTypeContext : BaseDbContext
    {
        public TablePerTypeContext() : base("name=TradeDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Stock>().ToTable("Stocks");
            modelBuilder.Entity<Trade>().ToTable("Trades");
        }
    }
}
