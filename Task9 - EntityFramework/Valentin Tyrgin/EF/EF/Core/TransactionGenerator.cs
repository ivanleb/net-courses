using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EF.Core.Repositories;
using EF.Core.Services;
using EF.Implementations;

namespace EF.Core
{
    internal abstract class TransactionGenerator : IGenerator, IGeneratorServices
    {
        private readonly BusinessService _businessService;

        protected TransactionGenerator(BusinessService bs)
        {
            _businessService = bs;
        }

        public bool Active { get; set; }

        public void Generate()
        {
            while (Active)
            {
                var buyer =
                    GetCollectionItem(_businessService.GetAllClients().Where(x => x.SecondName != "Тиньков").ToList());
                var seller = GetCollectionItem(_businessService.GetAllClients().ToList()
                    .Where(x => x != buyer)
                    .Where(x => x.Stocks.Any())
                    .ToList());
                var stock = GetCollectionItem(seller.Stocks);
                var amount = GetValue(stock);

                var inOperationStock = new Stock
                {
                    Quantity = amount,
                    StockType = stock.StockType,
                    Traider = buyer
                };
                _businessService.ProcessTrade(seller,buyer,inOperationStock);

                Thread.Sleep(200);
            }
        }

        public abstract T GetCollectionItem<T>(ICollection<T> entityCollection) where T : IId;

        public abstract int GetValue(object ob);
    }
}