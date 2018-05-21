using EF.Core.Repositories;

namespace EF.Core.Services
{
    public interface ICollectionModification
    {
        int Add<TV>(TV entity) where TV : class, IId;
        void Remove<TV>(TV entity) where TV : class;
        void SaveChanges();
    }
}