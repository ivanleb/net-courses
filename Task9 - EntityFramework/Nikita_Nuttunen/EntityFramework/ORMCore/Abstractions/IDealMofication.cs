using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IDealMofication<in T> where T : IDeal
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
