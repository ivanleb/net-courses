using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.DataModel;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Implementations
{
    public static class ExtentionDataRetriever
    {
        public static IQueryable<Client> GetOrangeZoneClients(this IQueryable<Client> clients)
        {
            return clients.Where(c => c.Balance == 0);
        }

        public static IQueryable<Client> GetBlackZoneClients(this IQueryable<Client> clients)
        {
            return clients.Where(c => c.Balance < 0);
        }
    }
}
