using SESimulator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SESimulator.Model;
using SESimulator.Core;

namespace SESimulatorConsoleApp.Implementations
{
    class SimpleStockExchange : StockExchange
    {
        private BussinesService bussinesService;
        public event EventHandler<IDealInfo> OnDealConcluded;

        public SimpleStockExchange(BussinesService bussinesService)
        {
            this.bussinesService = bussinesService;
        }
        protected override IDealInfo MakeDeal()
        {
            var sellers = bussinesService.GetAllStockOwners();
            var sellerss = sellers.ToList();        
            var seller = sellerss.ToList().GetRandom();
            var stockForSale = seller.StocksForSale.GetRandom();
            var buyer = bussinesService.GetAllClients().Where(client => client.Id != seller.Id).GetRandom();
            this.bussinesService.RegisterNewDeal(seller, buyer, stockForSale, stockForSale.Cost);
            var dealInfo = new DealInfo() { Amount = stockForSale.Cost, Stock = stockForSale.Type.Name, Buyer = $"{buyer.Name} {buyer.Surname}", Seller = $"{seller.Name} {seller.Surname}" };
            OnDealConcluded?.Invoke(this, dealInfo);
            return dealInfo;
        }
    }
}
