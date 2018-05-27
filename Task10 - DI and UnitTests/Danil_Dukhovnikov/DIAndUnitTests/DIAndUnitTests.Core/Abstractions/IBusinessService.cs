using System.Collections.Generic;
using System.Linq;
using DIAndUnitTests.Core.Models;

namespace DIAndUnitTests.Core.Abstractions
{
    public interface IBusinessService
    {
        void AddTrader(string name, string surname, decimal balane,  string phoneNumber);
        void AddShare(Trader owner, string name, decimal price);
        void RegisterDeal(Trader byer, Trader seller, Share share);
        void AddDeal(Trader buyer, Trader seller, Share share);
        IQueryable<Trader> GetOrangeTraders();
        IEnumerable<Trader> GetAllTraders();
    }
}