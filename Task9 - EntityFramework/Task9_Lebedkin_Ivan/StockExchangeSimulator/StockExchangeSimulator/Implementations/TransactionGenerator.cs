using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StockExchangeSimulator.DataModel;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;
using StockExchangeSimulator.Core;

namespace StockExchangeSimulator.Implementations
{
    public abstract class TransactionGenerator : ITransactionGenerator
    {
        public bool isContinue { get; set; }

        public abstract Transaction GetTransaction();

        public readonly ILoggerService LoggerService;

        public event Action<Transaction> onTransactionGenerate;

        protected TransactionGenerator(ILoggerService loggerService)
        {
            LoggerService = loggerService;
        }
        //ICollectionsModification<Transaction> Transactions
        public void Run(IEnumerable<Func<Transaction, bool>> isTransactionValid)
        {
            isContinue = true;
            bool cont = false;
            while (isContinue)
            {
                var transaction = LoggerService.RunWithExceptionLogging(() => GetTransaction(), isSilent: true);
                
                foreach (var checker in isTransactionValid)
                {
                    if (!checker(transaction))
                    {
                        cont = true;
                        break;
                    }
                }
                if (cont)
                {
                    LoggerService.Info("Incorrect transaction");
                    cont = false;
                    continue;
                }
                onTransactionGenerate?.Invoke(transaction);
                LoggerService.SendTransactionToLog(transaction);
                Thread.Sleep(100);
            }
        }
    }

    public class RandomTransactionGenerator : TransactionGenerator
    {
        public Random rnd;
        public event Func<Client> onTakeClient;

        public RandomTransactionGenerator(ILoggerService loggerService)
            : base(loggerService)
        {
            rnd = new Random();
        }

        public override Transaction GetTransaction()
        {
            var transaction = new Transaction();
            transaction.Buyer = onTakeClient();
            transaction.Seller = onTakeClient();

            if (transaction.Buyer.Stock == null || transaction.Seller.Stock == null) throw new Exception("Stock is null");
            if (transaction.Seller.ClientStocksQuantity <= 0)
                throw new Exception("Empty stock fund");

            transaction.StocksQuantity = rnd.Next(0, Math.Abs(transaction.Seller.ClientStocksQuantity) + 1);
            //Console.WriteLine($"transaction.StocksQuantity {transaction.StocksQuantity}");
            if (transaction.Seller.ClientStocksQuantity - transaction.StocksQuantity < 0)
            {
                throw new Exception("Negative stock quantity transaction");
            }
            transaction.Stock = transaction.Seller.Stock;
            return transaction;
        }
    }
}
