using System;
using System.Runtime.CompilerServices;
using DIAndUnitTests.Core.Abstractions;
using log4net;

namespace DIAndUnitTests.ConsoleApp.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog _logger;

        public LoggerService(ILog logger)
        {
            _logger = logger;
        }


        public void RunWithExeptionLogging(Action actionToRun, bool isSilent = false)
        {
            try
            {
                actionToRun();
            }
            catch (Exception e)
            {
                _logger.Error("ERROR: ", e);
                if (isSilent) return;
                throw;
            }
        }

        public T RunWithExeptionLogging<T>(Func<T> functionToRun, bool isSilent = false)
        {
            try
            {
                return functionToRun();
            }
            catch (Exception ex)
            {
                this._logger.Error("ERROR: ", ex);
                if (isSilent) return default(T);
                throw;
            }
        }

        public void Info(string message)
        {
            this._logger.Info(message);
        }

        public void Error(Exception ex)
        {
           this._logger.Error(ex);
        }
    }
}