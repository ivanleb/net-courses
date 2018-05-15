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

            using (var dbContext = new TPTUserDbContext("Server=tcp:chesnov.database.windows.net,1433;Initial Catalog=Task9Chesnov;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                var programmLogic = new ProgrammLogic(dbContext);
                TradingSimulator tradingSimulator = new TradingSimulator(loggerService);
                //logger.Info("registering new clients");
                //programmLogic.AddNewClient("Benzoline", "Cucumbersnatch", "89215135213", 1000);
                //programmLogic.AddNewClient("John", "Smith", "85123163241", 500);
                //programmLogic.AddNewClient("Lucifer", "Betrayer", "86669341246", 1500);
                //programmLogic.AddNewClient("Viktor", "Chestinov", "89712351783", 2000);
                //programmLogic.AddNewClient("Gennadiy", "Ikarov", "86123651231", 1700);
                //programmLogic.AddNewClient("Valentina", "Pristavko", "+71426123612", 1200);
                //programmLogic.AddNewClient("The God-Emperor", "Of Mankind", "+71426123612", 5000);
                //programmLogic.AddNewClient("Patrissia", "Bublegum", "+71426123612", 100);

                //logger.Info("Registering new stock types");
                //programmLogic.AddNewStockType("Rosal", 100);
                //programmLogic.AddNewStockType("Nestle", 200);
                //programmLogic.AddNewStockType("Tesla", 60);
                //programmLogic.AddNewStockType("Blackguard", 50);
                //programmLogic.AddNewStockType("Epic Games", 70);
                //programmLogic.AddNewStockType("Sven", 53);
                //programmLogic.AddNewStockType("Pioneer", 25);

                //logger.Info("adding stocks to clients");
                //for (int i = 0; i < 100; i++)
                //{
                //    programmLogic.AddStockToClient(programmLogic.GetStockTypes().GetRandom().Name, programmLogic.GetAllClients().GetRandom());
                //}


                userInput.OnKeyPressed += tradingSimulator.StopSequence;

                Task.Run(() =>
                {
                    loggerService.RunWithExceptionLogging(() => tradingSimulator.Run());
                });

                userInput.ListenToUser();
            }
        }
    }   
}
