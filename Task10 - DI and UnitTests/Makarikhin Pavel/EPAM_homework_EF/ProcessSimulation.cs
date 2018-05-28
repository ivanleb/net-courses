using EPAM_homework_EF_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_EF
{
    class ProcessSimulation : IProcess
    {
        private readonly IDataContext context;

        public ProcessSimulation(IDataContext context)
        {
            this.context = context;
        }

        public void Run(BussinesService service)
        {
            Random rand = new Random();

            for (int i = 0; i < rand.Next() % 8; i++)
            {/*/
                Task.Run(() =>
                {*/
                    List<Client> ClientsList = context.Clients.ToList();

                    int Buyer = rand.Next() % context.Clients.Count(), Seller = (Buyer + rand.Next() % context.Clients.Count()) % context.Clients.Count();

                    service.CreateDeal(ClientsList[Buyer], ClientsList[Seller]);
                //});
                /*
                DealHistory deal = new DealHistory();

                List<Client> ClientsList = context.Clients.ToList();
                deal.BuyerId = ClientsList[rand.Next() % context.Clients.Count()].Id;
                deal.SellerId = (deal.BuyerId + ClientsList[rand.Next() % context.Clients.Count()].Id) % context.Clients.Count();
                deal.ShareId = context.Shares.ToList()[rand.Next() % context.Shares.Count()].ShareId;
                deal.Amount = rand.Next() % context.ClientsShares.First(s => s.ClientId == deal.SellerId && s.ShareId == deal.ShareId).Amount;

                context.Add(deal);
                context.SaveChanges();
                */
            }
        }
    }
}
