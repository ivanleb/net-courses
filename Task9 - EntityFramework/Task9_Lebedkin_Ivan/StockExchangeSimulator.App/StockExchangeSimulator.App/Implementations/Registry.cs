using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;

namespace StockExchangeSimulator.App.Implementations
{
    public class Registry : IRegistry
    {
        public Registry(ILoggerService loggerService)
        {
            LoggerService = loggerService;
        }

        public ILoggerService LoggerService { get; set; }
    }
}
