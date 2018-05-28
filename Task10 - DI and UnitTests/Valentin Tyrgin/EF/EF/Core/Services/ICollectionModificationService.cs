using EF.Core.Abstractions;

namespace EF.Core.Services
{
    public interface ICollectionModificationService
    {
        int Add<T>(T entity) where T : class, IEntity;
        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class, IEntity;
        void SaveChanges();
    }
}