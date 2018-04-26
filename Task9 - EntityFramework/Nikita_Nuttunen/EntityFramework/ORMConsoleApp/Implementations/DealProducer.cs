using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMConsoleApp.Implementations
{
    public class DealProducer
    {
        private delegate void BalanceHandler(Client client);
        private event BalanceHandler BalanceChanged;

        private readonly LoggerService loggerService;
        private readonly BusinessService businessService;

        public bool IsContinue;

        public DealProducer(BusinessService businessService, LoggerService loggerService)
        {
            this.businessService = businessService;
            this.loggerService = loggerService;
        }

        public void Run()
        {
            Random rnd = new Random();
            IsContinue = true;
            loggerService.RunWithExceptionLogging(() =>
            {
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
                        var producedDeal = MakeDeal(seller, purchaser, stock);
                        businessService.RegisterNewDeal(producedDeal);
                        loggerService.Info($"{seller.Name} {seller.Surname} selled stock \"{stock.Type}\"" +
                            $" №{stock.Id} to {purchaser.Name} {purchaser.Surname} for {stock.Cost}");
                    }
                    System.Threading.Thread.Sleep(10000);
                }
            }, isSilent: true);
        }

        private Deal MakeDeal(Client seller, Client purchaser, Stock stock)
        {
            BalanceChanged = OnBalanceChanged;

            purchaser.Stocks.Add(stock);
            purchaser.Balance -= stock.Cost;

            seller.Stocks.Remove(stock);
            seller.Balance += stock.Cost;

            BalanceChanged(purchaser);
            BalanceChanged(seller);

            return new Deal() { Seller = seller, Purchaser = purchaser, SelledStock = stock, Cost = stock.Cost };
        }

        public void OnBalanceChanged(Client client)
        {
            if (client.Balance == 0) client.Area = "orange";
            if (client.Balance < 0) client.Area = "black";
            if (client.Balance > 0) client.Area = "green";
        }
    }
}
