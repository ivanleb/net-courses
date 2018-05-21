using SESimulator.Abstractions;
using SESimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SESimulator.Core
{
    public enum StockExchangeClientZone
    {
        White, Orange, Black
    }

    public class BussinesService
    {
        private readonly IDataContext dataContext;

        public BussinesService(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void RegisterNewClient(string firstName, string lastName, string phoneNumber, decimal balance)
        {
            var newClient = new Client()
            {
                Name = firstName,
                Surname = lastName,
                Balance = balance,
                PhoneNumber = phoneNumber,
                Stocks = new HashSet<Stock>()
            };
            this.dataContext.Add(newClient);
            this.dataContext.SaveChanges();
        }

        public void RegisterNewStockToClient(string type, Client client)
        {
            var stockType = dataContext.StockTypes.FirstOrDefault(st => st.Name == type);
            if (stockType == null)
            {
                throw new ArgumentException("The specified type does not exist. The stock can not be registered.", "type");
            }
            var newStock = new Stock()
            {
                Type = stockType
            };
            //this.dataContext.Add(newStock);
            client.Stocks.Add(newStock);
            this.dataContext.SaveChanges();
        }

        public void RegisterNewDeal(Client seller, Client buyer, Stock stock, decimal cost)
        {
            if (!seller.StocksForSale.Contains(stock))
            {
                throw new ArgumentException("The stock is not allowed to be selled.", "stock");
            }
            seller.Stocks.Remove(stock);
            buyer.Stocks.Add(stock);
            seller.Balance += cost;
            buyer.Balance -= cost;
            var newDeal = new Deal() { Buyer = buyer, Seller = seller, Cost = cost, Stock = stock };
            this.dataContext.Add(newDeal);
            this.dataContext.SaveChanges();
        }

        public void RegisterNewStockType (string name, decimal cost)
        {
            var newStockType = this.dataContext.StockTypes.FirstOrDefault(st => st.Name == name);
            if (newStockType != null) return;
            newStockType = new StockType() { Name = name, Cost = cost };
            this.dataContext.Add(newStockType);
            this.dataContext.SaveChanges();
        }

        public IQueryable<Client> GetAllStockOwners()
        {
            return this.dataContext.Clients.Where(cl => cl.Stocks.Count() > 0);
        }

        public IQueryable<Client> GetAllClients()
        {
            return this.dataContext.Clients;
        }

        public IQueryable<Client> GetClientsInZone(StockExchangeClientZone zone)
        {
            return this.dataContext.Clients.Where(client => client.Zone == zone);
        }

        public void ChangeStockCost(Stock stock, decimal newCost)
        {
            stock.Type.Cost = newCost;
            this.dataContext.SaveChanges();
        }
        public void ChangeStockCost(StockType stockType, decimal newCost)
        {
            stockType.Cost = newCost;
            this.dataContext.SaveChanges();
        }
        public void ChangeStockCost(string stockTypeName, decimal newCost)
        {
            var stockType = this.dataContext.StockTypes.FirstOrDefault(st => st.Name == stockTypeName);
            if (stockType == null) throw new ArgumentException($"Stocktype {stockTypeName} does not exist.", "stockTypeName");
            stockType.Cost = newCost;
            this.dataContext.SaveChanges();
        }

        public decimal GetStockCost(string stockTypeName)
        {
            var stockType = this.dataContext.StockTypes.FirstOrDefault(st => st.Name == stockTypeName);
            if (stockType == null) throw new ArgumentException($"Stocktype {stockTypeName} does not exist.", "stockTypeName");
            return stockType.Cost;
        }
    }
}
