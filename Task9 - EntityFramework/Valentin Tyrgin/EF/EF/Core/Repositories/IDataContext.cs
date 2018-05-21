using System.Linq;
using EF.Core.Services;
using EF.Implementations;

namespace EF.Core.Repositories
{
    public interface IDataContext : ICollectionsEntityUpdate<Traider>,
        ICollectionsEntityUpdate<Stock>,
        ICollectionsEntityUpdate<StockType>,
        ICollectionsEntityUpdate<Operation>,
        ICollectionModification
    {
        IQueryable<Traider> Traiders { get; }
        IQueryable<Stock> Stocks { get; }
        IQueryable<StockType> StockTypes { get; }
        IQueryable<Operation> Operations { get; }
    }
}