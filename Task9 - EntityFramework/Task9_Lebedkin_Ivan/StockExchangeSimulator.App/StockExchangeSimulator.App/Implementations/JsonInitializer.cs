using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.App.Implementations
{
    public class JsonInitializer
    {
        private readonly JObject _dataProvider;

        public JsonInitializer(string jsonFilePath)
        {
            _dataProvider = JObject.Parse(File.ReadAllText(jsonFilePath));
        }

        public IQueryable<Stock> Stocks
        {
            get
            {
                return _dataProvider["Stocks"].Select(s => new Stock()
                {
                    Id = s["Id"].Value<Int32>(),
                    Name = s["Name"].Value<string>(),
                    Price = s["Price"].Value<Int32>(),
                    Type = (StockTypeEnum)s["Type"].Value<Int32>()
                }).AsQueryable();
            }
        }
        public IQueryable<Client> Clients
        {
            get
            {
                var smth = (from stock in this.Stocks where stock.Id == 1 select stock).First();
                //Console.WriteLine(smth.Name);
                return _dataProvider["Clients"].Select(s => new Client()
                {
                    Id = s["Id"].Value<Int32>(),
                    FirstName = s["FirstName"].Value<string>(),
                    SurName = s["SurName"].Value<string>(),
                    TelephonNumber = s["TelephonNumber"].Value<string>(),
                    Balance = s["Balance"].Value<Int32>(),
                    //Zone = (ZoneType)s["Zone"].Value<byte>(),
                    Stock = new Stock()
                    {
                        Id = s["Stock"].Value<Int32>(),
                        Name = (from stock in this.Stocks where stock.Id == s["Stock"].Value<Int32>() select stock).First().Name,
                        Price = (from stock in this.Stocks where stock.Id == s["Stock"].Value<Int32>() select stock).First().Price,
                        Type = (from stock in this.Stocks where stock.Id == s["Stock"].Value<Int32>() select stock).First().Type
                    },
                    ClientStocksQuantity = s["ClientStocksQuantity"].Value<Int32>()
                }).AsQueryable();
            }
        }
    }
}
