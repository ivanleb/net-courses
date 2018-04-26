using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public delegate void BalanceHandler(IClient client);
    public interface IDealProducer
    {
        event BalanceHandler BalanceChanged;

        void Run();

        IDeal MakeDeal(IClient seller, IClient purchaser, IStock stock);

        void OnBalanceChanged(IClient client);
    }
}
