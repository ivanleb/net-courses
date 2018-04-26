using log4net;
using log4net.Config;
using ORMConsoleApp.Implementations;
using ORMCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new StockExchangeContext())
            {
                XmlConfigurator.Configure();
                var logger = LogManager.GetLogger("StockExchangeLogger");

                var loggerService = new LoggerService(logger);
                var businessService = new BusinessService(dbContext, loggerService);
                var dealProducer = new DealProducer(businessService, loggerService);

                var stock1 = new Stock() { Type = "Gazprom" };
                var stock2 = new Stock() { Type = "Tesla" };
                var stock3 = new Stock() { Type = "Apple" };
                var stock4 = new Stock() { Type = "Facebook" };
                var stock5 = new Stock() { Type = "Google" };
                var stock6 = new Stock() { Type = "Coca-Cola" };
                var stock7 = new Stock() { Type = "Coca-Cola" };
                var stock8 = new Stock() { Type = "Gazprom" };
                var stock9 = new Stock() { Type = "Lenovo" };
                var stock10 = new Stock() { Type = "Samsung" };

                var clients = new List<Client>()
                {
                    new Client() { Name = "John", Surname = "Smith", PhoneNumber = "9117539711", Balance = 10000, Area = "green", Stocks = new List<Stock>() { stock1, stock3 } },
                    new Client() { Name = "George", Surname = "Green", PhoneNumber = "9218883910", Balance = 0, Area = "orange", Stocks = new List<Stock>() { stock2 } },
                    new Client() { Name = "Mark", Surname = "Brown", PhoneNumber = "9992177819", Balance = -2000, Area = "black", Stocks = new List<Stock>() { stock4, stock7, stock8 } },
                    new Client() { Name = "Bruce", Surname = "White", PhoneNumber = "9004722910", Balance = 5000, Area = "green", Stocks = new List<Stock>() },
                    new Client() { Name = "Alex", Surname = "Walker", PhoneNumber = "9132897338", Balance = 0, Area = "orange", Stocks = new List<Stock>() { stock6 } },
                    new Client() { Name = "Peter", Surname = "Gregory", PhoneNumber = "9831912844", Balance = 0, Area = "orange", Stocks = new List<Stock>() },
                    new Client() { Name = "Jack", Surname = "Bellson", PhoneNumber = "9037187387", Balance = 2000, Area = "green", Stocks = new List<Stock>() { stock9 } },
                    new Client() { Name = "Victor", Surname = "Ivanov", PhoneNumber = "97781920015", Balance = 0, Area = "orange", Stocks = new List<Stock>() { stock10 } }
                };

                Console.WriteLine("Registered stocks:");
                businessService.RegisterNewStock(stock1);
                businessService.RegisterNewStock(stock2);
                businessService.RegisterNewStock(stock3);
                businessService.RegisterNewStock(stock4);
                businessService.RegisterNewStock(stock5);
                businessService.RegisterNewStock(stock6);
                businessService.RegisterNewStock(stock7);
                businessService.RegisterNewStock(stock8);
                businessService.RegisterNewStock(stock9);
                businessService.RegisterNewStock(stock10);

                Console.WriteLine("\nRegistered clients:");
                foreach (var client in clients)
                {
                    businessService.RegisterNewClient(client);                    
                }

                Console.WriteLine("\nClients from DB:");
                clients = businessService.GetClientsList();
                foreach (var client in clients)
                {
                    businessService.ShowClient(client);
                }

                Console.WriteLine("\nClients in orange area:");
                var orangeClients = businessService.GetClientsFromOrangeArea();
                foreach (var client in orangeClients)
                {
                    businessService.ShowClient(client);
                }

                businessService.ChangeStockType(dbContext.Stocks.First(c => c.Id == 1), "Tesla");

                Console.WriteLine("\nDeals will be produced soon. Press Enter to stop producing");
                System.Threading.Thread.Sleep(7000);

                Task.Factory.StartNew(() =>
                {
                    dealProducer.Run();
                });

                Console.ReadLine();
                dealProducer.IsContinue = false;

                Console.WriteLine("\nDeals producing stopped. Updated clients table:");
                foreach (var client in clients)
                {
                    businessService.ShowClient(client);
                }
                Console.ReadLine();
            }
        }
    }
}
