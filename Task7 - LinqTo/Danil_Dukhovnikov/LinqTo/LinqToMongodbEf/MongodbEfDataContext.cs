using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LinqTo.Core;

namespace LinqToMongodbEf
{
    public class MongodbEfDataContext : DbContext, IDataModel
    {
        public DbSet<Project> Projects { get; set; }
        
        IQueryable<Project> IDataModel.Projects => Projects;

        public MongodbEfDataContext(string connectionString) : base(connectionString)
        {

        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .ToTable("Projects")
                .HasKey(k => k.ProjectId)
                .HasMany(p => p.Categories);
            modelBuilder.Entity<Project>().Property(w => w.ProjectName).HasColumnName("projectName");
            modelBuilder.Entity<Project>().Property(w => w.ProjectBalance).HasColumnName("projectBalance");
            modelBuilder.Entity<Project>().Property(w => w.ProjectBudget).HasColumnName("projectBudget");

            modelBuilder.Entity<Category>()
                .ToTable("categories")
                .HasKey(k => k.CategoryId)
                .HasMany(p => p.Transactions);
            modelBuilder.Entity<Category>().Property(w => w.CategoryName).HasColumnName("categoryName");

            modelBuilder.Entity<Transaction>()
                .ToTable("transactions")
                .HasKey(k => k.TransactionId);
            modelBuilder.Entity<Transaction>().Property(w => w.Comment).HasColumnName("comment");
            modelBuilder.Entity<Transaction>().Property(w => w.Instant).HasColumnName("instant");
            modelBuilder.Entity<Transaction>().Property(w => w.Amount).HasColumnName("amount");
        }


    }
}