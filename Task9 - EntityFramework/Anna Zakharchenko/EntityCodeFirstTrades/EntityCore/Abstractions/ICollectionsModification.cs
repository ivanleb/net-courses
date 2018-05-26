using EntityCore.Model;
namespace EntityCore.Abstractions
{
    public interface ICollectionsModification<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
