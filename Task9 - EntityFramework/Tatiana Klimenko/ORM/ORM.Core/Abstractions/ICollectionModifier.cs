namespace ORM.Core.Abstractions
{
    public interface ICollectionModifier<T>
    {
        int Add(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}
