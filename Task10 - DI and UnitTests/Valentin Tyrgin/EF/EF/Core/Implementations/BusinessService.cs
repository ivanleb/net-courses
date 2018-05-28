using System;
using System.Data.Entity;
using System.Linq;
using EF.Core.Abstractions;
using EF.Core.Services;
using EF.Implementations.Entities;

namespace EF.Core.Implementations
{
    public class BusinessService : IBusiness
    {
        public BusinessService(IDataContext dataContext, ILogService logService)
        {
            this.DataContext = dataContext;
            this.LogService = logService;
        }

        public IDataContext DataContext { get; }
        public ILogService LogService { get; }
        
        public void RegisterEntity<T>(T entity) where T : class, IEntity
        {
            DataContext.Add(entity);
            DataContext.SaveChanges();
            if (entity.GetType() == typeof(TradeOperation))
            {
                var msg = entity.GetInfo();
                LogService.Info("--> New operation: " + msg);
            }
        }

        public IQueryable<Trader> GetAllTraiders() => DataContext.Traders.Include(x => x.Assets);

        public IQueryable<TradeOperation> GetAllOperations() => DataContext.TradeOperations;

        public IQueryable<Trader> GetOrangeZoneTraiders()
        {
            return DataContext.Traders.Where(x => x.Status == "Orange");
        }

        public IQueryable<Trader> GetBlackZoneTraiders()
        {
            return DataContext.Traders.Where(x => x.Status == "Black");
        }

        public TradeOperation ProcessTrade(Trader seller, Trader buyer, Stock tradable)
        {
            Withdraw(seller, tradable);
            Acquire(buyer, tradable);
            UpdateEntity(buyer);
            UpdateEntity(seller);

            TradeOperation entity = TradeOperation.CreateBuilder()
                .SetTime(DateTime.Now)
                .SetBuyer(buyer)
                .SetSeller(seller)
                .SetTradableType(tradable.TradableType)
                .SetTradableAmount(tradable.Quantity)
                .SetTradeAmount(tradable.Quantity * tradable.TradableType.Price);

            return entity;
        }

        public void UpdateEntity<T>(T entity) where T : class, IEntity
        {
            if (entity is Stock && (entity as Stock).Quantity == 0)
                DataContext.Remove(entity);
            else DataContext.Update(entity);
            DataContext.SaveChanges();
        }

        public void Withdraw(Trader trader, Stock stock)
        {
            var _stock = trader.Assets.Single(x => x.TradableType == stock.TradableType);
            _stock.Quantity -= stock.Quantity;
            trader.Balance += stock.TradableType.Price * stock.Quantity;
            UpdateEntity(_stock);
        }

        public void Acquire(Trader trader, Stock stock)
        {
            if (trader.Assets.Select(x => x.TradableType).Contains(stock.TradableType))
            {
                var _stock = trader.Assets.Single(x => x.TradableType == stock.TradableType);
                _stock.Quantity += stock.Quantity;
            }
            else
            {
                trader.Assets.Add(stock);
            }
            trader.Balance -= stock.TradableType.Price * stock.Quantity;
        }

    }
}