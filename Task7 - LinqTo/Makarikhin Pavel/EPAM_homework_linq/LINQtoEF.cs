using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EPAM_homework_linq
{
    class DbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Movie> Movies { get; set; }

        IQueryable<Song> IDataModel.Songs => Songs;

        IQueryable<Picture> IDataModel.Pictures => Pictures;

        IQueryable<Movie> IDataModel.Movies => Movies;

        public DbEfDataContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().ToTable("Songs"); 
            modelBuilder.Entity<Song>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Song>().Property(w => w.Author).HasColumnName("Author");
            modelBuilder.Entity<Song>().Property(w => w.Album).HasColumnName("Album");
            modelBuilder.Entity<Song>().Property(w => w.ChartPosition).HasColumnName("ChartPosition");
            modelBuilder.Entity<Song>().Property(w => w.Duration).HasColumnName("Duration");

            modelBuilder.Entity<Picture>().ToTable("Pictures");
            modelBuilder.Entity<Picture>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Picture>().Property(w => w.Author).HasColumnName("Author");
            modelBuilder.Entity<Picture>().Property(w => w.Age).HasColumnName("Age");
            modelBuilder.Entity<Picture>().Property(w => w.Style).HasColumnName("Style");
            modelBuilder.Entity<Picture>().Property(w => w.Cost).HasColumnName("Cost");

            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Movie>().Property(w => w.Name).HasColumnName("Name");
            modelBuilder.Entity<Movie>().Property(w => w.Genre).HasColumnName("Author");
            modelBuilder.Entity<Movie>().Property(w => w.Rating).HasColumnName("Rating");
            modelBuilder.Entity<Movie>().Property(w => w.Duration).HasColumnName("Duration");
        }
    }
}
