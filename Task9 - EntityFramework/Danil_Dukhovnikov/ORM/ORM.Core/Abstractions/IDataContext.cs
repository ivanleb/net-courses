using System.Linq;
using ORM.Core.Model;

namespace ORM.Core.Abstractions
{
    public interface IDataContext :
        ICollectionModification<Client>,
        ICollectionModification<StockType>,
        ICollectionModification<Stock>,
        ICollectionModification<Deal>
    {
        IQueryable<Client> Clients { get; }

        IQueryable<Stock> Stocks { get; }

        IQueryable<Deal> Deals { get; }

        IQueryable<StockType> StockTypes { get; }

        void SaveChanges();
    }
}
