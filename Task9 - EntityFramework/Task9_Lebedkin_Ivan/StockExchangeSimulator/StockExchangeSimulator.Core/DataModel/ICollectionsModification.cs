using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeSimulator.Core.Abstractions
{
    public interface ICollectionsModification<in T> //where T : IClient
    {
        int Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
