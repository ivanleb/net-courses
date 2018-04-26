using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IClientsModification<in T> where T : IClient
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
