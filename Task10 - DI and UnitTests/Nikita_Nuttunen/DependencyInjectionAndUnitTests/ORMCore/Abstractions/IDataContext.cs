using ORMCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IDataContext : 
        ICollectionModification<Client>, 
        ICollectionModification<Deal>, 
        ICollectionModification<Stock>
    {
        IQueryable<Client> Clients { get; }

        IQueryable<Stock> Stocks { get; }

        IQueryable<Deal> Deals { get; }

        void SaveChanges();
    }
}
