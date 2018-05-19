using System;
using System.Threading.Tasks;
using ORM.Core;
using ORM.ConsoleApp.Implementations;

namespace ORM.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var loggerService = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            using (var dbContext = new TradeDBContext())
            {
                var businessService = new BusinessService(dbContext, loggerService);
                var simulation = new Simulation(businessService, loggerService);

                Task.Run(() =>
                {
                    simulation.Run();
                });

                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        simulation.KeepRunning = false;
                        break;
                    }
                }
            }
        }
    }
}
