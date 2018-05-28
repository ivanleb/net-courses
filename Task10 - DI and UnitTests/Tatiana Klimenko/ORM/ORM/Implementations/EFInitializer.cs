using System;
using System.Collections.Generic;
using ORM.Core;
using ORM.Core.Entities;
using System.Data.Entity;

namespace ORM.Implementations
{
    class EFInitializer : DropCreateDatabaseAlways<StockExchangeDBContext>
    {
        private BusinessLogic businessLogic;

        public EFInitializer(BusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        protected override void Seed(StockExchangeDBContext context)
        {
            var stocks = new List<Stock>()
            {
                new Stock() { Type = StockType.Gazprom },
                new Stock() { Type = StockType.GMKNorNik },
                new Stock() { Type = StockType.LUKOIL },
                new Stock() { Type = StockType.Magnit },
                new Stock() { Type = StockType.Novatek },
                new Stock() { Type = StockType.Rosneft },
                new Stock() { Type = StockType.Sberbank },
                new Stock() { Type = StockType.Severstal },
                new Stock() { Type = StockType.Gazprom },
                new Stock() { Type = StockType.GMKNorNik },
                new Stock() { Type = StockType.LUKOIL },
                new Stock() { Type = StockType.Magnit },
                new Stock() { Type = StockType.Novatek },
                new Stock() { Type = StockType.Rosneft },
            };

            var clients = new List<Client>()
                {
                    new Client() { Name = "Henry", Surname = "Marsh", PhoneNumber = "9115751564", Balance = 20000, Stocks = new List<Stock>() { stocks[0], stocks[1] } },
                    new Client() { Name = "Charlie", Surname = "Chinaski", PhoneNumber = "9218898761", Balance = 10000, Stocks = new List<Stock>() { stocks[2], stocks[3] } },
                    new Client() { Name = "Charls", Surname = "Bukovski", PhoneNumber = "9213456677", Balance = 8000, Stocks = new List<Stock>() { stocks[4], stocks[5], stocks[6] } },
                    new Client() { Name = "Pelham", Surname = "Wodehouse", PhoneNumber = "9118945672", Balance = 5000, Stocks = new List<Stock>() { stocks[7] } },
                    new Client() { Name = "Arthur", Surname = "Rimbaud", PhoneNumber = "9135346789", Balance = 0, Stocks = new List<Stock>() { stocks[8], stocks[9], stocks[10], stocks[11] } },
                    new Client() { Name = "Henri", Surname = "Gauguin", PhoneNumber = "9831912844", Balance = -500, Stocks = new List<Stock>() { stocks[12], stocks[13] } }
                };

            Console.WriteLine("Registered stocks:");
            foreach (var stock in stocks)
            {
                businessLogic.RegisterNewStock(stock);
            }

            Console.WriteLine("\nRegistered clients:");
            foreach (var client in clients)
            {
                businessLogic.RegisterNewClient(client);
            }

            context.SaveChanges();
        }
    }
}
