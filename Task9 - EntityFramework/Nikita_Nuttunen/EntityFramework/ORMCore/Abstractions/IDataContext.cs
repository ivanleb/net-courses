using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IDataContext : IClientsModification<IClient>, IStockModification<IStock>, IDealMofication<IDeal> 
    {
        IQueryable<IClient> Clients { get; }

        IQueryable<IStock> Stocks { get; }

        IQueryable<IDeal> Deals { get; }

        void SaveChanges();
    }
}
