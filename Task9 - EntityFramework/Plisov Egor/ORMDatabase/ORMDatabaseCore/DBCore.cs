using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMDatabaseCore
{
    public enum SharesType { SimpleType = 10, PreferenceShare = 100 }

    public abstract class Table
    {
        [Key]
        public virtual int ID { get; set; }
    }

   public class Traider : Table
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNum { get; set; }

        public ICollection<Deal> IDDeal { get; set; }
    }

    public class Shares : Table
    {
        public int SimpleType { get; set; }
        public int PreferenceShare { get; set; }

    }

    public class TraiderBalance : Shares
    {
        [ForeignKey("Traider")]
        public override int ID { get; set; }
        public decimal Balance { get; set; }
        public string Zone { get; set; }
        public Traider Traider { get; set; }
    }

    public class Deal : Table
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        public int ID_seller { get; set; }
        public int ID_buyer { get; set; }
        public decimal Price { get; set; }
        public SharesType SharesType { get; set; }

    }

    public interface ICollectionsModification<in T> where T: Table
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }

    public interface IDataContex :
        ICollectionsModification<Traider>,
        ICollectionsModification<TraiderBalance>,
        ICollectionsModification<Deal>
    {
        IQueryable<Traider> Traiders { get; }
        IQueryable<TraiderBalance> TraiderBalances { get; }
        IQueryable<Deal> Deals { get; }

        void SaveChanges();
    } 

    public class BussinesService
    {
        private readonly IDataContex dataContex;

        public BussinesService(IDataContex dataContex)
        {
            this.dataContex = dataContex;
        }

        public IDataContex GetDataContex() => this.dataContex;

        public IQueryable<Traider> GetMostWantedTraider()
        {
            return this.dataContex.Traiders;
        }

        public Traider GetMostWantedTraiderById(int ID)
        {
            return this.dataContex.Traiders.Where(w => w.ID == ID).First();
        }

        public TraiderBalance GetMostWantedBalanceById(int traiderID)
        {
            return this.dataContex.TraiderBalances.Where(w => w.ID == traiderID).First();
        }

        public Traider GetMostWantedTraiderNameById(int traderID)
        {
            return this.dataContex.Traiders.Where(w => w.ID == traderID).First();
        }

        public IQueryable<Traider> GetZeroBalanceTraider()
        {
            IQueryable<TraiderBalance> bankrots = this.dataContex.TraiderBalances.Where(w => w.Balance == 0);
            IQueryable bankrotID = bankrots.Select(s => s.ID);
            List<Traider> bankrotsList = new List<Traider>();

            foreach (int i in bankrotID)
            {
                bankrotsList.Add(this.dataContex.Traiders.Where(w => w.ID == i).First());
            }
            return bankrotsList.AsQueryable();

        }

        public void AddNewTraiderWithStarterPack(Traider traider)
        {
            this.dataContex.Add(traider);
            TraiderBalance starterPack = new TraiderBalance()
            {
                ID = traider.ID,
                SimpleType = 5000,
                PreferenceShare = 1000,
                Balance = 10000,
                Zone = "Green"
            };
            this.dataContex.Add(starterPack);
            this.dataContex.SaveChanges();
        }

        public void RegisterNewDeal(Deal deal)
        {
            TraiderBalance newSellerBalance = GetMostWantedBalanceById(deal.ID_seller);
            TraiderBalance newBuyerBalance = GetMostWantedBalanceById(deal.ID_buyer);

            if (deal.SharesType == SharesType.SimpleType)
                UpdatingBalance(newSellerBalance, newBuyerBalance, SharesType.SimpleType, deal);
            if (deal.SharesType == SharesType.PreferenceShare)
                UpdatingBalance(newSellerBalance, newBuyerBalance, SharesType.PreferenceShare, deal);
        }

        void UpdatingBalance(TraiderBalance sellerBalance, TraiderBalance buyerBalance, SharesType sharesType, Deal newDeal)
        {
            if (sharesType == SharesType.SimpleType)
                sellerBalance.SimpleType -= (int)newDeal.Price;
            if (sharesType == SharesType.PreferenceShare)
                sellerBalance.PreferenceShare -= (int)newDeal.Price;

            sellerBalance.Balance += newDeal.Price * (decimal)sharesType;

            if (sellerBalance.Balance == 0)
                sellerBalance.Zone = "Red";
            if (sellerBalance.Balance > 0 && sellerBalance.Balance <= 1000)
                sellerBalance.Zone = "Orange";
            if (sellerBalance.Balance > 1000)
                sellerBalance.Zone = "Green";

            if (sharesType == SharesType.SimpleType)
                buyerBalance.SimpleType += (int)newDeal.Price;
            if (sharesType == SharesType.PreferenceShare)
                buyerBalance.PreferenceShare += (int)newDeal.Price;

            buyerBalance.Balance -= newDeal.Price * (decimal)sharesType;

            if (buyerBalance.Balance == 0)
                buyerBalance.Zone = "Red";
            if (buyerBalance.Balance > 0 && buyerBalance.Balance <= 1000)
                buyerBalance.Zone = "Orange";
            if (buyerBalance.Balance > 1000)
                buyerBalance.Zone = "Green";

            this.dataContex.Update(sellerBalance);
            this.dataContex.Update(buyerBalance);
            this.dataContex.SaveChanges();
        }


    }
}
