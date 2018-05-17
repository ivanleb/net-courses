using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMExampleDatabaseFirstConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new OneToManyRelationsDbEntities())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;

                var human = new Human() { Name = "TestName" };

                dbContext.Humans.Add(human);

                dbContext.SaveChanges();

                human.Vehicles.Add(new Vehicle() { Name = "BMW" });

                dbContext.SaveChanges();


                var count = dbContext.Humans.First().Vehicles.Count;


                var human2 = dbContext.Humans.Include("Vehicles").First();

                var count2 = human2.Vehicles.Count;

            }
        }
    }
}
