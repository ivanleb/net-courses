using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Abstractions;
using EntityCore.Model;
using EntityCore;

namespace EntityConsoleApp.Implementations
{
    abstract class TradeProducer : IProducer
    {
        public bool IsContinue  {get; set; }
        protected readonly LoggerService loggerService;
        protected readonly BussinesService bussinesService;

        protected TradeProducer(LoggerService loggerService, BussinesService bussinesService)
        {
            this.loggerService = loggerService;
            this.bussinesService = bussinesService;
        }

        public abstract void MakeTrade(Client seller, Client buyer, Stock stock);
        public void Run(int number)
        {
            Random rnd = new Random();
            IsContinue = true;
            while (IsContinue)
            {
                Client seller = bussinesService.GetClient(rnd.Next(1, number));
                Client buyer = bussinesService.GetClient(rnd.Next(1, number));
                Stock stockForSale = bussinesService.GetSellerStock(seller);
                if (stockForSale != null)
                {
                    MakeTrade(seller, buyer, stockForSale);
                }

                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}