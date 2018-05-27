using System;
using System.Threading.Tasks;
using DIAndUnitTests.ConsoleApp.Implementations;
using DIAndUnitTests.ConsoleApp.Refactoring;
using DIAndUnitTests.ConsoleApp.Refactoring.Implementation;
using DIAndUnitTests.Core;
using log4net;
using log4net.Config;

namespace DIAndUnitTests.ConsoleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var alphaConsoleRepository = new AlphaConsoleRepository();
            var alphaDataContext = new AlphaDataContext();
            var alphaService = new AlphaService(alphaDataContext, alphaConsoleRepository);
            
            alphaService.Run();

/*            XmlConfigurator.Configure();
            var loggerService = new LoggerService(LogManager.GetLogger("mylog"));

            using (var dataContext = new DataContext())
            {
                var businessService = new BusinessService(dataContext, loggerService);
                var simulator = new Simulator(businessService, loggerService);

                Task.Run(() => { simulator.Run(); });

                while (true)
                {
                    if (Console.ReadKey().Key.Equals(ConsoleKey.Q))
                    {
                        simulator.Stop();
                        break;
                    }
                }
            }*/
        }
        
        
    }
}