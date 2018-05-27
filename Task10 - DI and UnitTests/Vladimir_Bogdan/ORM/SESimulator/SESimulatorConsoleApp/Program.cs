using log4net;
using log4net.Config;
using SESimulator.Abstractions;
using SESimulatorConsoleApp.Implementations;
using SESimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SESimulator.Extentions;
using StructureMap;
using StructureMap.Pipeline;
using SESimulator.Logging;

namespace SESimulatorConsoleApp
{
    public class AlphaService
    {

        public void Run()
        {

        }
        class Program
        {
            static void Main(string[] args)
            {
                XmlConfigurator.Configure();

                Container container = new Container(_ =>
                {
                    _.For<IUserInput>().Use<ConsoleUserInput>().Ctor<ConsoleKey>().Is(ConsoleKey.Escape);
                    _.For<ILoggerService>().Use<LoggerService>().Singleton().Ctor<ILog>().Is(LogManager.GetLogger("SampleTextLogger"));
                    _.For<IStockExchange>().Use<SimpleStockExchange>();
                    _.For<BussinesService>().Use<BussinesService>();
                    _.For<IDataContext>().Use<MyDbContext>().Singleton().Ctor<string>().Is("Data Source=.;Initial Catalog=StockExchangeDB;Integrated Security=True");
                });

                var loggerService = container.GetInstance<ILoggerService>();

                IUserInput input = container.GetInstance<IUserInput>();

                var stockExchange = container.GetInstance<IStockExchange>();

                Database.SetInitializer(new EfInitializer(container.GetInstance<BussinesService>()));

                input.OnUserInputRecieved += (sender, keyInfo) => { if (keyInfo == ConsoleKey.Q) stockExchange.IsContinue = false; };

                loggerService.Warning("Opening the stock exchange.");

                loggerService.RunWithExceptionLogging(() =>
                {
                    Task.Run(() =>
                    {
                        stockExchange.Run((IDealInfo dealInfo) => { loggerService.Info($"{dealInfo.Seller} have sold {dealInfo.Stock} to  {dealInfo.Buyer} for {dealInfo.Amount}."); });
                    });
                }, isSilent: false);

                input.ListenToUser();
            }

        }
    }
}
;