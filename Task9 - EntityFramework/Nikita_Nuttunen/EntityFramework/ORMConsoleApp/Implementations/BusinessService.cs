using ORMCore.Abstractions;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMConsoleApp.Implementations
{
    public class BusinessService // : IBusinessService
    {
        private readonly StockExchangeContext dataContext;
        private readonly LoggerService loggerService;

        private Random rnd = new Random();

        public BusinessService(StockExchangeContext dataContext, LoggerService loggerService)
        {
            this.dataContext = dataContext;
            this.loggerService = loggerService;
        }
                
        public void ChangeStockType(Stock stock, string newType)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                var oldType = stock.Type;
                stock.Type = newType;
                dataContext.SaveChanges();
                loggerService.Info($"Stock №{stock.Id} changed type from \"{oldType}\" to \"{newType}\"");
            }, isSilent: true);
            
        }

        public Client GetClientById(int id)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return dataContext.Clients.First(c => c.Id == id);
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
        
        public IQueryable<Client> GetClientsFromOrangeArea()
        {
            return dataContext.Clients.Where(c => c.Area == "orange");
        }
              
        public void RegisterNewClient(Client client)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Clients.Add(client);
                dataContext.SaveChanges();
                loggerService.Info($"{client.Name} {client.Surname} was registered");
            }, isSilent: true);
            
        }

        public void RegisterNewStock(Stock stock)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Stocks.Add(stock);
                dataContext.SaveChanges();
                loggerService.Info($"\"{stock.Type}\" stock was registered");
            }, isSilent: true);
        }

        public void RegisterNewDeal(Deal deal)
        {
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Deals.Add(deal);
                dataContext.SaveChanges();
            }, isSilent: true);
        }

        public void ShowClient(Client client)
        {
            var clientsStocks = new List<string>();
            if (client.Stocks != null)
            {
                foreach (var stock in client.Stocks)
                {
                    clientsStocks.Add(stock.Type);
                }
            }

            if (client.Area == "black") Console.ForegroundColor = ConsoleColor.Red;
            if (client.Area == "orange") Console.ForegroundColor = ConsoleColor.Yellow;
            if (client.Area == "green") Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{client.Id} {client.Name} {client.Surname} {client.PhoneNumber} {client.Balance}$ {client.Area} {string.Join(", ", clientsStocks)}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public Stock GetRandomSellerStock(Client seller)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {                
                if (seller.Stocks.Count == 0) throw new ArgumentNullException($"{seller.Name} {seller.Surname} doesn't have stocks");
                if (seller.Stocks.Count == 1) return seller.Stocks.First();
                Random rnd = new Random();
                var stockIndex = rnd.Next(0, seller.Stocks.Count - 1);
                return seller.Stocks.ElementAt(stockIndex);
            }, isSilent: true);            
        }  
        
        public List<Client> GetClientsList()
        {
            return dataContext.Clients.Include(c => c.Stocks).ToList();
        }

    }
}
