using System.Collections.Generic;
using System.Linq;
using ORMCore.Model;

namespace ORMCore
{
    public interface IBuisnessService
    {
        void AddNewClient(string name, string surname, string phoneNumber, decimal balance);
        void AddNewStockType(string newTypeName, decimal price);
        void AddStockToClient(string stockType, Client client);
        IQueryable<Client> GetAllClients();
        IQueryable<Client> GetClientsInBlackZone();
        IQueryable<Client> GetClientsInOrangeZone();
        IQueryable<Deal> GetDeals();
        IQueryable<ICollection<Stock>> GetSecondClientStocks();
        IQueryable<StockType> GetStockTypes();
        void NewDeal(Client buyer, Client seller, decimal sum, Stock stock);
        void NewDeal(Deal deal);
    }
}