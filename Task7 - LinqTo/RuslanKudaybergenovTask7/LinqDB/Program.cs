using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Task7_linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace LinqDB
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        IQueryable<User> IDataModel.Users => Users;

        IQueryable<Location> IDataModel.Locations => Locations;


        public DbEfDataContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(w => w.ID);
            modelBuilder.Entity<User>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<User>().Property(w => w.Password).HasColumnName("Password");
            modelBuilder.Entity<User>().Property(w => w.Type).HasColumnName("Type");
            modelBuilder.Entity<User>().Property(w => w.IDLocation).HasColumnName("IDLocation");
            
            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<Location>().HasKey(w => w.ID);
            modelBuilder.Entity<Location>().Property(w => w.Country).HasColumnName("Country");
            modelBuilder.Entity<Location>().Property(w => w.City).HasColumnName("City");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbEfDataContext("connectionString"))
            {
                dbContext.ShowOutput();
            }
        }
    }
}
