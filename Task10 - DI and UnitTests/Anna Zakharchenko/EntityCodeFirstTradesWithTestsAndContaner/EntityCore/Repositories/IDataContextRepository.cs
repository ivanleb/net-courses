using System.Linq;
using EntityCore.Model;
using EntityCore.Abstractions;

namespace EntityCore
{
    public interface IDataContextRepository :
        ICollectionsModification<Client>,
        ICollectionsModification<Stock>,
        ICollectionsModification<Trade>
    {
        IQueryable<Client> Clients { get; }
        IQueryable<Stock> Stocks { get; }
        IQueryable<Trade> Trades { get; }

        void SaveChanges();
    }
}
