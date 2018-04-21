using ORMExampleCore;
using System.Data.Entity;
using System.Linq;

namespace ORMExampleConsoleApp
{
    public abstract class BaseDbContext : DbContext, IDataContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<ContactPerson> ContactPersons { get; set; }

        public DbSet<Person> Persons { get; set; }

        IQueryable<Person> IDataContext.Persons => this.Persons;
        IQueryable<Employee> IDataContext.Employees => this.Employees;
        IQueryable<ContactPerson> IDataContext.ContactPersons => this.ContactPersons;

        public int Add(Person entity) => this.Persons.Add(entity).Id;
        public void Remove(Person entity) => this.Persons.Remove(entity);
        public void Update(Person entity)
        {
            var modified = this.Persons.First(f => f.Id == entity.Id);

            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
        }

        public int Add(Employee entity) => this.Employees.Add(entity).Id;

        public void Remove(Employee entity) => this.Employees.Remove(entity);

        public void Update(Employee entity)
        {
            var modified = this.Employees.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
            modified.Position = entity.Position;
        }

        public int Add(ContactPerson entity) => this.ContactPersons.Add(entity).Id;

        public void Remove(ContactPerson entity) => this.ContactPersons.Remove(entity);

        public void Update(ContactPerson entity)
        {
            var modified = this.ContactPersons.First(f => f.Id == entity.Id);
            modified.FirstName = entity.FirstName;
            modified.LastName = entity.LastName;
            modified.Organization = entity.Organization;
        }


        void IDataContext.SaveChanges() => this.SaveChanges();

        protected BaseDbContext(string connectionString) : base(connectionString)
        {

        }
    }
}
