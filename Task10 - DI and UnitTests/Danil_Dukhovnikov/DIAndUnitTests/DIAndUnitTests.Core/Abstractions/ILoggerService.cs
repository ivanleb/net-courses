using System;

namespace DIAndUnitTests.Core.Abstractions
{
    public interface ILoggerService
    {
        void RunWithExeptionLogging(Action actionToRun, bool isSilent = false);
        T RunWithExeptionLogging<T>(Func<T> functionToRun, bool isSilent = false);
        void Info(string message);
        void Error(Exception ex);

    }
}