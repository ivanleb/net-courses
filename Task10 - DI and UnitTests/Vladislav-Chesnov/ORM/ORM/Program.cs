using System;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using ORM.Logging;
using ORM.Implementations;
using ORMCore;
using ORMCore.Abstractions;
using ORMCore.Repositories;
using StructureMap;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            Container container = new Container(_ =>
            {
                _.For<IUserInput>().Use<UserInput>().Ctor<ConsoleKey>().Is(ConsoleKey.D1);
                _.For<ILoggerService>().Use<LoggerService>().Ctor<ILog>().Is(logger);
            });

            var userInput = container.GetInstance<IUserInput>();

            var loggerService = container.GetInstance<ILoggerService>();

            var connectionString = "Server=tcp:chesnov.database.windows.net,1433;Initial Catalog=Task9Chesnovtest;Persist Security Info=False;User ID=Chesanita;Password=c6119900@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
