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
            Container container = new Container(_ => {
                _.For<IUserInput>().Use<ConsoleUserInput>();
            });
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);

            IUserInput input = container.GetInstance<IUserInput>(new ExplicitArguments(new Dictionary<string, object> { ["keyToStopListening"] = ConsoleKey.Escape }));// new ConsoleUserInput(ConsoleKey.Escape);
                using (var dbContext = new MyDbContext("Data Source=.;Initial Catalog=StockExchangeDB;Integrated Security=True"))
                {
                    var bussinesService = new BussinesService(dbContext);

                    Database.SetInitializer(new EfInitializer(bussinesService));

                    var stockExchange = new SimpleStockExchange(bussinesService);

                    input.OnUserInputRecieved += (sender, keyInfo) => { if (keyInfo == ConsoleKey.Q) stockExchange.IsContinue = false; };

                    loggerService.Warning("Opening the stock exchange.");
                    loggerService.RunWithExceptionLogging(() => {
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
}
;