using System;
using System.Collections.Generic;
using System.Linq;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.BL.Domain
{
    public class ClientService : IClientService
    {
        private readonly IRepositoryClient _repositoryClient;
        private readonly IRepositoryStock _repositoryStock;
        private readonly Random _random;
        private readonly List<int> _previousClientsIds;
        private int _count;

        public ClientService(IRepositoryClient repositoryClient, IRepositoryStock repositoryStock)
        {
            _repositoryClient = repositoryClient;
            _repositoryStock = repositoryStock;
            _random = new Random();
            _previousClientsIds = new List<int>();
            _count = 0;
        }

        private bool IsUnicClient(int clientId)
        {
            if (_previousClientsIds.Count == _repositoryClient.Clients.ToList().Count)
            {
                throw new Exception("Can't generate unic client from DB");
            }
            return _previousClientsIds.Contains(clientId);
        }
        private void SetState(int clientId)
        {
            _previousClientsIds.Add(clientId);
            _count++;
        }

        private void ResetState()
        {
            _previousClientsIds.Clear();
            _count = 0;
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }
        public Client GetUnicRandomClient(int attempsNumber)
        {
            if (_count >= attempsNumber)
            {
                ResetState();
            }

            int rndIndex = 0;

            do
            {
                rndIndex = _repositoryClient.Clients.First().Id +
                           _random.Next(0, _repositoryClient.Clients.ToList().Count);
            } while (IsUnicClient(rndIndex));

            SetState(rndIndex);

            var choosedClients = from clients in _repositoryClient.Clients
                where clients.Id == rndIndex //&& clients.Stock != null
                select clients;

            var stId = (from choosCl in choosedClients
                select choosCl.Stock.Id).First();

            var selectedStocks = from stocks in _repositoryStock.Stocks
                where stocks.Id == stId
                select stocks;

            var selectedStock = selectedStocks.First();

            var selectedClient = choosedClients.First();
            selectedClient.Stock = selectedStock;

            return selectedClient;
        }

        public void AddClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveClient(Client client)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Client> GetAllClients()
        {
            return _repositoryClient.Clients;
        }

        public IQueryable<Client> GetBlackClients()
        {
            return _repositoryClient.Clients.Where(c => c.Balance < 0);
        }

        public IQueryable<Client> GetOrangeClients()
        {
            return _repositoryClient.Clients.Where(c => c.Balance == 0);
        }
    }
}