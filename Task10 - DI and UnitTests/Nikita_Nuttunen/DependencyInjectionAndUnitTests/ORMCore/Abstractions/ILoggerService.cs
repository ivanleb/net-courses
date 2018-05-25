using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMCore.Abstractions
{
    public interface ILoggerService
    {
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = true);

        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = true);

        void Info(string message);

        void Error(Exception ex);
    }
}
