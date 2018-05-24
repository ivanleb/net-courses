using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMCore.Model;

namespace ORMCore.Repositories
{
    public interface IModelRepository :
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
