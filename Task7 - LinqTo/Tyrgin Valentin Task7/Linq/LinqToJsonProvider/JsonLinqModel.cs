using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Newtonsoft.Json.Linq;

namespace LinqToJsonProvider
{
    internal class JsonLinqModel:IDataModel
    {
        private readonly JObject jsonDataProvider;

        public JsonLinqModel(string pathToDataFile)
        {
            this.jsonDataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Car> Cars
        {
            get
            {
                return jsonDataProvider["Cars"].Select(s => new Car()
                {
                    Name = s["Name"].Value<string>(),
                    Price = s["Price"].Value<decimal>(),
                    FuelConsumption = s["FuelConsumption"].Value<double>(),
                    HorsePower = s["HorsePower"].Value<int>(),
                    EngineCapacity = s["EngineCapacity"].Value<double>(),
                    Brend = s["Brend"].Value<string>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }
        public IQueryable<Dealer> Dealers
        {
            get
            {
                return jsonDataProvider["Dealers"].Select(s => new Dealer()
                {
                    Title = s["Title"].Value<string>(),
                    Location = s["Location"].Value<string>(),
                    CarsNumber = s["CarsNumber"].Value<int>(),
                    Employee = s["Employee"].Value<int>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }
        public IQueryable<Order> Orders
        {
            get
            {
                return jsonDataProvider["Orders"].Select(s => new Order()
                {
                    CustomerName = s["CustomerName"].Value<string>(),
                    CarId = s["CarId"].Value<int>(),
                    DealerId = s["DealerId"].Value<int>(),
                    Date = s["Date"].Value<DateTime>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }
    }
}
