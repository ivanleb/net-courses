using System;
using System.Collections.Generic;
using System.Linq;
using ORM.Core.Abstractions;
using ORM.Core.Model;

namespace ORM.Core
{
    public class BussinesService
    {
        private readonly IDataContext _dataContext;

        public BussinesService(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        private static string GetZone(decimal balance)
        {
            if (balance == 0) return "Orange";
            
            return balance < 0 ? "Black" : "White";
        }

        public void RegisterNewClient(string firstName, string surname, string phoneNumber, decimal balance)
        {
            var newClient = new Client()
            {
                Name = firstName,
                Surname = surname,
                Balance = balance,
                PhoneNumber = phoneNumber
            };
            this._dataContext.Add(newClient);
            this._dataContext.SaveChanges();
        }

        public void RegisterNewStockToClient(string stockType, Client client, bool canBeSold = true)
        {
            var stockTypeItem = _dataContext.StockTypes.Single(_ => _.Name == stockType);
            var newStock = new Stock()
            {
                Type = stockTypeItem,
                IsForSale = canBeSold,
            };
            client.Stocks.Add(newStock);
            this._dataContext.SaveChanges();
        }

        public void RegisterNewDeal(Client seller, Client buyer, Stock stock, decimal cost)
        {
            if (!stock.IsForSale)
            {
                throw new ArgumentException("The stock is not allowed to be selled.", nameof(stock));
            }
            
            seller.Stocks.Remove(stock);
            buyer.Stocks.Add(stock);
            seller.Balance += cost;
            seller.Zone = GetZone(seller.Balance);
            buyer.Balance -= cost;
            buyer.Zone = GetZone(buyer.Balance);
            var newDeal = new Deal() { Buyer = buyer, Seller = seller, Cost = cost, Stock = stock };
            this._dataContext.Add(newDeal);
            this._dataContext.SaveChanges();
        }

        public void RegisterNewStockType (string name, decimal cost)
        {
            var newStockType = this._dataContext.StockTypes.FirstOrDefault(st => st.Name == name);
            if (newStockType != null) return;
            newStockType = new StockType() { Name = name, Cost = cost };
            this._dataContext.Add(newStockType);
            this._dataContext.SaveChanges();
        }

        public IEnumerable<Client> GetAllStockOwners()
        {
            return this._dataContext.Clients.Where(client => client.Stocks.Any());
        }

        public IQueryable<Client> GetAllClients()
        {
            return this._dataContext.Clients;
        }

        public IQueryable<Client> GetClientsInZone(string zone)
        {
            return this._dataContext.Clients.Where(client =>
                string.Equals(client.Zone, zone, StringComparison.CurrentCultureIgnoreCase));
        }

        public void ChangeStockCost(Stock stock, decimal newCost)
        {
            stock.Type.Cost = newCost;
            this._dataContext.SaveChanges();
        }
        public void ChangeStockCost(StockType stockType, decimal newCost)
        {
            stockType.Cost = newCost;
            this._dataContext.SaveChanges();
        }
        public void ChangeStockCost(string stockTypeName, decimal newCost)
        {
            var stockType = this._dataContext.StockTypes.FirstOrDefault(st => st.Name == stockTypeName);
            
            if (stockType == null)
                throw new ArgumentException($"Stocktype {stockTypeName} does not exist.", nameof(stockTypeName));
            stockType.Cost = newCost;
            
            this._dataContext.SaveChanges();
        }

        public decimal GetStockCost(string stockTypeName)
        {
            var stockType = this._dataContext.StockTypes.FirstOrDefault(st => st.Name == stockTypeName);

            if (stockType == null)
                throw new ArgumentException($"Stocktype {stockTypeName} does not exist.", nameof(stockTypeName));
            
            return stockType.Cost;
        }
    }
}
