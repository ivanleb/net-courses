using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF_Core
{
    public class BussinesService
    {
        private readonly IDataContext dataContext;
        private readonly ILoggerService logger;
        Random rand = new Random();

        public BussinesService(IDataContext dataContext, ILoggerService logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public void RegisterShare(string Name)
        {
            var share = new Share()
            {
                ShareName = Name,
                ShareCost = rand.Next() % 50
            };

            dataContext.Add(share);

            dataContext.SaveChanges();

            logger.Info($"{share.ToString()} registred");
        }
        public void RegisterNewClient(string firstName, string lastName, string number, decimal balance)
        {
            var client = new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                Number = number,
                Zone = "White",
                Balance = balance
            };

            dataContext.Add(client);

            dataContext.SaveChanges();

            logger.Info($"{client.ToString()} registred");

            AddClientToWhiteZone(client);
        }

        public void RegisterClientShares(Client client)
        {
            foreach (Share share in dataContext.Shares.ToList())
            {
                var clientShare = new ClientShare()
                {
                    ClientId = client.Id,
                    ShareId = share.ShareId,
                    Amount = rand.Next() % 50
                };

                dataContext.Add(clientShare);

                logger.Info($"{clientShare.ToString()} registred for {client.ToString()}");
            }

            dataContext.SaveChanges();
        }

        public void AddClientToWhiteZone(Client client)
        {
            var whiteZoneClient = new WhiteZoneClient()
            {
                ClientId = client.Id
            };

            client.Zone = "White";

            dataContext.Update(client);

            dataContext.Add(whiteZoneClient);
            dataContext.SaveChanges();
            logger.Info($"{client.ToString()} added to white zone");
        }
        public void AddClientToOrangeZone(Client client)
        {
            var orangeZoneClient = new OrangeZoneClient()
            {
                ClientId = client.Id,
                Timeout = 15
            };

            client.Zone = "Orange";

            dataContext.Update(client);

            dataContext.Add(orangeZoneClient);
            dataContext.SaveChanges();
            logger.Info($"{client.ToString()} added to orange zone");
        }
        public void AddClientToBlackZone(Client client)
        {
            var blackZoneClient = new BlackZoneClient()
            {
                ClientId = client.Id,
                Penalty = 100
            };

            client.Zone = "Black";

            dataContext.Update(client);

            dataContext.Add(blackZoneClient);
            dataContext.SaveChanges();
            logger.Info($"{client.ToString()} added to black zone");
        }

        public void DeleteClientFromZone(Client client)
        {
            switch (client.Zone)
            {
                case "White":
                    dataContext.Remove(dataContext.WhiteZoneClients.First(c => c.ClientId == client.Id));
                    break;
                case "Orange":
                    dataContext.Remove(dataContext.OrangeZoneClients.First(c => c.ClientId == client.Id));
                    break;
                case "Black":
                    dataContext.Remove(dataContext.BlackZoneClients.First(c => c.ClientId == client.Id));
                    break;
            }

            logger.Info($"{client.ToString()} removed from {client.Zone} zone");
        }

        public void CreateDeal(Client Buyer, Client Seller)
        {
            var SellerShares = (from ClientShare cs in dataContext.ClientsShares where cs.ClientId == Seller.Id select cs).ToList();

            foreach (ClientShare cs in SellerShares)
            {
                int amount = rand.Next() % cs.Amount;

                var Deal = new DealHistory()
                {
                    BuyerId = Buyer.Id,
                    SellerId = Seller.Id,
                    ShareId = cs.ShareId, 
                    Amount = amount,
                    Total = amount * dataContext.Shares.First(s => s.ShareId == cs.ShareId).ShareCost
                };

                dataContext.Add(Deal);

                logger.Info($"DEAL {Deal.ToString()} OCCURED");

                Seller.Balance += Deal.Total;

                DeleteClientFromZone(Seller);

                if (Seller.Balance > 0)
                    AddClientToWhiteZone(Seller);
                else if (Seller.Balance == 0)
                    AddClientToOrangeZone(Seller);
                else if (Seller.Balance < 0)
                    AddClientToBlackZone(Seller);

                cs.Amount -= amount;

                var BuyerShare = (from ClientShare bs in dataContext.ClientsShares where bs.ClientId ==  Buyer.Id && bs.ShareId == cs.ShareId select bs).First();

                Buyer.Balance -= Deal.Total;

                DeleteClientFromZone(Buyer);

                if (Buyer.Balance > 0)
                    AddClientToWhiteZone(Buyer);
                else if (Buyer.Balance == 0)
                    AddClientToOrangeZone(Buyer);
                else if (Buyer.Balance < 0)
                    AddClientToBlackZone(Buyer);

                BuyerShare.Amount += amount;

                dataContext.Update(Buyer);
                dataContext.Update(BuyerShare);
                dataContext.Update(Seller);
                dataContext.Update(cs);

                logger.Info($"{Buyer.ToString()} changed");
                logger.Info($"{Seller.ToString()} changed");
                logger.Info($"{cs.ToString()} changed");
                logger.Info($"{BuyerShare.ToString()} changed");


                dataContext.SaveChanges();
            }

        }
    }
}
