﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMCore.Model;
using ORMCore.Abstractions;
using ORMCore.Repositories;

namespace ORMCore
{
    public class BuisnessService : IBuisnessService
    {
        private IModelRepository dataContext;

        public BuisnessService(IModelRepository dataContext)
        {
            this.dataContext = dataContext;
        }

        public void AddNewClient(string name, string surname, string phoneNumber, decimal balance)
        {
            var client = new Client()
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phoneNumber,
                Balance = balance,
                ClientStocks = new List<Stock>()
            };
            dataContext.Add(client);
            dataContext.SaveChanges();
        }

        public void AddStockToClient(string stockType, Client client)
        {
            var type = dataContext.StockTypes.FirstOrDefault(t => t.Name == stockType);
            if (stockType == null) throw new ArgumentException("Type does not exists");
            var newStock = new Stock()
            {
                Type = type
            };
            client.ClientStocks.Add(newStock);
            dataContext.SaveChanges();
        }

        public void AddNewStockType(string newTypeName, decimal price)
        {
            if (dataContext.StockTypes.FirstOrDefault(t => t.Name == newTypeName) != null) return;
            var newType = new StockType()
            {
                Cost = price,
                Name = newTypeName
            };
            dataContext.Add(newType);
            dataContext.SaveChanges();
        }

        public void NewDeal(Deal deal)
        {
            if (!deal.Seller.ClientStocksForSale.Contains(deal.Stock)) throw new ArgumentException("Stock cant be selled!");
            deal.Seller.ClientStocks.Remove(deal.Stock);
            deal.Seller.Balance += deal.Stock.Type.Cost;
            deal.Buyer.ClientStocks.Add(deal.Stock);
            deal.Buyer.Balance -= deal.Stock.Type.Cost;
            if (deal.Buyer.Balance == 0) deal.Buyer.ClientZone = Zone.Orange;
            if (deal.Buyer.Balance < 0) deal.Buyer.ClientZone = Zone.Black;
            dataContext.Add(deal);
            dataContext.SaveChanges();
        }

        public IQueryable<Client> GetAllClients()
        {
            return dataContext.Clients;
        }

        public IQueryable<Client> GetClientsInOrangeZone()
        {
            return dataContext.Clients.Where(c => c.ClientZone == Zone.Orange);
        }

        public IQueryable<Client> GetClientsInBlackZone()
        {
            return dataContext.Clients.Where(c => c.ClientZone == Zone.Black);
        }

        public IQueryable<StockType> GetStockTypes()
        {
            return dataContext.StockTypes;
        }

        public ICollection<Stock> GetClientStocksById(int id)
        {
            return dataContext.Clients.Where(s=>s.Id == id).Select(s => s.ClientStocks).FirstOrDefault();
        }

        public IQueryable<Deal> GetDeals()
        {
            return dataContext.Deals;
        }

    }
}
