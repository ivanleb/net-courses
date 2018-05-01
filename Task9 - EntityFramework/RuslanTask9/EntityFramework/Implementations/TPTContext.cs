using System.Data.Entity;
using ORMCore;

namespace EntityFramework.Implementations
{
    public class TPTContext : BaseDbContext
    {
        public TPTContext(string connectionstring) : base(connectionstring)
        {
            Database.SetInitializer<TPTContext>(new DropCreateDatabaseIfModelChanges<TPTContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shareholder>().ToTable("Shareholders");
            modelBuilder.Entity<Balance>().ToTable("Balances");
            modelBuilder.Entity<Trade>().ToTable("Trades");
        }
    }
}
