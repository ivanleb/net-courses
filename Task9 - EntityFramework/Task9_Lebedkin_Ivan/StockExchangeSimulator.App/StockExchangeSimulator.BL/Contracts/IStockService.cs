using StockExchangeSimulator.Data.Models;

namespace StockExchangeSimulator.BL.Contracts
{
    public interface IStockService
    {
        void AddStock(Stock stock);
    }
}