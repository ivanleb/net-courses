using ORMExampleCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace ORMExampleConsoleApp
{

    public class TablePerHierarchyContext : BaseDbContext
    {
        public TablePerHierarchyContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("tblData");

            modelBuilder.Entity<Person>().HasIndex(p => p.Id).IsClustered().IsUnique();

            modelBuilder.Entity<Person>()
               .Map<Employee>(m => m.Requires("Discriminator").HasValue("Employee"))
               .Map<ContactPerson>(m => m.Requires("Discriminator").HasValue("ContactPerson"));

            modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<Person>().Property(p => p.FirstName).HasColumnName("FirstName");
            modelBuilder.Entity<Person>().Property(p => p.LastName).HasColumnName("LastName");

            modelBuilder.Entity<Employee>().Property(p => p.Position).HasColumnName("Position");
            modelBuilder.Entity<ContactPerson>().Property(p => p.Organization).HasColumnName("Organization");
        }

       
    }
}
