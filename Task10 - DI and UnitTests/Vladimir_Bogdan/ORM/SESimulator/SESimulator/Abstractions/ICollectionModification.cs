namespace SESimulator.Abstractions
{
    public interface ICollectionModification<T>
    {
        int Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}