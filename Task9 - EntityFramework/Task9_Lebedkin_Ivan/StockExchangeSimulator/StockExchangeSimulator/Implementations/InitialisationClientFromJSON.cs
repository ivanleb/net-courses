using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.DataModel;
using Newtonsoft.Json.Linq;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Implementations
{
    public class InitialisationClientFromJSON : IInitialClientsDataModel
    {
        private readonly JObject dataProvider;

        public InitialisationClientFromJSON(string pathToDataFile)
        {
            this.dataProvider = JObject.Parse(System.IO.File.ReadAllText(pathToDataFile));
        }
        public IQueryable<Stock> Stocks
        {
            get
            {
                return dataProvider["Stocks"].Select(s => new Stock()
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
                return dataProvider["Clients"].Select(s => new Client()
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
