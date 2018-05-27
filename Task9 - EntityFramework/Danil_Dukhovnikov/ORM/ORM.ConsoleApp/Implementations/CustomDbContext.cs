using System.Data.Entity;
using ORM.Core.Model;

namespace ORM.ConsoleApp.Implementations
{
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext(string connectionString) : base(connectionString) { }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasMany(client => client.Stocks)
                .WithRequired(stock => stock.Owner)
                .HasForeignKey(stock => stock.ClientId).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Deal>().HasRequired(d => d.Buyer)
                .WithMany().HasForeignKey(b => b.BuyerIdd)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Deal>().HasRequired(d => d.Seller)
                .WithMany().HasForeignKey(b => b.Selleri)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Stock>().HasRequired(stock => stock.Type)
                .WithMany().HasForeignKey(s=>s.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
