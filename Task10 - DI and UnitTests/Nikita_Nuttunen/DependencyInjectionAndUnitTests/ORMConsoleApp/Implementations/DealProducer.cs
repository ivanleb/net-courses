using ORMCore;
using ORMCore.Abstractions;
using ORMCore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ORMConsoleApp.Implementations
{
    public class DealProducer : IDealProducer
    {
        private readonly BusinessService businessService;
        private readonly LoggerService loggerService;

        public bool IsContinue { get; set; }

        public DealProducer(BusinessService businessService, LoggerService loggerService)
        {
            this.businessService = businessService;
            this.loggerService = loggerService;
        }

        public void Run()
        {
            Random rnd = new Random();
            IsContinue = true;

            Console.WriteLine("Clients from database:");
            var clients = businessService.GetClients();
            foreach (var client in clients)
            {
                businessService.ShowClient(client);
            }

            Console.WriteLine("Clients from orange area:");
            var orangeClients = businessService.GetClientsFromOrangeArea();
            foreach (var client in orangeClients)
            {
                businessService.ShowClient(client);
            }

            var stock1 = businessService.GetStockById(1);
            businessService.ChangeStockType(stock1, "Tesla");

            Console.WriteLine("\nDeals will be produced soon. Press Enter to stop producing");
            System.Threading.Thread.Sleep(7000);

            while (IsContinue & businessService.GetClientsAmount() > 1)
            {
                var clientsAmount = businessService.GetClientsAmount();

                var seller = businessService.GetRandomClient();
                var purchaser = businessService.GetRandomClient();
                while (seller == purchaser)
                {
                    purchaser = businessService.GetRandomClient();
                }

                Console.WriteLine($"\n{seller.Name} {seller.Surname} is trying to sell a stock to {purchaser.Name} {purchaser.Surname}...");
                System.Threading.Thread.Sleep(1000);

                var stock = businessService.GetRandomSellerStock(seller);
                if (stock != null)
                {
                    var producedDeal = businessService.MakeDeal(seller, purchaser, stock);
                    businessService.RegisterNewDeal(producedDeal);
                    loggerService.Info($"{seller.Name} {seller.Surname} selled stock \"{stock.Type}\"" +
                        $" №{stock.Id} to {purchaser.Name} {purchaser.Surname} for {stock.Cost}");
                }
                System.Threading.Thread.Sleep(10000);
            }

            Console.WriteLine("\nDeals producing stopped. Updated clients table:");
            foreach (var client in clients)
            {
                businessService.ShowClient(client);
            }

        }
    }
}
