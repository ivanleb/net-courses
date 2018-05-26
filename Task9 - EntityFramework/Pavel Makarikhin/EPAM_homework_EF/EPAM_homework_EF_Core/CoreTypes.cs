using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF_Core
{
    public class Share
    {
        public int ShareId { get; set; }
        public string ShareName { get; set; }
        public decimal ShareCost { get; set; }
        public override string ToString()
        {
            return $"Share:{ShareId} {ShareName} {ShareCost}";
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Zone { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"Client:{Id} {FirstName} {LastName} {Number} {Balance}";
        }
    }

    public class ClientShare
    {
        public int ClientId { get; set; }
        public int ShareId { get; set; }
        public int Amount { get; set; }

        public override string ToString()
        {
            return $"ClientID:{ClientId} ShareID:{ShareId} Amount:{Amount}";
        }
    }

    public class DealHistory
    {
        public int DealId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int ShareId { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public override string ToString()
        {
            return $"DealID:{DealId} BuyerID: {BuyerId} SellerID:{SellerId} ShareID:{ShareId} Amount:{Amount} TOTAL:{Total}";
        }
    }

    public class WhiteZoneClient
    {
        public int ClientId { get; set; }
    }

    public class OrangeZoneClient : WhiteZoneClient
    {
        public int Timeout { get; set; }
    }
    public class BlackZoneClient : WhiteZoneClient
    {
        public int Penalty { get; set; }
    }

    public interface ICollectionModification<in T>
    {
        int Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }

    public interface IDataContext :
        ICollectionModification<WhiteZoneClient>,
        ICollectionModification<OrangeZoneClient>,
        ICollectionModification<BlackZoneClient>,
        ICollectionModification<Client>,
        ICollectionModification<Share>,
        ICollectionModification<DealHistory>,
        ICollectionModification<ClientShare>
    {
        IQueryable<WhiteZoneClient> WhiteZoneClients { get; }
        IQueryable<OrangeZoneClient> OrangeZoneClients { get; }
        IQueryable<BlackZoneClient> BlackZoneClients { get; }
        IQueryable<Client> Clients { get; }
        IQueryable<Share> Shares { get; }
        IQueryable<ClientShare> ClientsShares { get; }
        IQueryable<DealHistory> History { get; }

        void SaveChanges();
    }
}
