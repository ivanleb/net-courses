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
        private readonly JObject _dataProvider;

        public JsonLinqDataModel(string pathToDataFile)
        {
            _dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }

        public IQueryable<Product> Products
        {
            get
            {
                var rofl = _dataProvider["Products"];
                return _dataProvider["Products"].Select(s => new Product
                {
                    Name = s["Name"].Value<string>(),
                    UnitPrice = s["UnitPrice"].Value<decimal>(),
                    Category = s["Category"].Value<string>(),
                    UnitsInStock = s["UnitsInStock"].Value<int>(),
                    Id = s["Id"].Value<int>()
                }).AsQueryable();
            }
        }

        public IQueryable<Order> Orders {
            get
            {
                var rofl = _dataProvider["Orders"];
                return _dataProvider["Orders"].Select(s => new Order
                {
                    Id = s["OrderId"].Value<int>(),
                    OrderDate = s["OrderDate"].Value<DateTime>(),
                    CustomerId = s["CustomerId"].Value<int>(),
                    Total = s["Total"].Value<decimal>()
                }).AsQueryable();
            }
        }
        public IQueryable<Customer> Customers {
            get
            {
                var rofl = _dataProvider["Customers"];
                return _dataProvider["Customers"].Select(s => new Customer
                {
                    Name = s["Name"].Value<string>(),
                    Address = s["Address"].Value<string>(),
                    City = s["City"].Value<string>(),
                    Region = s["Region"].Value<string>(),
                    Country = s["Country"].Value<string>(),
                    Id = s["Id"].Value<int>(),
                    Orders = s["Orders"].ToObject<List<int>>(),
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
