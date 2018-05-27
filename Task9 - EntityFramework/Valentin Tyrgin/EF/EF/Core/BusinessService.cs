using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EF.Core.Repositories;
using EF.Implementations;

namespace EF
{
    public class BusinessService
    {
        private readonly IDataContext _dataContext;

        public BusinessService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void RegisterTraider(string fName, string sName, decimal cash, ICollection<Stock> stock = null)
        {
            var entity = new Traider
            {
                FirstName = fName,
                SecondName = sName,
                Balance = cash,
                Stocks = stock
            };
            Logger.Log.Info("New Traider ---> " + entity.GetInfo());
            _dataContext.Add(entity);
            _dataContext.SaveChanges();
        }

        public void RegisterStock(StockType type, Traider traider, int amount)
        {
            var entity = new Stock
            {
                StockType = type,
                Traider = traider,
                Quantity = amount
            };
            _dataContext.Add(entity);
            _dataContext.SaveChanges();
        }

        public void RegisterStockType(string type, decimal price)
        {
            var entity = new StockType
            {
                Type = type,
                Price = price
            };
            _dataContext.Add(entity);
            _dataContext.SaveChanges();
        }

        public void RegisterOperation(Traider buyer, Traider seller, Stock stock)
        {
            var operation = new Operation
            {
                Buyer = buyer,
                Seller = seller,
                StockAmount = stock.Quantity,
                StockType = stock.StockType,
                Date = DateTime.Now,
                Cash = stock.StockType.Price * stock.Quantity
            };
            Logger.Log.Info("New Operation ---> " + operation.GetInfo() + "\n");
            _dataContext.Add(operation);
            _dataContext.SaveChanges();
        }

        public void UpdateStock(Stock stock)
        {
            _dataContext.Update(stock);
            _dataContext.SaveChanges();
        }

        public void UpdateTraider(Traider traider)
        {
            _dataContext.Update(traider);
            _dataContext.SaveChanges();
        }

        public IQueryable<Traider> GetAllClients() => _dataContext.Traiders.Include(x => x.Stocks);

        public IQueryable<Operation> GetAllOperations() => _dataContext.Operations;

        public IQueryable<Traider> GetOrangeZoneTraiders()
        {
            return _dataContext.Traiders.Where(x => x.Status == "Orange");
        }

        public IQueryable<Traider> GetBlackZoneTraiders()
        {
            return _dataContext.Traiders.Where(x => x.Status == "Black");
        }

        public void ProcessTrade(Traider seller, Traider buyer, Stock stock)
        {
            Withdraw(seller, stock);
            Acquire(buyer, stock);
            RegisterOperation(buyer, seller, stock);
            UpdateTraider(buyer);
            UpdateTraider(seller);
        }

        public void Withdraw(Traider traider, Stock stock)
        {
            var _stock = traider.Stocks.Single(x => x.StockType == stock.StockType);
            _stock.Quantity -= stock.Quantity;
            traider.Balance += stock.StockType.Price * stock.Quantity;
            UpdateStock(_stock);
        }

        public void Acquire(Traider traider, Stock stock)
        {
            if (traider.Stocks.Select(x => x.StockType).Contains(stock.StockType))
            {
                var _stock = traider.Stocks.Single(x => x.StockType == stock.StockType);
                _stock.Quantity += stock.Quantity;
            }
            else
            {
                traider.Stocks.Add(stock);
            }
            traider.Balance -= stock.StockType.Price * stock.Quantity;
        }
    }
}