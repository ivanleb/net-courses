using System;
using System.Collections.Generic;
using System.Linq;
using EF.Core.Abstractions;
using EF.Core.Implementations;
using EF.Core.Services;
using EF.Implementations;
using EF.Implementations.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EF.Tests
{
    [TestClass]
    public class BusinessServiceTests
    {
        private IBusiness BusinessService;
        private IDataContext DataContext;
        private ILogService LogService;

        [TestInitialize]
        public void TestSetup()
        {
            DataContext = Substitute.For<IDataContext>();
            LogService = Substitute.For<ILogService>();
            BusinessService = new BusinessService(DataContext, LogService);
        }

        [TestMethod]
        public void ShouldRegisterTrader()
        {
            var traderEntity = new Trader();
            BusinessService.RegisterEntity(traderEntity);
            Received.InOrder(() =>
            {
                DataContext.Received(1).Add(Arg.Any<IEntity>());
                DataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldRegiterAndLogOperation()
        {
            IEntity operation = new TradeOperation
            {
                Time = DateTime.Now,
                Buyer = new Trader {TraderInfo = new IndividualInfo {SecondName = "BuyerSecondName"}},
                Seller = new Trader {TraderInfo = new IndividualInfo {SecondName = "SellerSecondName"}},
                TradableType = new TradableType {Type = "TradableType"},
                TradeAmount = 10,
                TradableAmount = 11
            };
            BusinessService.RegisterEntity(operation);
            Received.InOrder(() =>
            {
                DataContext.Received(1).Add(Arg.Any<IEntity>());
                DataContext.Received(1).SaveChanges();
                //не смог заставить этот кусок работать
                //operation.Received(1).GetInfo();
                LogService.Received().Info(Arg.Any<string>());
            });
        }

        [TestMethod]
        public void ShouldReturnIQuerybleTraider()
        {
            var traders = new List<Trader>
            {
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()}
            }.AsQueryable();
            DataContext.Traders.Returns(traders);

            var allTraders = BusinessService.GetAllTraiders();

            Assert.AreEqual(allTraders.Count(), 3);
        }

        [TestMethod]
        public void ShouldReturnIQuerybleOperation()
        {
            var operations = new List<TradeOperation>
            {
                new TradeOperation(),
                new TradeOperation(),
                new TradeOperation()
            }.AsQueryable();
            DataContext.TradeOperations.Returns(operations);

            var allTraders = BusinessService.GetAllOperations();

            Assert.AreEqual(allTraders.Count(), 3);
        }

        [TestMethod]
        public void ShouldReturnSingleTraderWithZoneEqualOrange()
        {
            var oTrader = new Trader {Status = "Orange"};
            var _traders = new List<Trader>
            {
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()}
            };
            _traders.Add(oTrader);
            var traders = _traders.AsQueryable();
            DataContext.Traders.Returns(traders);

            var orangeTrader = BusinessService.GetOrangeZoneTraiders().Single();

            Assert.AreEqual(orangeTrader, oTrader);
        }

        [TestMethod]
        public void ShouldReturnTradersWithZOneEqualOrange()
        {
            var traders = new List<Trader>
            {
                new Trader(),
                new Trader(),
                new Trader(),
                new Trader {Status = "Orange"},
                new Trader {Status = "Orange"}
            }.AsQueryable();

            DataContext.Traders.Returns(traders);

            var orangeTrader = BusinessService.GetOrangeZoneTraiders();

            Assert.AreEqual(orangeTrader.Count(), 2);
        }

        [TestMethod]
        public void ShouldReturnSingleTraderWithZoneEqualBlack()
        {
            var bTrader = new Trader {Status = "Black"};
            var _traders = new List<Trader>
            {
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()},
                new Trader {Assets = new List<Stock>()}
            };
            _traders.Add(bTrader);
            var traders = _traders.AsQueryable();
            DataContext.Traders.Returns(traders);

            var black = BusinessService.GetBlackZoneTraiders().Single();

            Assert.AreEqual(black, bTrader);
        }

        [TestMethod]
        public void ShouldReturnTradersWithZoneEqualBlack()
        {
            var traders = new List<Trader>
            {
                new Trader(),
                new Trader(),
                new Trader(),
                new Trader {Status = "Black"},
                new Trader {Status = "Orange"}
            }.AsQueryable();

            DataContext.Traders.Returns(traders);

            var orangeTrader = BusinessService.GetOrangeZoneTraiders();

            Assert.AreEqual(orangeTrader.Count(), 1);
        }

        [TestMethod]
        public void ShouldCallUpdateEntityAndSave()
        {
            var entity = new Trader();

            BusinessService.UpdateEntity(entity);
            Received.InOrder(() =>
            {
                DataContext.Received(1).Update(Arg.Is(entity));
                DataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldRemoveEmptyStockAndSaveChanges()
        {
            var entity = new Stock {Quantity = 0};

            BusinessService.UpdateEntity(entity);
            Received.InOrder(() =>
            {
                DataContext.Received(1).Remove(Arg.Is(entity));
                DataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void DecreaseSellerStockQuantity_IncreaseSellerBalance()
        {
            var tradableType = new TradableType {Price = 1000, Type = "TestType1"};
            var sellerStock = new Stock {Quantity = 100, TradableType = tradableType};
            var stockForSell = new Stock {Quantity = 10, TradableType = tradableType};

            var trader = new Trader
            {
                Balance = 100000,
                Assets = new List<Stock>
                {
                    new Stock(),
                    sellerStock
                }
            };
            BusinessService.Withdraw(trader, stockForSell);
            Received.InOrder(() => { BusinessService.UpdateEntity(sellerStock); });

            Assert.AreEqual(sellerStock.Quantity, 90);
            Assert.AreEqual(trader.Balance, 110000);
        }

        [TestMethod]
        public void IncreaseBuyerStockQty_DecreaseBuyerBalance()
        {
            var tradableType = new TradableType {Price = 1000, Type = "TestType1"};
            var buyerStock = new Stock {Quantity = 100, TradableType = tradableType};
            var stockForBuy = new Stock {Quantity = 10, TradableType = tradableType};

            var trader = new Trader
            {
                Balance = 100000,
                Assets = new List<Stock>
                {
                    new Stock(),
                    buyerStock
                }
            };
            BusinessService.Acquire(trader, stockForBuy);
            Assert.AreEqual(buyerStock.Quantity, 110);
            Assert.AreEqual(trader.Balance, 90000);
        }

        [TestMethod]
        public void AddNewStockTypeToBuyer()
        {
            var tradableType = new TradableType {Price = 1000, Type = "TestType1"};
            var stockForBuy = new Stock {Quantity = 10, TradableType = tradableType};

            var trader = new Trader
            {
                Balance = 100000,
                Assets = new List<Stock>
                {
                    new Stock(),
                    new Stock(),
                    new Stock()
                }
            };
            BusinessService.Acquire(trader, stockForBuy);
            Assert.AreEqual(trader.Assets.Count, 4);
            Assert.AreEqual(trader.Balance, 90000);
            Assert.AreEqual(trader.Assets.Single(x => x.TradableType == tradableType), stockForBuy);
        }

        [TestMethod]
        public void BuyerWithdrawStock_SelsNewOperation()
        {
            var tradableType = new TradableType {Price = 1000, Type = "TestType1"};
            var stock = new Stock {Quantity = 10, TradableType = tradableType};
            var seller = new Trader
            {
                Assets = new List<Stock>
                {
                    new Stock {Quantity = 100, TradableType = tradableType},
                    new Stock()
                },
                TraderInfo = new IndividualInfo {SecondName = "SellerName"},
                Balance = 100000
            };
            var buyer = new Trader
            {
                Assets = new List<Stock>
                {
                    new Stock {Quantity = 100, TradableType = tradableType},
                    new Stock()
                },
                TraderInfo = new IndividualInfo {SecondName = "BuyerName"},
                Balance = 100000
            };

            var trade = BusinessService.ProcessTrade(seller, buyer, stock);
            Received.InOrder(() =>
            {
                BusinessService.Withdraw(seller, stock);
                BusinessService.Acquire(buyer, stock);
                BusinessService.UpdateEntity(buyer);
                BusinessService.UpdateEntity(seller);
            });
            Assert.AreEqual(trade.TradableType, stock.TradableType);
            Assert.AreEqual(trade.TradableAmount, 10);
            Assert.AreEqual(trade.TradeAmount, 10000);
            Assert.AreEqual(trade.Buyer.TraderInfo.SecondName, buyer.TraderInfo.SecondName);
            Assert.AreEqual(trade.Seller.TraderInfo.SecondName, seller.TraderInfo.SecondName);
        }
    }
}