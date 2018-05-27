using System;
using System.Linq;
using EF.Core.Services;
using EF.Implementations.Entities;

namespace EF.Core.Abstractions
{
    public interface IDataContext : ICollectionModificationService, IDisposable
    {
        IQueryable<Trader> Traders { get; }
        IQueryable<Stock> Stocks { get; }
        IQueryable<TradableType> TradableTypes { get; }
        IQueryable<TradeOperation> TradeOperations { get; }
    }
}