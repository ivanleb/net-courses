using System.Linq;
using EntityCore.Model;

namespace EntityCore.Abstractions
{
    public interface IDataContext :
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
