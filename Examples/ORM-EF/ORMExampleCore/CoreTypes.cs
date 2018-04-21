using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMExampleCore
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }

    public class Employee : Person
    {
        public string Position { get; set; }
    }

    public class ContactPerson : Person
    {
        public string Organization { get; set; }
    }

    public interface ICollectionsModification<in T> where T : Person
    {
        int Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }

    public interface IDataContext : 
        ICollectionsModification<Person>, 
        ICollectionsModification<Employee>,
        ICollectionsModification<ContactPerson>
    {
        IQueryable<Person> Persons { get; }

        IQueryable<Employee> Employees { get;  }

        IQueryable<ContactPerson> ContactPersons { get; }

        void SaveChanges();
    }

   
    public class BussinesService
    {
        private readonly IDataContext dataContext;

        public BussinesService(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Employee> GetMostWantedEmployees(string nameFragment)
        {
            return this.dataContext.Employees.Where(w => 
            w.FirstName.Contains(nameFragment) 
            | 
            w.LastName.Contains(nameFragment));
        }

        public void RegisterNewEmployeeAndCreateRelatedContact(string firstName, string lastName)
        {
            var employee = new Employee()
            {
                FirstName = firstName,
                LastName = lastName
            };

            var contactPerson = new ContactPerson()
            {
                FirstName = $"Contact: {firstName}",
                LastName = $"Contact: {lastName}",
                Organization = "EPAM"
            };

            this.dataContext.Add(employee);

            this.dataContext.Add(contactPerson);
            
            this.dataContext.SaveChanges();
        } 
    }
}
