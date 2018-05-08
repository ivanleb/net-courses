using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.Core;
using StockExchangeSimulator.Core.DataModel;
using log4net;

namespace StockExchangeSimulator.Implementations
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog logger;

        public LoggerService(ILog logger)
        {
            this.logger = logger;
        }

        public void SendTransactionToLog(Transaction transaction)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            this.Info($"\n{transaction.Seller.FirstName} {transaction.Seller.SurName} -> {transaction.Buyer.FirstName} {transaction.Buyer.SurName},\nstock number = {transaction.StocksQuantity}, stock price= {transaction.Stock.Price}\n");// cost {(long)transaction.StocksQuantity * (long)transaction.Stock.Price}");
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine($"{transaction.Seller.FirstName} {transaction.Seller.SurName} - > {transaction.Buyer.FirstName} {transaction.Buyer.SurName}, cost {transaction.StocksQuantity * transaction.Stock.Price}");
        }

        public void Error(Exception ex)
        {
            
            this.logger.Error(ex);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
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
