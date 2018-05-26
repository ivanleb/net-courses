using System.Linq;
using ORM.Core.Entities;

namespace ORM.Core.Abstractions
{
    public interface IDataContext : 
        ICollectionModifier<Client>, 
        ICollectionModifier<Stock>,
        ICollectionModifier<Deal>
    {
        IQueryable<Client> Clients { get; }

        IQueryable<Stock> Stocks { get; }

        IQueryable<Deal> Deals { get; }

        void SaveChanges();
    }
}
