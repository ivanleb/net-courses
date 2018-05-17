using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LinqCore;

namespace LinqToJsonProvIder
{
    class JsonLinqDataModel : IDataModel
    {
        private readonly JObject dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Shop> Shops
        {
            get
            {
                return dataProvider["Shops"].Select(s => new Shop()
                {
                    Name = s["Name"].Value<string>(),
                    Street = s["Street"].Value<string>(),
                    Brand = s["Brand"].Value<string>(),
                    SquareInMetrs = s["SquareInMetrs"].Value<int>(),
                    DailyProceeds = s["DailyProceeds"].Value<double>()
                }).AsQueryable();
            }
        }

        public IQueryable<Restaurant> Restaurants
        {
            get
            {
                return dataProvider["Restaurants"].Select(s => new Restaurant()
                {
                    Name = s["Name"].Value<string>(),
                    Style = s["Type"].Value<string>(),
                    AverageBill = s["AverageBill"].Value<double>(),
                    Kitchen = s["Kitchen"].Value<string>(),
                    CapacityVisitors = s["CapacityVisitors"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Hotel> Hotels
        {
            get
            {
                return dataProvider["Hotels"].Select(s => new Hotel()
                {
                    Name = s["Name"].Value<string>(),
                    YearBuilt = s["YearBuilt"].Value<int>(),
                    NumberRooms = s["NumderRooms"].Value<int>(),
                    IsAdultOnle = s["IsAdultOnly"].Value<bool>()
                }).AsQueryable();
            }
        }

        static void Main(string[] args)
        {
            IDataModel dataModel = new JsonLinqDataModel(".\\data.json");
            dataModel.ShowOutput();
        }
    }
}
