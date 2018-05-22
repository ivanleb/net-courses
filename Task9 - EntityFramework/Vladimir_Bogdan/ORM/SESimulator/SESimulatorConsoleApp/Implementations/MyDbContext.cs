using SESimulator.Model;
using System.Data.Entity;

namespace SESimulatorConsoleApp.Implementations
{
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext(string connectionString) : base(connectionString) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Deal>().HasRequired(z=>z.Buyer).WithOptional().Map(m =>
            //{
            //    m.MapKey("");
            //});
            modelBuilder.Entity<Client>().HasMany(client => client.Stocks).WithRequired(stock => stock.Owner).HasForeignKey(stock => stock.ClientId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Deal>().HasRequired(d => d.Buyer).WithMany().HasForeignKey(b => b.BuyerIdd).WillCascadeOnDelete(false);
            modelBuilder.Entity<Deal>().HasRequired(d => d.Seller).WithMany().HasForeignKey(b => b.Selleriiiii).WillCascadeOnDelete(false);
            modelBuilder.Entity<Stock>().HasRequired(stock => stock.Type).WithMany().HasForeignKey(s=>s.TypeId).WillCascadeOnDelete(false);
        }
    }
}
