namespace StockExchangeSimulator.App.Implementations
{
    public static class DataContexExtensions
    {
        public static  void ClearData(this StockExchangeDataContext dataContext)
        {
            dataContext.Clients.RemoveRange(dataContext.Clients);
            dataContext.Stocks.RemoveRange(dataContext.Stocks);
            dataContext.Transactions.RemoveRange(dataContext.Transactions);

            dataContext.SaveChanges();
        }
    }
}