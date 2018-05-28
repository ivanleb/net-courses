using System.Linq;
using EF.Core.Abstractions;
using EF.Implementations.Entities;

namespace EF.Core.Services
{
    public interface IBusinessService
    {
        void RegisterEntity<T>(T entity) where T : class, IEntity;
        TradeOperation ProcessTrade(
            Trader seller,
            Trader buyer,
            Stock tradable);

        IQueryable<Trader> GetAllTraiders();
        IQueryable<Trader> GetOrangeZoneTraiders();
        IQueryable<Trader> GetBlackZoneTraiders();
        IQueryable<TradeOperation> GetAllOperations();
    }
}