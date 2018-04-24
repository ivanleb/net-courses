using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SQLite;
using LinqCore;

namespace LinqEF
{
    public class SQLiteDataContext : DbContext, IDataModel
    {
        public DbSet<Amplifier> Amplifiers { get; set; }
        public DbSet<Guitar> Guitars { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }

        IQueryable<Amplifier> IDataModel.Amplifiers => Amplifiers;
        IQueryable<Guitar> IDataModel.Guitars => Guitars;
        IQueryable<Keyboard> IDataModel.Keyboards => Keyboards;

        public SQLiteDataContext(string path) : 
            base(new SQLiteConnection($"data source={path}"), true)
        {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amplifier>().ToTable("Amplifiers");
            modelBuilder.Entity<Amplifier>().HasKey(a => a.Id);
            modelBuilder.Entity<Amplifier>().Property(a => a.Brand).HasColumnName("brand");
            modelBuilder.Entity<Amplifier>().Property(a => a.Name).HasColumnName("name");
            modelBuilder.Entity<Amplifier>().Property(a => a.Nobs).HasColumnName("nobs");
            modelBuilder.Entity<Amplifier>().Property(a => a.Effects).HasColumnName("effects");
            modelBuilder.Entity<Amplifier>().Property(a => a.MaxPower).HasColumnName("max_power");
            modelBuilder.Entity<Amplifier>().Property(a => a.Price).HasColumnName("price");

            modelBuilder.Entity<Guitar>().ToTable("Guitars");
            modelBuilder.Entity<Guitar>().HasKey(g => g.Id);
            modelBuilder.Entity<Guitar>().Property(g => g.Brand).HasColumnName("brand");
            modelBuilder.Entity<Guitar>().Property(g => g.Name).HasColumnName("name");
            modelBuilder.Entity<Guitar>().Property(g => g.PickupConfig).HasColumnName("pickup_config");
            modelBuilder.Entity<Guitar>().Property(g => g.Color).HasColumnName("color");
            modelBuilder.Entity<Guitar>().Property(g => g.Frets).HasColumnName("frets");
            modelBuilder.Entity<Guitar>().Property(g => g.Strings).HasColumnName("strings");
            modelBuilder.Entity<Guitar>().Property(g => g.Price).HasColumnName("price");
            modelBuilder.Entity<Guitar>().Property(g => g.IncludedAccessories).HasColumnName("included_accessories");

            modelBuilder.Entity<Keyboard>().ToTable("Keyboards");
            modelBuilder.Entity<Keyboard>().HasKey(k => k.Id);
            modelBuilder.Entity<Keyboard>().Property(k => k.Brand).HasColumnName("brand");
            modelBuilder.Entity<Keyboard>().Property(k => k.Name).HasColumnName("name");
            modelBuilder.Entity<Keyboard>().Property(k => k.Keys).HasColumnName("keys");
            modelBuilder.Entity<Keyboard>().Property(k => k.Price).HasColumnName("price");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new SQLiteDataContext(@".\..\..\data.db"))
            {
                dbContext.ShowOutput();
            }
        }
    }
}
