using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.Abstractions;

namespace StockExchangeSimulator.Core.DataModel
{
    public interface IDataContext :
    ICollectionsModification<Client>,
    ICollectionsModification<Stock>,
    ICollectionsModification<Transaction>
    {
        IQueryable<Client> Clients { get; }

        IQueryable<Stock> Stocks { get; }

        IQueryable<Transaction> Transactions { get; }

        void SaveChanges();
    }
    
}
