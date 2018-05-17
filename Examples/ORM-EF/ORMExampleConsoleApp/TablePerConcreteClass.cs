using ORMExampleCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ORMExampleConsoleApp
{
    public class TablePerConcreteClass : BaseDbContext
    {
        public TablePerConcreteClass(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Employee>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Employees");
            });

            modelBuilder.Entity<ContactPerson>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("ContactPersons");
            });

            //modelBuilder.Entity<Person>().ToTable("Persons").HasKey(p => p.Id);
            //modelBuilder.Entity<Employee>().ToTable("Employees").HasKey(p => p.Id);
            //modelBuilder.Entity<ContactPerson>().ToTable("ContactPersons").HasKey(p => p.Id);

            //modelBuilder.Entity<Person>().Property(p => p.Id)
            // .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<Employee>().Property(p => p.Id)
            //    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ContactPerson>().Property(p => p.Id)
            //  .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }
}
