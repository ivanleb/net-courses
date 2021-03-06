﻿using System;
using log4net;

namespace ExceptionsAndLogging
{
    public interface ILoggerService
    {
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);

        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);

        void Info(string message);

        void Error(Exception ex);
    }

    public class LoggerService : ILoggerService
    {
        private ILog logger;

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
            catch (Exception ex)
            {
                this.logger.Error("ERROR: ", ex);
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
                this.logger.Error("ERROR: ", ex);
                if (isSilent) return (default(T));
                throw;
            }
        }
    }
}
