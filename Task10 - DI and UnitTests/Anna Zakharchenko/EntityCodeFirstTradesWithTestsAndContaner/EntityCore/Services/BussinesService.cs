using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Abstractions;
using EntityCore.Model;

namespace EntityCore
{
    public class BussinesService
    {
        private readonly IDataContextRepository dataContext;
        private readonly ILoggerService loggerService;
        public BussinesService(IDataContextRepository dataContext, ILoggerService loggerService)
        {
            this.dataContext = dataContext;
            this.loggerService = loggerService;
        }

        public Client GetClient(int id)
        {
            return dataContext.Clients.First(c => c.Id == id);
        }

        public Stock GetSellerStock(Client seller)
        {
            if (seller.Stocks.Count == 0)
            {
                loggerService.Info($"{seller.FirstName} {seller.LastName} doesn't have stocks for sale! " +
                    $"The deal fell through.");
                return null;
            }
            Random rnd = new Random();
            return seller.Stocks.ElementAt(rnd.Next(seller.Stocks.Count - 1));
        }

        public IQueryable<Client> GetClientsFromOrangeZone()
        {
            return dataContext.Clients.Where(c => c.Zone == ClientZoneOfBalance.Orange);
        }

        public void RegisterNewClient(string firstName, string lastName, string phoneNamber, decimal balance)
        {
            Client client = new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNamber,
                Balance = balance,
                Stocks = new List<Stock>()
            };
            dataContext.Add(client);
            dataContext.SaveChanges();
        }

        public void RegisterNewStock(Client client, KeyValuePair<string,decimal> typeStock)
        {
            Stock stock = new Stock()
            {
                TypeOfStock = typeStock,
                Cost = typeStock.Value,
                NameTypeOfStock = typeStock.Key
            };
            client.Stocks.Add(stock);
            dataContext.SaveChanges();
        }

        private ClientZoneOfBalance ChangeClientsZone(Client client)
        {
            if (client.Balance == 0)
                return ClientZoneOfBalance.Orange;

            if (client.Balance < 0)
                return ClientZoneOfBalance.Black;

            return ClientZoneOfBalance.Green;
        }

        public Trade GetNewTrade(Client seller, Client buyer, Stock stock)
        {
            seller.Balance += stock.Cost;
            seller.Zone = ChangeClientsZone(seller);
            buyer.Balance -= stock.Cost;
            buyer.Zone=ChangeClientsZone(buyer);
            seller.Stocks.Remove(stock);
            buyer.Stocks.Add(stock);

            return new Trade()
            {
              Seller = seller,
              Buyer = buyer,
              StockFromSeller = stock
            };
        }

        public void RegisterNewTrade(Trade trade)
        {                     
            loggerService.RunWithExceptionLogging(() =>
            {
                dataContext.Add(trade);
                dataContext.Update(trade.Seller);
                dataContext.Update(trade.Buyer);
                dataContext.SaveChanges();
            }, isSilent: true);

            loggerService.Info($"Changed balance: {trade.Seller}\n{trade.Buyer}");
            if (trade.Buyer.Zone != ClientZoneOfBalance.Green)
                loggerService.Info($"Zone from {trade.Buyer} was changed, now it is {trade.Buyer.Zone} zone.");

            loggerService.Info($"Trade was made: {trade}");
        }

    //public void NewBalanceForSeller(Trade trade)
    //    {
    //        loggerService.Info($"Changed balance: {trade.Seller}\n{trade.Buyer}");
    //        if(trade.Buyer.Zone != ClientZoneOfBalance.Green)
    //            loggerService.Info($"Zone from {trade.Buyer} was changed, now it is {trade.Buyer.Zone} zone.");

    //    }
    //    public void NewTradeMade(Trade trade)
    //    {
    //        loggerService.Info($"Trade was made: {trade}");
    //    }

    }
}
