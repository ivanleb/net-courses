using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LinqCore;

namespace LinqToDBProvIder
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        IQueryable<Shop> IDataModel.Shops => Shops;
        IQueryable<Restaurant> IDataModel.Restaurants => Restaurants;
        IQueryable<Hotel> IDataModel.Hotels => Hotels;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().ToTable("Shops");
            modelBuilder.Entity<Shop>().HasKey(w => w.Id);
            modelBuilder.Entity<Shop>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Shop>().Property(w => w.Street).HasColumnName("Street");
            modelBuilder.Entity<Shop>().Property(w => w.Brand).HasColumnName("Brand");
            modelBuilder.Entity<Shop>().Property(w => w.SquareInMetrs).HasColumnName("SquareInMetrs");
            modelBuilder.Entity<Shop>().Property(w => w.DailyProceeds).HasColumnName("DailyProceeds");

            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Restaurant>().HasKey(w => w.Id);
            modelBuilder.Entity<Restaurant>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Restaurant>().Property(w => w.Style).HasColumnName("Type");
            modelBuilder.Entity<Restaurant>().Property(w => w.Kitchen).HasColumnName("Kitchen");
            modelBuilder.Entity<Restaurant>().Property(w => w.CapacityVisitors).HasColumnName("CapacityVisitors");
            modelBuilder.Entity<Restaurant>().Property(w => w.AverageBill).HasColumnName("AverageBill");

            modelBuilder.Entity<Hotel>().ToTable("Hotels");
            modelBuilder.Entity<Hotel>().HasKey(w => w.Id);
            modelBuilder.Entity<Hotel>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Hotel>().Property(w => w.YearBuilt).HasColumnName("YearBuilt");
            modelBuilder.Entity<Hotel>().Property(w => w.NumberRooms).HasColumnName("NumderRooms");
            modelBuilder.Entity<Hotel>().Property(w => w.IsAdultOnle).HasColumnName("IsAdultOnly");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbEfDataContext("Data Source=.;Initial Catalog=Task7DB;Integrated Security=True"))
            {
                dbContext.ShowOutput();
            }
        }
    }
}
