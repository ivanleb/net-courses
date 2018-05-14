using LinqExampleCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToJsonProvider
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Room> Rooms {
            get
            {
                var dataprov = dataProvider["Rooms"];
                return dataProvider["Rooms"].Select(s => new Room
                {
                    PricePerDay = s["PricePerDay"].Value<decimal>(),
                    Category = s["Category"].Value<string>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Visit> Visits {
            get
            {
                var dataprov = dataProvider["Visits"];
                return dataProvider["Visits"].Select(s => new Visit
                {
                    Id = s["VisitId"].Value<int>(),
                    VisitDate = s["VisitDate"].Value<DateTime>(),
                    GuestId = s["GuestId"].Value<int>(),
                    DaysNumber = s["DaysNumber"].Value<int>(),
                    Total = s["Total"].Value<decimal>()
                }).AsQueryable();
            }
        }

        public IQueryable<Guest> Guests {
            get
            {
                var dataProv = dataProvider["Guests"];
                return dataProvider["Guests"].Select(s => new Guest
                {
                    Name = s["Name"].Value<string>(),
                    Address = s["Address"].Value<string>(),
                    City = s["City"].Value<string>(),
                    Country = s["Country"].Value<string>(),
                    Id = s["Id"].Value<int>(),
                    Visits = s["Visits"].ToObject<List<int>>(),
                }).AsQueryable();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");
            dataModel.ShowData();
            dataModel.ShowOperations();
            Console.ReadLine();
        }
    }
}
