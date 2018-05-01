using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace LinqToDbEf
{
    public class LinqToDbModel:DbContext, IDataModel
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        IQueryable<Car> IDataModel.Cars => Cars;
        IQueryable<Dealer> IDataModel.Dealers => Dealers;
        IQueryable<Order> IDataModel.Orders => Orders;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Car>().HasKey(x=>x.Id);
            modelBuilder.Entity<Car>().Property(x => x.Brend).HasColumnName("Brend");
            modelBuilder.Entity<Car>().Property(x => x.EngineCapacity).HasColumnName("EngineCapacity");
            modelBuilder.Entity<Car>().Property(x => x.FuelConsumption).HasColumnName("FuelConsumption");
            modelBuilder.Entity<Car>().Property(x => x.HorsePower).HasColumnName("HorsePower");
            modelBuilder.Entity<Car>().Property(x => x.Price).HasColumnName("Price");

            modelBuilder.Entity<Dealer>().ToTable("Dealers");
            modelBuilder.Entity<Dealer>().HasKey(x => x.Id);
            modelBuilder.Entity<Dealer>().Property(x => x.CarsNumber).HasColumnName("CarsNumber");
            modelBuilder.Entity<Dealer>().Property(x => x.Employee).HasColumnName("Employee");
            modelBuilder.Entity<Dealer>().Property(x => x.Location).HasColumnName("Location");
            modelBuilder.Entity<Dealer>().Property(x => x.Title).HasColumnName("Title");

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.CarId).HasColumnName("CarId");
            modelBuilder.Entity<Order>().Property(x => x.CustomerName).HasColumnName("CustomerName");
            modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("Date");
            modelBuilder.Entity<Order>().Property(x => x.DealerId).HasColumnName("DealerId");
        }
        public LinqToDbModel() : base("DBConnection") {}
    }
}
