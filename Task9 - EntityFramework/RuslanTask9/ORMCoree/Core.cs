using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMCore
{
    public enum  SharesTypes { FirstType = 15, SecondType= 70, ThirdType = 300 }
    
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

        public string ValueType { get; set; }

        public override string ToString()
        {
            return $"Shareholder ID: {ShareholderId} | Buyer ID {BuyerId} | ValueType: {ValueType} | Value: {Value}";
        }
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

        public IQueryable<Shareholder> GetMostWantedShareholders()
        {
            return this.dataContext.Shareholders;
        }

        public Shareholder GetMostWantedShareholdersById(int memberId)
        {
            return this.dataContext.Shareholders.Where(w=>w.Id==memberId).First();
        }

        public IQueryable<Trade> GetMostWantedTradesByName(int memberId)
        {
            return this.dataContext.Trades.Where(w =>
            w.BuyerId == memberId
            |
            w.ShareholderId == memberId);
        }


        public Balance GetMostWantedBalanceById(int memberId)
        {
            return this.dataContext.Balances.Where(w => w.Id == memberId).FirstOrDefault();
        }

        public void RegisterNewShareholder(Shareholder shareholder)
        {
            this.dataContext.Add(shareholder);

            RegisterNewBalance(shareholder.Id, 1000, 1000, 1000, 7000, "middle");

            this.dataContext.SaveChanges();
        }

        public void RegisterNewTrade(Trade trade, Shareholder shareholder, Shareholder buyer)//int inputShareholderId, int InputBuyerId, SharesTypes FST, int value)
        {
            this.dataContext.Add(trade);

            var newShareholderBalance = GetMostWantedBalanceById(shareholder.Id);

            var newBuyerBalance = GetMostWantedBalanceById(buyer.Id);

            //var shareTypes = Enum.GetNames(typeof(SharesTypes)); how to do constant value?!

            //var c = nameof(SharesTypes.FirstType);

            switch (trade.ValueType)
            {
                case "FirstType":
                    {

                        newShareholderBalance.FirstType -= (int)trade.Value;

                        newShareholderBalance.BalanceValue += trade.Value * (Decimal)SharesTypes.FirstType;

                        if (newShareholderBalance.BalanceValue == 0)
                            newShareholderBalance.BalanceZone = "WARNING!";

                        if (newShareholderBalance.BalanceValue > 0)
                            newShareholderBalance.BalanceZone = "low";

                        if (newShareholderBalance.BalanceValue > 5000)
                            newShareholderBalance.BalanceZone = "middle";
                        

                        newBuyerBalance.FirstType += (int)trade.Value;

                        newBuyerBalance.BalanceValue -= trade.Value * (Decimal)SharesTypes.FirstType;

                        if (newBuyerBalance.BalanceValue < 5000)
                            newBuyerBalance.BalanceZone = "low";

                        if (newBuyerBalance.BalanceValue == 0)
                            newBuyerBalance.BalanceZone = "WARNING!";

                        if (newBuyerBalance.BalanceValue < 0)
                            newBuyerBalance.BalanceZone = "in the dark!";

                        UpdateBalancesAndSaveChanges(newBuyerBalance, newShareholderBalance);

                        break;
                    }
                case "SecondType":
                    {
                        newShareholderBalance.SecondType -= (int)trade.Value;

                        newShareholderBalance.BalanceValue += trade.Value * (Decimal)SharesTypes.SecondType;

                        if (newShareholderBalance.BalanceValue == 0)
                            newShareholderBalance.BalanceZone = "WARNING!";

                        if (newShareholderBalance.BalanceValue > 0)
                            newShareholderBalance.BalanceZone = "low";

                        if (newShareholderBalance.BalanceValue > 5000)
                            newShareholderBalance.BalanceZone = "middle";

                        newBuyerBalance.SecondType += (int)trade.Value;

                        newBuyerBalance.BalanceValue -= trade.Value * (Decimal)SharesTypes.SecondType;

                        if (newBuyerBalance.BalanceValue < 5000)
                            newBuyerBalance.BalanceZone = "low";

                        if (newBuyerBalance.BalanceValue == 0)
                            newBuyerBalance.BalanceZone = "WARNING!";

                        if (newBuyerBalance.BalanceValue < 0)
                            newBuyerBalance.BalanceZone = "in the dark!";
                        
                        UpdateBalancesAndSaveChanges(newBuyerBalance, newShareholderBalance);

                        break;
                    }
                case "ThirdType":
                    {
                        newShareholderBalance.ThirdType -= (int)trade.Value;

                        newShareholderBalance.BalanceValue += trade.Value * (Decimal)SharesTypes.ThirdType;

                        if (newShareholderBalance.BalanceValue == 0)
                            newShareholderBalance.BalanceZone = "WARNING!";

                        if (newShareholderBalance.BalanceValue > 0)
                            newShareholderBalance.BalanceZone = "low";

                        if (newShareholderBalance.BalanceValue > 5000)
                            newShareholderBalance.BalanceZone = "middle";
                        
                        newBuyerBalance.ThirdType += (int)trade.Value;

                        newBuyerBalance.BalanceValue -= trade.Value * (Decimal)SharesTypes.ThirdType;

                        if (newBuyerBalance.BalanceValue < 5000)
                            newBuyerBalance.BalanceZone = "low";

                        if (newBuyerBalance.BalanceValue == 0)
                            newBuyerBalance.BalanceZone = "WARNING!";

                        if (newBuyerBalance.BalanceValue < 0)
                            newBuyerBalance.BalanceZone = "in the dark!";
                        
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

        void RegisterNewBalance(
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
        }
    }
}
