using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EF.Core.Abstractions;
using EF.Implementations.Entities;

namespace EF.Core.Implementations
{
    internal abstract class TransactionGenerator : ITransactionGenerator
    {
        private readonly IBusiness _businessService;

        protected TransactionGenerator(IBusiness bs)
        {
            _businessService = bs;
        }

        public Task Generate()
        {
            return Task.Run(() =>
            {
                while (Active)
                {
                    var buyer =
                        GetCollectionItem(
                            _businessService.GetAllTraiders()
                                .ToList()
                                .Where(x => x.TraderInfo.SecondName != "Тиньков")
                                .ToList());
                    var seller = GetCollectionItem(_businessService.GetAllTraiders().ToList()
                        .Where(x => x != buyer)
                        .Where(x => x.Assets.Any())
                        .ToList());
                    var stock = GetCollectionItem(seller.Assets);
                    var amount = GetValue(stock);

                    var inOperationStock = new Stock
                    {
                        Quantity = amount,
                        TradableType = stock.TradableType,
                        Trader = buyer
                    };
                    var operation = _businessService.ProcessTrade(seller, buyer, inOperationStock);
                    _businessService.RegisterEntity(operation);

                    Thread.Sleep(200);
                }
            });
        }

        public abstract T GetCollectionItem<T>(ICollection<T> entityCollection) where T : IEntity;

        public abstract int GetValue(object ob);

        public bool Active { get; set; }

    }
}