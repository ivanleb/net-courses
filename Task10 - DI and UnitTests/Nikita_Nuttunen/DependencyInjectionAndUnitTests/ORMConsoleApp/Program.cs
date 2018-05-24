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
                var logger = LogManager.GetLogger("StockExchangeLogger");

                Container container = new Container(_ =>
                {
                    _.For<ILoggerService>().Use<LoggerService>().Ctor<ILog>().Is(logger);
                    _.For<IDealProducer>().Use<DealProducer>();
                });


                var loggerService = container.GetInstance<ILoggerService>();

                var dealProducer = container.GetInstance<IDealProducer>();

                Task.Factory.StartNew(() =>
                {
                    loggerService.RunWithExceptionLogging(() => dealProducer.Run(), isSilent: true);
                });

                Console.ReadLine();
                dealProducer.IsContinue = false;
                               
                Console.ReadLine();
            }
        }
    }
}
