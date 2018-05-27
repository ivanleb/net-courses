using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EF.Implementations;
using EF.Implementations.Entities;

namespace EF.Core.Implementations
{
    public class StockExchangeDbInitializer : DropCreateDatabaseAlways<StockExchangeDataContext>
    {
        protected override void Seed(StockExchangeDataContext context)
        {
            var stockTypeList = new List<TradableType>
            {
                new TradableType {Type = "Газпром", Price = 145.65M},
                new TradableType {Type = "ЛУКОЙЛ", Price = 4450M},
                new TradableType {Type = "Netflix", Price = 20192.67M},
                new TradableType {Type = "Twitter", Price = 2032.03M},
                new TradableType {Type = "Microsoft Corp", Price = 5999.67M}
            };
            var traiderList = new List<Trader>
            {
                new Trader { TraderInfo = new IndividualInfo { FirstName = "Иван", SecondName = "Иванов",  ContactPhoneNumber = "7911-111-11-11"}, Balance = 50000M },
                new Trader { TraderInfo = new IndividualInfo {FirstName = "Алексей", SecondName = "Алексеев", ContactPhoneNumber = "7922-222-22-22"}, Balance = 50000M},
                new Trader { TraderInfo = new IndividualInfo {FirstName = "Сергей", SecondName = "Сергеев", ContactPhoneNumber = "7933-333-33-33"}, Balance = 50000M},
                new Trader { TraderInfo = new IndividualInfo {FirstName = "Михаил", SecondName = "Михайлов", ContactPhoneNumber = "7944-444-44-44"}, Balance = 50000M},
                new Trader { TraderInfo = new IndividualInfo {FirstName = "Олег", SecondName = "Тиньков", ContactPhoneNumber = "7955-555-55-55"}, Balance = 0}
            };
            var stockList = from stype in stockTypeList
                            select new Stock { TradableType = stype, Quantity = 50000 / (int)stype.Price, Trader = traiderList[4] };
            foreach (var stockType in stockTypeList)
                context.Add(stockType);
            //context.TradableTypes.Add(stockType);
            foreach (var traider in traiderList)
                context.Add(traider);
            foreach (var stock in stockList)
                context.Add(stock);
            context.SaveChanges();
        }
    }
}