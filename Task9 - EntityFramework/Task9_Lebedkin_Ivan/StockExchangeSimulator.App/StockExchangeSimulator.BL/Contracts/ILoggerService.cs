using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface ILoggerService
    {
        void Info(string message);
        void Error(Exception ex);
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);
        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);
    }
}
