using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface IBusinessService
    {
        IClient GetClientById(int id);

        IQueryable<IClient> GetClientsFromOrangeArea();

        void RegisterNewClient(IClient client);

        void RegisterNewStock(IStock stock);

        void ChangeStockType(IStock stock, string newType);

        void MakeDeal(IClient seller, IClient purchaser, IStock stock);
    }
}
