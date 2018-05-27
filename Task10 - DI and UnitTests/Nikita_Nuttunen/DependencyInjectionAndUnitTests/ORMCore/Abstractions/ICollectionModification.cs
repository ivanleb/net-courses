using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface ICollectionModification<T>
    {
        int Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
