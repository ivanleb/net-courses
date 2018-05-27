using System.Configuration;
using System.Linq;
using LinqTo.Core;
using MongoDB.Driver;

namespace LinqToMongodbProvider
{
    public class MongodbLinqDataModel : IDataModel
    {
        
        private readonly IMongoCollection<Project> dataProvider;

        public MongodbLinqDataModel()
        {
            var config = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var client = new MongoClient(config);
            
            var db = client.GetDatabase("db_mongo_server");
            dataProvider = db.GetCollection<Project>("Projects");
        }

        public IQueryable<Project> Projects => dataProvider.AsQueryable();
    }
}