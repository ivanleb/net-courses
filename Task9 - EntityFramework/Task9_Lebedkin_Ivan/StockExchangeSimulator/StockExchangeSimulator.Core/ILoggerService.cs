using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core.DataModel;

namespace StockExchangeSimulator.Core
{
    public interface ILoggerService
    {
        void SendTransactionToLog(Transaction transaction);
        //
        void Info(string message);
        void Error(Exception ex);
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false); 
        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);
    }
}
