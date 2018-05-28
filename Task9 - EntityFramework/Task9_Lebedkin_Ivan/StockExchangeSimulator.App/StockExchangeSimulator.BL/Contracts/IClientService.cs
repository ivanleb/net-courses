using System.Linq;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface IClientService
    {
        void Init();
        Client GetUnicRandomClient(int attempsNumber);
        void AddClient(Client client);
        void RemoveClient(Client client);
        IQueryable<Client> GetAllClients();
        IQueryable<Client> GetBlackClients();
        IQueryable<Client> GetOrangeClients();
    }
}