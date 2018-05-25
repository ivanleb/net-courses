using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMCore
{
    public enum  SharesTypes { FirstType = 15, SecondType= 30, ThirdType = 300 }

    public abstract class Tabels
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
    }

    public abstract class AbstractPerson : Tabels
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class Shareholder : AbstractPerson
    {
        public ICollection<Trade> IdTrades { get; set; }
    }

    public class Shares : Tabels
    {
        public int FirstType { get; set; }

        public int SecondType { get; set; }

        public int ThirdType { get; set; }
    }

    public class Balance : Shares
    {
        [ForeignKey("Shareholder")]
        public override int Id { get; set; }

        public decimal BalanceValue { get; set; }

        public string BalanceZone { get; set; }

        public Shareholder Shareholder { get; set; }
    }

    public class Trade : Tabels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int ShareholderId { get; set; }
        
        public int BuyerId { get; set; }

        public decimal Value { get; set; }

        public SharesTypes ValueType { get; set; }

        //public override string ToString()
        //{
        //    return $"Shareholder ID: {ShareholderId} | Buyer ID {BuyerId} | ValueType: {ValueType} | Value: {Value}";
        //}
    }

    public interface ICollectionsModification<in T> where T : Tabels
    {
        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);
    }

    public interface IDataContext :
        ICollectionsModification<Shareholder>,
        ICollectionsModification<Trade>,
        ICollectionsModification<Balance>
    {
        IQueryable<Shareholder> Shareholders { get; }

        IQueryable<Trade> Trades { get; }

        IQueryable<Balance> Balances { get; }

        void SaveChanges();
    }


    public class BussinesService
    {
        private readonly IDataContext dataContext;

        public BussinesService(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IDataContext GetDataContext() => this.dataContext;

        public IQueryable<Shareholder> GetMostWantedShareholdersByShareholder(Shareholder shareholder)
        {
            return this.dataContext.Shareholders.Where(w =>
            w.FirstName == shareholder.FirstName
            &&
            w.LastName == shareholder.LastName
            &&
            w.PhoneNumber == shareholder.PhoneNumber);
        }

        public IQueryable<Shareholder> GetAllShareholders()
        {
            return this.dataContext.Shareholders;
        }

        public Shareholder GetMostWantedShareholdersById(int memberId)
        {
            return this.dataContext.Shareholders.Where(w=>w.Id==memberId).FirstOrDefault();
        }

        public IQueryable<Trade> GetMostWantedTradesByName(int memberId)
        {
            return this.dataContext.Trades.Where(w =>
            w.BuyerId == memberId
            |
            w.ShareholderId == memberId);
        }

        public IQueryable<Balance> GetBalancesWithZeroBalance()
        {
            return this.dataContext.Balances.Where(w => w.BalanceValue == 0);
        }

        public IQueryable<Shareholder> GetShareholdersWithZeroBalance()
        {
            var zeroBalances = GetBalancesWithZeroBalance();
            var idsOfZeroBalances = zeroBalances.Select(s => s.Id);
            var shareholdersWithZeroBalance = new List<Shareholder>();

            foreach (var id in idsOfZeroBalances)
                shareholdersWithZeroBalance.Add(this.dataContext.Shareholders.Where(w => w.Id == id).FirstOrDefault());
            return shareholdersWithZeroBalance.AsQueryable();
        }

        public Balance GetMostWantedBalanceById(int memberId)
        {
            return this.dataContext.Balances.Where(w => w.Id == memberId).FirstOrDefault();
        }

        public void RegisterNewShareholderWithStartingBalance(Shareholder shareholder)
        {
            this.dataContext.Add(shareholder);

            RegisterNewBalance(
                shareholderId: shareholder.Id,
                firstType: 1000,
                secondType: 1000,
                thirdType: 1000,
                balanceValue: 7000,
                balanceZone: "middle");
            //this.dataContext.SaveChanges();
        }

        public void RegisterNewTrade(Trade trade, Shareholder shareholder, Shareholder buyer)
        {
            this.dataContext.Add(trade);

            var newShareholderBalance = GetMostWantedBalanceById(shareholder.Id);

            var newBuyerBalance = GetMostWantedBalanceById(buyer.Id);

            switch (trade.ValueType)
            {
                case SharesTypes.FirstType:
                    {
                        PreparationsForUpdatingBalances(newShareholderBalance, newBuyerBalance, SharesTypes.FirstType, trade);

                        UpdateBalancesAndSaveChanges(newBuyerBalance, newShareholderBalance);

                        break;
                    }
                case SharesTypes.SecondType:
                    {
                        PreparationsForUpdatingBalances(newShareholderBalance, newBuyerBalance, SharesTypes.SecondType, trade);

                        UpdateBalancesAndSaveChanges(newBuyerBalance, newShareholderBalance);

                        break;
                    }
                case SharesTypes.ThirdType:
                    {
                        PreparationsForUpdatingBalances(newShareholderBalance, newBuyerBalance, SharesTypes.ThirdType, trade);
                        
                        UpdateBalancesAndSaveChanges(newBuyerBalance, newShareholderBalance);

                        break;
                    }
            }

            void UpdateBalancesAndSaveChanges(Balance buyerBalance, Balance shareholderBalance)
            {
                this.dataContext.Update(buyerBalance);

                this.dataContext.Update(shareholderBalance);

                this.dataContext.SaveChanges();
            }
            
        }

        public void PreparationsForUpdatingBalances(Balance shareholderBalance, Balance buyerBalance, SharesTypes sharesType, Trade inputedTrade)
        {
            if (sharesType == SharesTypes.FirstType)
                shareholderBalance.FirstType -= (int)inputedTrade.Value;

            if (sharesType == SharesTypes.SecondType)
                shareholderBalance.SecondType -= (int)inputedTrade.Value;

            if (sharesType == SharesTypes.ThirdType)
                shareholderBalance.ThirdType -= (int)inputedTrade.Value;


            shareholderBalance.BalanceValue += inputedTrade.Value * (Decimal)sharesType;

            if (shareholderBalance.BalanceValue < 0)
                shareholderBalance.BalanceZone = "Black Zone!";

            if (shareholderBalance.BalanceValue == 0)
                shareholderBalance.BalanceZone = "Orange Zone!";

            if (shareholderBalance.BalanceValue > 0)
                shareholderBalance.BalanceZone = "low";

            if (shareholderBalance.BalanceValue > 5000)
                shareholderBalance.BalanceZone = "middle";

            if (shareholderBalance.BalanceValue > 10000)
                shareholderBalance.BalanceZone = "high";

            if (shareholderBalance.BalanceValue > 20000)
                shareholderBalance.BalanceZone = "insane";

            if (sharesType == SharesTypes.FirstType)
                buyerBalance.FirstType += (int)inputedTrade.Value;

            if (sharesType == SharesTypes.SecondType)
                buyerBalance.SecondType += (int)inputedTrade.Value;

            if (sharesType == SharesTypes.ThirdType)
                buyerBalance.ThirdType += (int)inputedTrade.Value;

            buyerBalance.BalanceValue -= inputedTrade.Value * (Decimal)sharesType;//SharesTypes.SecondType;

            if (buyerBalance.BalanceValue > 20000)
                buyerBalance.BalanceZone = "insane";

            if (buyerBalance.BalanceValue < 20000)
                buyerBalance.BalanceZone = "high";

            if (buyerBalance.BalanceValue < 10000)
                buyerBalance.BalanceZone = "middle";

            if (buyerBalance.BalanceValue < 5000)
                buyerBalance.BalanceZone = "low";

            if (buyerBalance.BalanceValue == 0)
                buyerBalance.BalanceZone = "Orange Zone!";

            if (buyerBalance.BalanceValue < 0)
                buyerBalance.BalanceZone = "Black Zone!";
        }

        public void RegisterNewBalance(
            int shareholderId,
            int firstType, int secondType, int thirdType,
            decimal balanceValue, string balanceZone)
        {
            var balance = new Balance()
            {
                Id = shareholderId,
                FirstType = firstType,
                SecondType = secondType,
                ThirdType = thirdType,
                BalanceValue = balanceValue,
                BalanceZone = balanceZone
            };
            
            this.dataContext.Add(balance);
            this.dataContext.SaveChanges();
        }

        
    }
}
