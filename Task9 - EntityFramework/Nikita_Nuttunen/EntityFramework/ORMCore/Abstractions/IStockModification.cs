using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IStockModification<in T> where T : IStock
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
