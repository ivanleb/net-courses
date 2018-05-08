using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.DataModel
{
    public interface IInitialClientsDataModel
    {
        IQueryable<Stock> Stocks { get; }
        IQueryable<Client> Clients { get; }
    }

    public static class DataRetriever
    {
        public static IQueryable<Client> GetClients(this IQueryable<Client> clients)
        {
            return clients;
        }
    }
}
