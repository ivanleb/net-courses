using System;
using System.Linq;
using System.Threading.Tasks;
using ORM.Core;
using ORM.Implementations;
using log4net;
using log4net.Config;
using System.Data.Entity;
using ORM.Core.Entities;
using StructureMap;
using ORM.Core.Abstractions;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new StockExchangeDBContext())
            {
                XmlConfigurator.Configure();

                var container = new Container(new DIContainer());

                var loggerService = container.GetInstance<ILoggerService>();
                var businessLogic = container.GetInstance<BusinessLogic>();
                var simulator = container.GetInstance<ISimulation>();                

                Database.SetInitializer(new EFInitializer(businessLogic));

                Task.Factory.StartNew(() =>
                {
                    simulator.Run();
                });

                Console.ReadLine();
                simulator.IsContinue = false;
                
                Console.ReadLine();
            }
        }
    }
}
