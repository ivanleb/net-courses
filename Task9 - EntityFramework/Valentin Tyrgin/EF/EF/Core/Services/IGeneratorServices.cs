using System.Collections.Generic;
using EF.Core.Repositories;

namespace EF.Core.Services
{
    public interface IGeneratorServices
    {
        void Generate();
        T GetCollectionItem<T>(ICollection<T> entityCollection) where T : IId;
        int GetValue(object ob);
    }
}