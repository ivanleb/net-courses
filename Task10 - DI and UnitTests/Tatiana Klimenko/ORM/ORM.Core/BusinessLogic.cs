using System;
using System.Collections.Generic;
using System.Linq;
using ORM.Core.Entities;
using ORM.Core.Abstractions;

namespace ORM.Core
{
    public class BusinessLogic
    {
        private IDataContext dataContext;
        private ILoggerService loggerService;


        public BusinessLogic(IDataContext dataContext, ILoggerService loggerService)
        {
            this.dataContext = dataContext;
            this.loggerService = loggerService;
        }


        public void RegisterNewClient(Client client)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(client);
                dataContext.SaveChanges();
                loggerService.Info($"{client.Name} {client.Surname} client was registered");
            }, isSilent: true);

        }

        public void RegisterNewStock(Stock stock)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(stock);
                dataContext.SaveChanges();
                loggerService.Info($"\"{stock.Type}\" stock was registered");
            }, isSilent: true);
        }

        public void RegisterNewDeal(Deal deal)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(deal);
                dataContext.SaveChanges();
                loggerService.Info($"{deal.Seller.Name} {deal.Seller.Surname} selled stock №{deal.SelledStock.Id} \"{deal.SelledStock.Type}\" to {deal.Purchaser.Name} {deal.Purchaser.Surname} for {deal.SelledStock.Cost}");
            }, isSilent: true);
        }


        public Client GetRandomClient()
        {
            return dataContext.Clients.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
        }

        public int GetClientsAmount()
        {
            return dataContext.Clients.Count();
        }

        public IQueryable<Client> GetClients()
        {
            return dataContext.Clients;
        }

        public Deal MakeDeal(Client seller, Client purchaser, Stock stock)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                purchaser.Stocks.Add(stock);
                purchaser.Balance -= stock.Cost;

                seller.Stocks.Remove(stock);
                seller.Balance += stock.Cost;

                return new Deal() { Seller = seller, Purchaser = purchaser, SelledStock = stock, Cost = stock.Cost };
            }, isSilent: true);
        }

        public Stock GetRandomSellerStock(Client seller)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                if (seller.Stocks.Count == 1) return seller.Stocks.First();
                Random rnd = new Random();
                var stockIndex = rnd.Next(0, seller.Stocks.Count - 1);
                return seller.Stocks.ElementAt(stockIndex);
            }, isSilent: true);
        }

        public void ShowClients(List<Client> clients)
        {
            Console.WriteLine($"{"Id",3} {"Name",10} {"Surname",12} {"PhoneNumber",16} {"Balance",9} {"Zone",6} {"Stocks",12}");

            foreach (var client in clients)
            {
                if (client.Zone == Zone.Black) Console.ForegroundColor = ConsoleColor.Red;
                if (client.Zone == Zone.Orange) Console.ForegroundColor = ConsoleColor.Yellow;
                if (client.Zone == Zone.Green) Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(client.ToString());

                if (client.Stocks != null)
                {
                    foreach (var stock in client.Stocks)
                    {
                        Console.WriteLine($"{stock.Type,76}");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public List<Client> GetClientsFromOrangeZone()
        {
            List<Client> orangeClients = new List<Client>();

            foreach (var client in dataContext.Clients)
            {
                if (client.Zone == Zone.Orange)
                {
                    orangeClients.Add(client);
                }
            }
            return orangeClients;
        }
    }
}

