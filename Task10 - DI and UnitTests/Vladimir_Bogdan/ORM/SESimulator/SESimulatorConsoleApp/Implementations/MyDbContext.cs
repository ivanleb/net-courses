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
            //modelBuilder.Entity<Client>().HasMany(client => client.Stocks).WithRequired(stock => stock.Owner).HasForeignKey(stock => stock.ClientId);
            ////modelBuilder.Entity<Stock>().HasRequired(stock => stock.Type) ;
        }
    }
}
