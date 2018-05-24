using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Model
{
    public interface IDataContext: 
        ICollectionModification<Client>, 
        ICollectionModification<Stock>, 
        ICollectionModification<StockType>, 
        ICollectionModification<Deal>
    {
        IQueryable<Client> Clients { get; }

        IQueryable<Stock> Stocks { get; }

        IQueryable<StockType> StockTypes { get; }

        IQueryable<Deal> Deals { get; }

        void SaveChanges();
    }
}
