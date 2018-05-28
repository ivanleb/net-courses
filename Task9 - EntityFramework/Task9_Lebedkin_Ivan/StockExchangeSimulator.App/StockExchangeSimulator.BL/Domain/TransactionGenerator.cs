using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data;
using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Domain
{
    public class TransactionGenerator: ITransactionGenerator
    {
        private readonly IClientService _repository;
        private readonly Random rnd;

        public TransactionGenerator(IClientService repository)
        {
            _repository = repository;
            rnd = new Random();
        }

        public Transaction GenerateTransaction(IEnumerable<ITransactionValidator> transactionValidators)
        {
            var transaction = new Transaction();
            transaction.Seller = _repository.GetUnicRandomClient(2);
            transaction.Buyer = _repository.GetUnicRandomClient(2);
            transaction.Stock = transaction.Seller.Stock;
            transaction.StocksQuantity = rnd.Next(0, Math.Abs(transaction.Seller.ClientStocksQuantity) + 1);
            transaction.Buyer.Balance -= transaction.Stock.Price * transaction.StocksQuantity;
            transaction.Seller.Balance += transaction.Stock.Price * transaction.StocksQuantity;
            transaction.Seller.ClientStocksQuantity -= transaction.StocksQuantity;

            if (transactionValidators.Any(checker => !checker.IsTransactionValid(transaction)))
            {
                throw new Exception("Incorrect transaction");
            }

            return transaction;
        }
    }
}
