using log4net;
using log4net.Config;
using ORMConsoleApp.Implementations;
using ORMCore;
using ORMCore.Abstractions;
using StructureMap;
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
                var container = new Container(new DIContainer());

                var loggerService = container.GetInstance<ILoggerService>();
                var businessService = container.GetInstance<BusinessService>();
                var dealProducer = container.GetInstance<IDealProducer>();

                Database.SetInitializer(new EfInitializer(businessService));

                Task.Factory.StartNew(() =>
                {
                    loggerService.RunWithExceptionLogging(() => dealProducer.Run());
                });

                Console.ReadLine();
                dealProducer.IsContinue = false;

                Console.ReadLine();
            }
        }
    }
}
