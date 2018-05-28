using EF.Core.Repositories;
using EF.Core.Services;

namespace EF.Core.Abstractions
{
    public interface IEntity : IEntityRepository, IEntityService
    {
    }
}