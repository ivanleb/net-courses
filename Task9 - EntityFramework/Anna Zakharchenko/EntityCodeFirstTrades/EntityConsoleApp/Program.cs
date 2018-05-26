using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityConsoleApp.Implementations;
using EntityCore;
using log4net.Config;
using log4net;

namespace EntityConsoleApp
{
    class Program
    {
        private static Dictionary<string, decimal> allStocksType = new Dictionary<string, decimal>
        {
            {"Tatneft", 3898},
            {"NLMK", 1610 },
            {"LUKOIL NK", 4328},
            {"DIXY Group", 320 },
            {"AvtoVAZ", 150 },
            {"Raspadskaya", 950 },
            {"Yandex" , 21918 },
            {"Severstal", 1915 },
            {"Rosneft Oil Company", 4008 },
            {"Norilsk Nickel", 10670 },
            {"KAMAZ", 320 }
        };

        static void CreateDB(BussinesService bussinesService, LoggerService loggerService)
        {
            loggerService.Info("Create a database...");
            
            bussinesService.RegisterNewClient("Johann", "Bach", "839404983", 473637849);
            bussinesService.RegisterNewClient("Wolfgang", "Mozart", "6272893", 2638);
            bussinesService.RegisterNewClient("Giuseppe", "Verdi", "896232", 855790);
            bussinesService.RegisterNewClient("Sergei", "Rahmaninov", "213568", 5670);
            bussinesService.RegisterNewClient("Franz", "Schubert", "645689", 54680);

            foreach (var stock in allStocksType)
            {
                if ((stock.Key != "Tatneft") || (stock.Key != "NLMK"))
                {
                    bussinesService.RegisterNewStock(bussinesService.GetClient(1), stock);
                }

                if ((stock.Key != "KAMAZ") || (stock.Key != "Norilsk Nickel"))
                {
                    bussinesService.RegisterNewStock(bussinesService.GetClient(2), stock);
                }

                if ((stock.Key != "DIXY Group") || (stock.Key != "Severstal"))
                {
                    bussinesService.RegisterNewStock(bussinesService.GetClient(3), stock);
                }

                if ((stock.Key != "AvtoVAZ") || (stock.Key != "Yandex"))
                {
                    bussinesService.RegisterNewStock(bussinesService.GetClient(4), stock);
                }

                if ((stock.Key != "Rosneft Oil Company") || (stock.Key != "LUKOIL NK"))
                {
                    bussinesService.RegisterNewStock(bussinesService.GetClient(5), stock);
                }
            }

            loggerService.Info("The database is created. We can start to trading!");
        }
        static void Main(string[] args)
        {
           using(var dbContext = new TablePerTypeContext())
            {
                XmlConfigurator.Configure();
                ILog logger = LogManager.GetLogger("TextLogger");
                LoggerService loggerService = new LoggerService(logger);

                BussinesService bussinesService = new BussinesService(dbContext, loggerService);

                CreateDB(bussinesService, loggerService);

                GoodTradeProducer producer = new GoodTradeProducer(loggerService, bussinesService);
                producer.OnBalanceChanged += bussinesService.NewTradeMade;
                producer.OnBalanceChanged += bussinesService.NewBalanceForSeller;

                Task.Run(()=>
                {
                    producer.Run(dbContext.Clients.Count());
                });

                Console.ReadLine();
                producer.IsContinue = false;
                Console.WriteLine("Stop trading!");
                Console.ReadKey();
            }
        }
    }
}
