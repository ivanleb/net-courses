using LinqExampleCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToDbEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Book> Books { get; set; }

        IQueryable<Book> IDataModel.Books => Books;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasKey(w => w.Id);
            modelBuilder.Entity<Book>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Book>().Property(w => w.Price).HasColumnName("Price");
            modelBuilder.Entity<Book>().Property(w => w.Genre).HasColumnName("Genre");
            modelBuilder.Entity<Book>().Property(w => w.IsForSale).HasColumnName("IsForSale");
            modelBuilder.Entity<Book>().Property(w => w.IsValuable).HasColumnName("IsValuable");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbEfDataContext("Server=tcp:chdenis.database.windows.net,1433;Initial Catalog=dbBooks;Persist Security Info=False;User ID=chesdenis;Password=Xsw2cde3vfr4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                dbContext.ShowOutput();
            }
        }
    }
}
