using System;

namespace Exceptions_and_Logging.Abstractions
{
    interface ILoggerService
    {
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);

        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);

        void Info(string message);

        void Error(Exception ex);
    }
}
