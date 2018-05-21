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

                var stockExchange = new SimpleStockExchange(bussinesService);
                loggerService.Warning("Registering new clients in the StockExchangeDataContext");
                bussinesService.RegisterNewClient("Ilya", "Muromec", "12345", 1000);
                bussinesService.RegisterNewClient("Elena", "Prekrasnaya", "111", 800);
                bussinesService.RegisterNewClient("Ivan", "Durak", "", 200);
                bussinesService.RegisterNewClient("Vasilisa", "Premudraya", "555", 0);
                bussinesService.RegisterNewClient("Koshey", "Bessmertniy", "666", 3000);


                loggerService.Warning("Registering new stock types in the StockExchangeDataContext");
                bussinesService.RegisterNewStockType("Lukoil", 200);
                bussinesService.RegisterNewStockType("Gazprom", 400);
                bussinesService.RegisterNewStockType("Telegram", 400);

                loggerService.Warning("Registering new stocks in the StockExchangeDataContext");
                bussinesService.RegisterNewStockToClient("Lukoil", bussinesService.GetAllClients().GetRandom());
                bussinesService.RegisterNewStockToClient("Lukoil", bussinesService.GetAllClients().GetRandom());
                bussinesService.RegisterNewStockToClient("Gazprom", bussinesService.GetAllClients().GetRandom());
                bussinesService.RegisterNewStockToClient("Gazprom", bussinesService.GetAllClients().GetRandom());
                bussinesService.RegisterNewStockToClient("Telegram", bussinesService.GetAllClients().GetRandom());
                bussinesService.RegisterNewStockToClient("Telegram", bussinesService.GetAllClients().GetRandom());
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

                input.OnUserInputRecieved += (sender, keyInfo) => { if (keyInfo == ConsoleKey.D1) stockExchange.IsContinue = false; };

                loggerService.Warning("Opening the stock exchange.");
                loggerService.RunWithExceptionLogging(() => {
                    stockExchange.Run((IDealInfo dealInfo) => { loggerService.Info($"{dealInfo.Seller} have sold {dealInfo.Stock} to  {dealInfo.Buyer} for {dealInfo.Amount}."); });
                }, isSilent: false);

                input.ListenToUser();
            }
        }
    }
}
;