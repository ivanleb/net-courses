using SESimulator.Abstractions;
using System;
using System.Linq;
using SESimulator.Core;
using SESimulator.Extentions;

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
            var seller = sellers.GetRandom();
            var stockForSale = seller.Stocks.Where(stock=>stock.IsForSale).GetRandom();
            var cost = stockForSale.Type.Cost;
            var buyer = bussinesService.GetAllClients().Where(client => client.Id != seller.Id).GetRandom();
            this.bussinesService.RegisterNewDeal(seller, buyer, stockForSale, cost);
            var dealInfo = new DealInfo() { Amount = cost, Stock = stockForSale.Type.Name, Buyer = $"{buyer.Name} {buyer.Surname}", Seller = $"{seller.Name} {seller.Surname}" };
            OnDealConcluded?.Invoke(this, dealInfo);
            return dealInfo;
        }
    }
}
