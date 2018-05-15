using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMCore.Model
{
    public interface ICollectionModification<T>
    {
        int Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
