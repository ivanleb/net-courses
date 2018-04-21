using ORMExampleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var dbContext = new TablePerHierarchyContext(
            //    "Data Source=.;Initial Catalog=TablePerHierarchyExampleDb;Integrated Security=True"))
            //{
            //    var bussinesService = new BussinesService(dbContext);

            //    bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Abc", "Def");
            //    bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Ghi", "Jkl");

            //    Console.WriteLine("Sucessfully registered employees");

            //    var searchResult = bussinesService.GetMostWantedEmployees("bc")
            //        .Select(s => s.FirstName).ToArray();

            //    Console.WriteLine($"Found resuts: {string.Join(";", searchResult)}");

            //}

            //Console.WriteLine("Table Per Hierarchy Done");
            //Console.ReadLine();



            //Console.WriteLine("========================================================");


            using (var dbContext = new TablePerTypeContext(
               "Data Source=.;Initial Catalog=TablePerTypeExampleDb2;Integrated Security=True"))
            {
                var bussinesService = new BussinesService(dbContext);

                //bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Abc", "Def");
                //bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Ghi", "Jkl");

                Console.WriteLine("Sucessfully registered employees");

                var searchResult = bussinesService.GetMostWantedEmployees("bc")
                    .Select(s => s.FirstName).ToArray();

                var searchResult2 = bussinesService.GetMostWantedEmployees("bc")
                   .Select(s => s.FirstName).ToArray();

                Console.WriteLine($"Found resuts: {string.Join(";", searchResult)}");

            }

            Console.WriteLine("Table Per Type Done");
            Console.ReadLine();




            //Console.WriteLine("========================================================");

            /*
            using (var dbContext = new TablePerConcreteClass(
             "Data Source=.;Initial Catalog=TablePerConcreteClass;Integrated Security=True"))
            {
                var bussinesService = new BussinesService(dbContext);

                bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Abc", "Def");
                bussinesService.RegisterNewEmployeeAndCreateRelatedContact("Ghi", "Jkl");

                Console.WriteLine("Sucessfully registered employees");

                var searchResult = bussinesService.GetMostWantedEmployees("bc")
                    .Select(s => s.FirstName).ToArray();

                Console.WriteLine($"Found resuts: {string.Join(";", searchResult)}");

            }*/

            //Console.WriteLine("Table Per Type Done");
            //Console.ReadLine();

        }
    }
}
