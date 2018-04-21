using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMDatabaseFirstConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbEntities = new OneToManyRelationsDbEntities())
            {
                var count = dbEntities.Humans.Count();

                var count2 = dbEntities.Vehicles.Count();
            }
        }
    }
     
}
