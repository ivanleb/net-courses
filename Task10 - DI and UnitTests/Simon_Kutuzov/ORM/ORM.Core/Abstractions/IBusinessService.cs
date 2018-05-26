using System.Linq;
using ORM.Core.Model;

namespace ORM.Core.Abstractions
{
    public interface IBusinessService
    {
        IQueryable<Trader> Traders { get; }
        IQueryable<Trader> GetTraidersInOrangeZone();
        void AddTrader(string firstName, string secondName, string phoneNumber, decimal balance);
        void AddListing(string name, decimal price);
        void AddShare(Listing listing, Trader owner);
        void MakeDeal(Trader buyer, Trader seller, Share share);
    }
}
