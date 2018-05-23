using System;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using ORM.Logging;
using ORM.Implementations;
using ORMCore;
using ORMCore.Abstractions;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            var loggerService = new LoggerService(logger);

            IUserInput userInput = new UserInput(ConsoleKey.D1);

            var connectionString = "Server=tcp:chesnov.database.windows.net,1433;Initial Catalog=Task9Chesnovtest;Persist Security Info=False;User ID=Chesanita;Password=**********@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            TradingSimulator tradingSimulator = new TradingSimulator(loggerService, connectionString);

            //tradingSimulator.FillDbWithData();

            userInput.OnKeyPressed += tradingSimulator.StopSequence;

            Task.Run(() =>
            {
                loggerService.RunWithExceptionLogging(() => tradingSimulator.Run());
            });

            userInput.ListenToUser();
        }
    }   
}
