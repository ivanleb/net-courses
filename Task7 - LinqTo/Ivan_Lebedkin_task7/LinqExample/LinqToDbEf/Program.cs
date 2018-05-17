using LinqExampleCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToDbEf
{
    public class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Dinosaur> Dinosaurs { get; set; }
        public DbSet<HistoricalFigure> HistoricalFigures { get; set; }

        //private System.Data.SqlClient.SqlConnection Conection;

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

            modelBuilder.Entity<Dinosaur>().ToTable("Dinosaurs");
            modelBuilder.Entity<Dinosaur>().HasKey(w => w.Id);
            modelBuilder.Entity<Dinosaur>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Dinosaur>().Property(w => w.Weight).HasColumnName("Weight");
            modelBuilder.Entity<Dinosaur>().Property(w => w.High).HasColumnName("High");
            modelBuilder.Entity<Dinosaur>().Property(w => w.IsDangerous).HasColumnName("IsDangerous");
            modelBuilder.Entity<Dinosaur>().Property(w => w.IsFlying).HasColumnName("IsFlying");
            modelBuilder.Entity<Dinosaur>().Property(w => w.IsFloating).HasColumnName("IsFloating");

            modelBuilder.Entity<HistoricalFigure>().ToTable("HistoricalFigures");
            modelBuilder.Entity<HistoricalFigure>().HasKey(w => w.Id);
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.IsHaveBookAbout).HasColumnName("IsHaveBookAbout");
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.BookAbout).HasColumnName("BookAbout");
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.IsDangerous).HasColumnName("IsDangerous");
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.ArmCount).HasColumnName("ArmCount");
            modelBuilder.Entity<HistoricalFigure>().Property(w => w.IsReptilian).HasColumnName("IsReptilian");
        }

        //IDataModel
        IQueryable<Book> IDataModel.Books => Books;

        IQueryable<Dinosaur> IDataModel.Dinosaurs => Dinosaurs;
        
        IQueryable<HistoricalFigure> IDataModel.HistoricalFigures => HistoricalFigures;

    }



    class Program
    {
        static void Main(string[] args)
        {

            using (var dbContext = new DbEfDataContext(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\education\Ivan_Lebedkin_task7\LinqExample\LinqToDbEf\BookDinoHistoryDB.mdf;Integrated Security=True"))
            {
                //dbContext.ShowOutput();
                dbContext.ShowTheBiggestDinosaurs();
            }

            //using (var dbContext = new DbEfDataContext("Server=tcp:chdenis.database.windows.net,1433;Initial Catalog=dbBooks;Persist Security Info=False;User ID=chesdenis;Password=Xsw2cde3vfr4;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //{
            //    dbContext.ShowOutput();
            //}
        }
    }
}
