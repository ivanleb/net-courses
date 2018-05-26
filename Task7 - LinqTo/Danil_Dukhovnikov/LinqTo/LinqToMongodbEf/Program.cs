using System.Configuration;
using LinqTo.Core;

namespace LinqToMongodbEf
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext =
                new MongodbEfDataContext(ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString))
            {
                dbContext.ShowOutput();
            }
        }
    }
}