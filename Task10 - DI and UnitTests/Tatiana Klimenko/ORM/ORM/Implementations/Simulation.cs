using System;
using ORM.Core.Abstractions;
using ORM.Core;
using System.Linq;

namespace ORM.Implementations
{
    public class Simulation : ISimulation
    {
        private readonly LoggerService loggerService;
        private readonly BusinessLogic businessLogic;

        public bool IsContinue { get; set; }

        public Simulation(BusinessLogic businessLogic, LoggerService loggerService)
        {
            this.businessLogic = businessLogic;
            this.loggerService = loggerService;
        }

        public void Run()
        {
            Random rnd = new Random();
            IsContinue = true;

            var clients = businessLogic.GetClients();
            businessLogic.ShowClients(clients.ToList());


            Console.WriteLine("\nClients in orange area:");
            businessLogic.ShowClients(businessLogic.GetClientsFromOrangeZone());

            Console.WriteLine("\nDeals are producing. Press Enter to stop producing/");

            loggerService.RunWithExceptionLogging(() =>
            {
                while (IsContinue & businessLogic.GetClientsAmount() > 1)
                {
                    var clientsAmount = businessLogic.GetClientsAmount();

                    var seller = businessLogic.GetRandomClient();
                    var purchaser = businessLogic.GetRandomClient();

                    while (seller.Stocks.Count < 1)
                    {
                        seller = businessLogic.GetRandomClient();
                    }

                    while (seller == purchaser)
                    {
                        purchaser = businessLogic.GetRandomClient();
                    }

                    var stock = businessLogic.GetRandomSellerStock(seller);


                    Console.WriteLine($"\nClient №{seller.Id} {seller.Name} {seller.Surname} is trying to sell a stock \"{stock.Type}\" to client №{purchaser.Id} {purchaser.Name} {purchaser.Surname}...");

                    var deal = businessLogic.MakeDeal(seller, purchaser, stock);
                    businessLogic.RegisterNewDeal(deal);

                    System.Threading.Thread.Sleep(10000);
                }
            }, isSilent: true);


            Console.WriteLine("\nDeals producing stopped. Clients table is updated:");
            businessLogic.ShowClients(clients.ToList());
        }
    }
}
