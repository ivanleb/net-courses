namespace ORM.Core.Abstractions
{
    public interface ICollectionModification<in T>
    {
        int Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}