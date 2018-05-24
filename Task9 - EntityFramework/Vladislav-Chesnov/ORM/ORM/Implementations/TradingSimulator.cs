using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Logging;
using ORMCore;
using ORMCore.Model;
using ORMCore.Abstractions;
using System.Threading;

namespace ORM.Implementations
{
    public class TradingSimulator
    {
        private readonly ILoggerService loggerService;

        private readonly string connectionString;

        public TradingSimulator(ILoggerService loggerService, string connectionString)
        {
            this.loggerService = loggerService;
            this.connectionString = connectionString;
        }

        public bool IsContinue { get; set; }

        public void FillDbWithData()
        {
            using (var dbContext = new TPTUserDbContext(connectionString))
            {
                var buisnessService = new BuisnessService(dbContext);

                loggerService.Info("registering new clients");
                buisnessService.AddNewClient("Benzoline", "Cucumbersnatch", "89215135213", 1000);
                buisnessService.AddNewClient("John", "Smith", "85123163241", 500);
                buisnessService.AddNewClient("Lucifer", "Betrayer", "86669341246", 1500);
                buisnessService.AddNewClient("Viktor", "Chestinov", "89712351783", 2000);
                buisnessService.AddNewClient("Gennadiy", "Ikarov", "86123651231", 1700);
                buisnessService.AddNewClient("Valentina", "Pristavko", "+71426123612", 1200);
                buisnessService.AddNewClient("The God-Emperor", "Of Mankind", "+71426123612", 5000);
                buisnessService.AddNewClient("Patrissia", "Bublegum", "+71426123612", 100);

                loggerService.Info("Registering new stock types");
                buisnessService.AddNewStockType("Rosal", 100);
                buisnessService.AddNewStockType("Nestle", 200);
                buisnessService.AddNewStockType("Tesla", 60);
                buisnessService.AddNewStockType("Blackguard", 50);
                buisnessService.AddNewStockType("Epic Games", 70);
                buisnessService.AddNewStockType("Sven", 53);
                buisnessService.AddNewStockType("Pioneer", 25);

                loggerService.Info("adding stocks to clients");
                for (int i = 0; i < 100; i++)
                {
                    buisnessService.AddStockToClient(buisnessService.GetStockTypes().GetRandom().Name, buisnessService.GetAllClients().GetRandom());
                }
            }
        }

        public void Run()
        {
            IsContinue = true;
            using (var dbContext = new TPTUserDbContext(connectionString))
            {
                var buisnessService = new BuisnessService(dbContext);

                var stockTypes = buisnessService.GetStockTypes().ToList();
                loggerService.Info("Printing info about all clients");
                foreach (var client in buisnessService.GetAllClients())
                {
                    loggerService.Info(client.ToString());
                }

                loggerService.Info("The programm starts in 5 seconds. To exit press 1. Every stock cost will change on each iteration");
                Thread.Sleep(5000);

                Random rnd = new Random();

                while (IsContinue)
                {
                    var buyer = buisnessService.GetAllClients().Where(b => b.Balance > 0).GetRandom();
                    var seller = buisnessService.GetAllClients().Where(c => c.Id != buyer.Id).Where(c=>c.ClientStocksForSale.Count>0).GetRandom();
                    var stock = seller.ClientStocksForSale.GetRandom();
                    var sum = stock.Type.Cost;
                    Deal newDeal = new Deal
                    {
                        Buyer = buyer,
                        Seller = seller,
                        Stock = stock,
                        Sum = sum
                    };
                    loggerService.Info($"Deal begins. Buyer is - {buyer.Name} {buyer.Surname}, balance: {buyer.Balance}, seller - {seller.Name} {seller.Surname}, balance: {seller.Balance}\nbuyer buys and seller sells stock of {stock.Type.Name} company for the {stock.Type.Cost}$");
                    buisnessService.NewDeal(newDeal);
                    if (buyer.ClientZone == Zone.Orange) loggerService.Warn("Attention! Client's balance is 0. Client's ability to buy stocks is suspended");
                    if (buyer.ClientZone == Zone.Black) loggerService.Warn("Attention! Client's balance below 0. Client's ability to buy stocks is suspended");
                    loggerService.Info($"Deal concluded. {buyer.Name}'s(buyer) balance now - {buyer.Balance}, {seller.Name}'s (seller) balance now - {seller.Balance}\n");

                    foreach(var type in stockTypes)
                    {
                        if (type.Cost < 1) type.Cost += rnd.Next(1, 8);
                        type.Cost += rnd.Next(-3, 3);
                    }

                    Thread.Sleep(5000);
                }
                loggerService.Info("Shutting down");
            }
        }

        public void StopSequence(object sender, CommandEventArgs eventArgs)
        {
            if (eventArgs.PressedKey.Key == ConsoleKey.D1) IsContinue = false;
        }
    }
}