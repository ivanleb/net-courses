using System;
using log4net;
using System.Collections.Generic;
using Exceptions_Logging.Abstractions;

namespace Exceptions_Logging.Implementations
{
    public class LoggerService : ILogable
    {
        private readonly ILog logger;

        public LoggerService(ILog logger)
        {
            this.logger = logger;
        }

        public void Error(Exception ex)
        {
            this.logger.Error(ex);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void RunWithExceptionLogging(Action actionToRun, bool isSilent = false)
        {
            try
            {
                actionToRun();
            }
            catch(Exception ex)
            {
                this.logger.Error("Error: ", ex);

                if (isSilent)
                    return;

                throw;
            }
        }

        public T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false)
        {
            try
            {
                return functionToRun();
            }
            catch(Exception ex)
            {
                this.logger.Error("Errro: ", ex);

                if (isSilent)
                    return default(T);

                throw;
            }
        }
    }

}

