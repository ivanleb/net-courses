namespace EF.Core.Services
{
    public interface ICollectionsEntityUpdate<in T>
    {
        void Update(T entity);
    }
}