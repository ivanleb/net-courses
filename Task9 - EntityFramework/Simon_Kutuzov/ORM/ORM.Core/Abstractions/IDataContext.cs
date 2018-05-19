using ORM.Core.Model;
using System.Linq;

namespace ORM.Core.Abstractions
{
    public interface IDataContext
    {
        IQueryable<Trader> Traiders { get; }
        IQueryable<Listing> Listings { get; }
        IQueryable<Share> Shares { get; }
        IQueryable<Deal> Deals { get; }

        void Add(Trader trader);
        void Add(Listing listing);
        void Add(Share share);
        void Add(Deal deal);
        void SaveChanges();
    }
}
