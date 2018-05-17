using ORMExampleCore;
using System.Data.Entity;

namespace ORMExampleConsoleApp
{
    public class TablePerTypeContext : BaseDbContext
    {
        public TablePerTypeContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<ContactPerson>().ToTable("ContactPersons");
        }
    }
}
