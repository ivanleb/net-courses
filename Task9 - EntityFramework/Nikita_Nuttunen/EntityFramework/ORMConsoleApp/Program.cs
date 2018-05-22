using log4net;
using log4net.Config;
using ORMConsoleApp.Implementations;
using ORMCore;
using ORMCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new StockExchangeDbContext())
            {
                XmlConfigurator.Configure();
                var logger = LogManager.GetLogger("StockExchangeLogger");
                var loggerService = new LoggerService(logger);

                var businessService = new BusinessService(dbContext, loggerService);
                var dealProducer = new DealProducer(businessService, loggerService);

                Database.SetInitializer(new EfInitializer(businessService));

                var clients = businessService.GetClients();
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
