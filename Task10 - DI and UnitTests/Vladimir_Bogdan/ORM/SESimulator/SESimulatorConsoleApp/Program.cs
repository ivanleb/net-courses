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

namespace SESimulatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);

            IUserInput input = new ConsoleUserInput(ConsoleKey.Escape);

            using (var dbContext = new MyDbContext("Data Source=.;Initial Catalog=StockExchangeDB;Integrated Security=True"))
            {
                var bussinesService = new BussinesService(dbContext);

                Database.SetInitializer(new EfInitializer(bussinesService));

                var stockExchange = new SimpleStockExchange(bussinesService);

                loggerService.RunWithExceptionLogging(() =>
                {
                    bussinesService.RegisterNewStockToClient("NoNameCompany", bussinesService.GetAllClients().GetRandom());
                }, isSilent: true);

                loggerService.Warning("Changing any stock's cost.");
                bussinesService.ChangeStockCost("Telegram", bussinesService.GetStockCost("Telegram")+100);

                loggerService.Warning("All registered clients:\n");
                foreach(var client in bussinesService.GetAllClients())
                {
                    loggerService.Info(client.ToString());
                }

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
;