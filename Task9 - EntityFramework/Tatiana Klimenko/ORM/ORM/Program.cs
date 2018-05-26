using System;
using System.Linq;
using System.Threading.Tasks;
using ORM.Core;
using ORM.Implementations;
using log4net;
using log4net.Config;
using System.Data.Entity;
using ORM.Core.Entities;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new StockExchangeDBContext())
            {
                XmlConfigurator.Configure();
                var logger = LogManager.GetLogger("StockExchangeLogger");
                var loggerService = new LoggerService(logger);

                var businessLogic = new BusinessLogic(dbContext, loggerService);
                var simulator = new Simulation(businessLogic, loggerService);

                Database.SetInitializer(new EFInitializer(businessLogic));

                var clients = businessLogic.GetClients();
                businessLogic.ShowClients(clients.ToList());
                

                Console.WriteLine("\nClients in orange area:");
                businessLogic.ShowClients(businessLogic.GetClientsFromOrangeZone());
                                
                Console.WriteLine("\nDeals are producing. Press Enter to stop producing/");
                Task.Factory.StartNew(() =>
                {
                    simulator.Run();
                });

                Console.ReadLine();
                simulator.IsContinue = false;

                Console.WriteLine("\nDeals producing stopped. Clients table is updated:");
                businessLogic.ShowClients(clients.ToList());

                Console.ReadLine();
            }
        }
    }
}
