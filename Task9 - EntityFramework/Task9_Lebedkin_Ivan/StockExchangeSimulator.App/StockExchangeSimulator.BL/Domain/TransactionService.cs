using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.BL.Domain
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryTransaction _repositoryTransaction;

        public TransactionService(IRepositoryTransaction repositoryTransaction)
        {
            _repositoryTransaction = repositoryTransaction;
        }

        public Transaction GetTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void Add(Transaction transaction)
        {
            _repositoryTransaction.Add(transaction);
            _repositoryTransaction.SaveChanges();
        }
    }
}