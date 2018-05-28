using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface IRegistry
    {
        ILoggerService LoggerService { get; set; }
    }
}
