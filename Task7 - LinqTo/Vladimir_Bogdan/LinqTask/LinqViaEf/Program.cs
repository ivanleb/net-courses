using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LinqCore;

namespace LinqViaEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

        IQueryable<Player> IDataModel.Players => Players;
        IQueryable<Team> IDataModel.Teams => Teams;
        IQueryable<Stadium> IDataModel.Stadiums => Stadiums;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>().HasKey(w => w.Id);
            modelBuilder.Entity<Player>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Player>().Property(w => w.Salary).HasColumnName("Salary");
            modelBuilder.Entity<Player>().Property(w => w.StrongestHand).HasColumnName("Shoots");
            modelBuilder.Entity<Player>().Property(w => w.BirthdayDate).HasColumnName("Birthday");
            modelBuilder.Entity<Player>().Property(w => w.Citizenship).HasColumnName("Citizenship");
            modelBuilder.Entity<Player>().Property(w => w.Age);

            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Team>().HasKey(w => w.Id);
            modelBuilder.Entity<Team>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Team>().Property(w => w.Country).HasColumnName("Country");
            modelBuilder.Entity<Team>().Property(w => w.HeadCoach).HasColumnName("Coach");
            modelBuilder.Entity<Team>().Property(w => w.FoundationDate).HasColumnName("FoundedIn");

            modelBuilder.Entity<Stadium>().ToTable("Stadiums");
            modelBuilder.Entity<Stadium>().HasKey(w => w.Id);
            modelBuilder.Entity<Stadium>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Stadium>().Property(w => w.City).HasColumnName("City");
            modelBuilder.Entity<Stadium>().Property(w => w.Capacity).HasColumnName("Capacity");

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
