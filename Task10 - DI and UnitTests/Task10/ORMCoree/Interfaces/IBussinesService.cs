using System.Linq;

namespace ORMCore
{
    public interface IBussinesService
    {
        IDataContext GetDataContext();
        IQueryable<Shareholder> GetShareholdersWithZeroBalance();
        void RegisterNewShareholderWithStartingBalance(Shareholder shareholder);
        void RegisterNewTrade(Trade trade, Shareholder shareholder, Shareholder buyer);
    }
}