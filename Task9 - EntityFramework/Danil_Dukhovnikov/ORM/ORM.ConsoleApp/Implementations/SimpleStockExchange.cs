using System;
using System.Linq;
using ORM.Core;
using ORM.Core.Abstractions;
using ORM.Core.Extentions;

namespace ORM.ConsoleApp.Implementations
{
    internal class SimpleStockExchange : StockExchange
    {
        private readonly BussinesService _bussinesService;
        public event EventHandler<IDealInfo> OnDealConcluded;

        public SimpleStockExchange(BussinesService bussinesService)
        {
            this._bussinesService = bussinesService;
        }
        protected override IDealInfo MakeDeal()
        {
            var sellers = _bussinesService.GetAllStockOwners();
            var seller = sellers.GetRandom();
            var stockForSale = seller.Stocks.Where(stock=>stock.IsForSale).GetRandom();
            var cost = stockForSale.Type.Cost;
            var buyer = _bussinesService.GetAllClients().Where(client => client.Id != seller.Id).GetRandom();
            this._bussinesService.RegisterNewDeal(seller, buyer, stockForSale, cost);
            var dealInfo = new DealInfo() { Amount = cost, Stock = stockForSale.Type.Name, Buyer = $"{buyer.Name} {buyer.Surname}", Seller = $"{seller.Name} {seller.Surname}" };
            OnDealConcluded?.Invoke(this, dealInfo);
            return dealInfo;
        }
    }
}
