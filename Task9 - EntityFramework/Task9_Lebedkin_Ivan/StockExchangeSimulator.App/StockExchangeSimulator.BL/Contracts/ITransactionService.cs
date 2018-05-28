using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface ITransactionService
    {
        Transaction GetTransaction();
        void Add(Transaction transaction);
    }
}