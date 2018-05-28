using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.BL.Test
{
    public class RepositoryMock : IClientService
    {
        public void Init()
        {
            
        }

        public void Init(IRepositoryClient repositoryClient)
        {
            throw new NotImplementedException();
        }

        public Client GetUnicRandomClient(int attempsNumber)
        {
            return new Client()
            {
                Id = 0,
                FirstName = "Warren",
                SurName = "Buffet",
                TelephonNumber = "+7 777 77 77",
                Balance = 100_500,
                ClientStocksQuantity = 500,
                Stock = new Stock()
            };
        }

        public Transaction GetTransaction()
        {
            return new Transaction();
        }

        public void AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(Client client)
        {
            throw new NotImplementedException();
        }

        IQueryable<Client> IClientService.GetAllClients()
        {
            throw new NotImplementedException();
        }

        public void AddStock(Stock stock)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Client> GetBlackClients()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Client> GetOrangeClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return new List<Client>();
        }
    }
}
