using EF.Core.Repositories;
using EF.Core.Services;
using EF.Implementations.Entities;

namespace EF.Core.Abstractions
{
    public interface IBusiness : IBusinessRepository, IBusinessService
    {
        void UpdateEntity<T>(T entity) where T : class, IEntity;
        void Withdraw(Trader trader, Stock stock);
        void Acquire(Trader trader, Stock stock);
    }
}