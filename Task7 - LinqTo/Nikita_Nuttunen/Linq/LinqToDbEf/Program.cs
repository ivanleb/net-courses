using ConsoleOutput;
using LinqCore;
using LinqCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LinqToDbEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Device> Devices { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        IQueryable<Device> IDataModel.Devices => Devices;

        IQueryable<Order> IDataModel.Orders => Orders;

        IQueryable<Customer> IDataModel.Customers => Customers;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().ToTable("Devices");
            modelBuilder.Entity<Device>().HasKey(b => b.Id);
            modelBuilder.Entity<Device>().Property(b => b.Name).HasColumnName("Name");
            modelBuilder.Entity<Device>().Property(b => b.Price).HasColumnName("Price");
            modelBuilder.Entity<Device>().Property(b => b.Category).HasColumnName("Category");
            modelBuilder.Entity<Device>().Property(b => b.IsAvailable).HasColumnName("IsAvailable");

            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Name).HasColumnName("Name");
            modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasColumnName("PhoneNumber");
            modelBuilder.Entity<Customer>().Property(c => c.Status).HasColumnName("Status");
            modelBuilder.Entity<Customer>().Property(c => c.DateOfBirth).HasColumnName("DateOfBirth");

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().Property(o => o.CustomerId).HasColumnName("CustomerId");
            modelBuilder.Entity<Order>().Property(o => o.Date).HasColumnName("Date");
            modelBuilder.Entity<Order>().Property(o => o.Total).HasColumnName("Total");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbEfDataContext(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nikita\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                dbContext.ShowOutput();
            }
            Console.ReadLine();
        }
    }
}
