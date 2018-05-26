using System;
using ExceptionsAndLogging.Abstractions;
using log4net;

namespace ExceptionsAndLogging.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog _logger;

        public LoggerService(ILog logger)
        {
            this._logger = logger;
        }

        public void Error(Exception ex)
        {
            this._logger.Error(ex);
        }

        public void Info(string message)
        {
            this._logger.Info(message);
        }

        public void RunWithExceptionLogging(Action actionToRun, bool isSilent = false)
        {
            try
            {
                actionToRun();
            }
            catch (Exception ex)
            {
                this._logger.Error("ERROR: ", ex);
                if (isSilent) return;
                throw;
            }
        }

        public T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false)
        {
            try
            {
                return functionToRun();
            }
            catch (Exception ex)
            {
                this._logger.Error("ERROR: ", ex);
                if (isSilent) return (default(T));
                throw;
            }
        }
    }
}