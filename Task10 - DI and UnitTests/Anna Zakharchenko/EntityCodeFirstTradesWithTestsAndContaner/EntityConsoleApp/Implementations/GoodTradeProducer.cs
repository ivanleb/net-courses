using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore;
using EntityCore.Model;

namespace EntityConsoleApp.Implementations
{
    class GoodTradeProducer : TradeProducer
    {
        public GoodTradeProducer(BussinesService bussinesService) : base(bussinesService)
        {
        }
        public override void MakeTrade(Client seller, Client buyer, Stock stockForSale)
        {
            Trade trade = bussinesService.GetNewTrade(seller, buyer, stockForSale);
            bussinesService.RegisterNewTrade(trade);
        }
    }
}
