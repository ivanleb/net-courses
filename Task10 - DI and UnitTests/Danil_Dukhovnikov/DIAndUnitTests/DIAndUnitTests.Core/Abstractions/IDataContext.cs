using System;
using System.Linq;
using DIAndUnitTests.Core.Models;

namespace DIAndUnitTests.Core.Abstractions
{
    public interface IDataContext : 
        ICollectionsModification<Deal>,
        ICollectionsModification<Share>,
        ICollectionsModification<Trader>, IDisposable
    {
        IQueryable<Deal> Deals { get; }
        IQueryable<Share>Shares { get; }
        IQueryable<Trader> Traders { get; }

        void SaveChanges();
    }
}