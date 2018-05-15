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


        public TradingSimulator(ILoggerService loggerService)
        {
            this.loggerService = loggerService;

        }

        public bool IsContinue { get; set; }

        public void Run()
        {
            IsContinue = true;
            using (var dbContext = new TPTUserDbContext("Server=tcp:chesnov.database.windows.net,1433;Initial Catalog=Task9Chesnov;Persist Security Info=False;User ID=;Password==;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                var programmLogic = new ProgrammLogic(dbContext);

                var stockTypes = programmLogic.GetStockTypes().ToList();

                loggerService.Info("Printing info about all clients");
                foreach (var client in programmLogic.GetAllClients())
                {
                    loggerService.Info(client.ToString());
                }

                loggerService.Info("The programm starts in 5 seconds. To exit press 1. Every stock cost will change on each iteration");
                Thread.Sleep(5000);

                Random rnd = new Random();

                while (IsContinue)
                {
                    var buyer = programmLogic.GetAllClients().Where(b => b.Balance > 0).GetRandom();
                    var seller = programmLogic.GetAllClients().Where(c => c.Id != buyer.Id).Where(c=>c.ClientStocksForSale.Count>0).GetRandom();
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
                    programmLogic.NewDeal(newDeal);
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