using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using StockExchangeSimulator.BL.Contracts;

namespace StockExchangeSimulator.App.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog logger;

        public LoggerService(ILog logger)
        {
            this.logger = logger;
        }
        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            this.logger.Info(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Error(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            this.logger.Error(ex);
            //Console.WriteLine($" {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
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

                if (isSilent)
                {
                    return;
                }

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

                if (isSilent)
                {
                    return default(T);
                }

                throw;
            }
        }
    }
}
