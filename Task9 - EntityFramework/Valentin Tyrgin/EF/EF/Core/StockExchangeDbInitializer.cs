using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EF.Core;
using EF.Implementations;

namespace EF
{
    public class StockExchangeDbInitializer : DropCreateDatabaseAlways<StockExchangeDataContext>
    {
        protected override void Seed(StockExchangeDataContext context)
        {
            var stockTypeList = new List<StockType>
            {
                new StockType {Type = "Газпром", Price = 145.65M},
                new StockType {Type = "ЛУКОЙЛ", Price = 4450M},
                new StockType {Type = "Netflix", Price = 20192.67M},
                new StockType {Type = "Twitter", Price = 2032.03M},
                new StockType {Type = "Microsoft Corp", Price = 5999.67M}
            };
            var traiderList = new List<Traider>
            {
                new Traider {FirstName = "Иван", SecondName = "Иванов", Balance = 50000M, Phone = "7911-111-11-11"},
                new Traider {FirstName = "Алексей", SecondName = "Алексеев", Balance = 50000M, Phone = "7922-222-22-22"},
                new Traider {FirstName = "Сергей", SecondName = "Сергеев", Balance = 50000M, Phone = "7933-333-33-33"},
                new Traider {FirstName = "Михаил", SecondName = "Михайлов", Balance = 50000M, Phone = "7944-444-44-44"},
                new Traider {FirstName = "Олег", SecondName = "Тиньков", Balance = 0, Phone = "7955-555-55-55"}
            };
            var stockList = from stype in stockTypeList
                select new Stock {StockType = stype, Quantity = 50000 / (int) stype.Price, Traider = traiderList[4]};
            foreach (var stockType in stockTypeList)
                context.StockTypes.Add(stockType);
            foreach (var traider in traiderList)
                context.Traiders.Add(traider);
            foreach (var stock in stockList)
                context.Stocks.Add(stock);
            context.SaveChanges();
        }
    }
}