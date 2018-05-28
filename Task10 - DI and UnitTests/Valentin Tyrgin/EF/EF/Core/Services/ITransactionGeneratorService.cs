using System.Collections.Generic;
using System.Threading.Tasks;
using EF.Core.Abstractions;

namespace EF.Core.Services
{
    public interface ITransactionGeneratorService
    {
        Task Generate();
        T GetCollectionItem<T>(ICollection<T> entityCollection) where T : IEntity;
        int GetValue(object ob);
    }
}