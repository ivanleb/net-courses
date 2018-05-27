namespace DIAndUnitTests.Core.Abstractions
{
    public interface ICollectionsModification<in T>
    {
        void Add(T entity);
    }
}