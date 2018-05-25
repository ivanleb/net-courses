using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMCore;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace BussinesService.Tests
{
    [TestClass]
    public class BussinesServiceTests
    {
        ORMCore.BussinesService bussinesService;
        ORMCore.IDataContext dataContext;

        [TestInitialize]
        public void TestSetup()
        {
            this.dataContext = Substitute.For<IDataContext>();

            this.bussinesService = new ORMCore.BussinesService(this.dataContext);
        }

        [TestMethod]
        public void Should_RegisterNewShareholderWithStartingBalance()
        {
            this.bussinesService.RegisterNewShareholderWithStartingBalance(new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            });

            NSubstitute.Received.InOrder(() =>
            {
                this.dataContext.Add(Arg.Any<Shareholder>());
                this.dataContext.SaveChanges();
            });
        }

        [TestMethod]
        public void Should_RegisterNewBalance()
        {
            this.bussinesService.RegisterNewBalance(
                shareholderId: 1,
                firstType: 1,
                secondType: 1,
                thirdType: 1,
                balanceValue: 1,
                balanceZone: "low");

            NSubstitute.Received.InOrder(() =>
            {
                this.dataContext.Add(Arg.Any<Balance>());
                this.dataContext.SaveChanges();
            });
        }

        [TestMethod]
        public void Should_GetMostWantedShareholdersById()
        {
            var shareholder = new Shareholder { Id = 1 };

            var shareholders = new List<Shareholder>()
            {
                new Shareholder{Id = 1},
                new Shareholder{Id = 2},
                new Shareholder{Id = 3}
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);

            var takenShareholder = this.bussinesService.GetMostWantedShareholdersById(shareholder.Id);

            Assert.AreEqual(shareholder.Id, takenShareholder.Id);
        }

        [TestMethod]
        public void Should_RegisterNewTradeAndChangeBalancesOfShareholders()
        {
            var shareholder = new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var buyer = new Shareholder
            {
                Id = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var shareholders = new List<Shareholder>()
            {
                shareholder,
                buyer,
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);

            var shareholderBalance = new Balance
            {
                Id = shareholder.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var buyerBalance = new Balance
            {
                Id = buyer.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var balances = new List<Balance>()
            {
                shareholderBalance,
                buyerBalance
            }.AsQueryable();

            this.dataContext.Balances.Returns(balances);

            var trade = new Trade
            {
                Id = 1,
                BuyerId = 1,
                ShareholderId = 2,
                Value = 1,
                ValueType = SharesTypes.FirstType
            };

            this.bussinesService.RegisterNewTrade(
                trade: trade,
                shareholder: shareholder,
                buyer: buyer);

            NSubstitute.Received.InOrder(() =>
            {
                this.dataContext.Add(trade);
                this.dataContext.SaveChanges();
            });

            Assert.AreEqual(0,this.dataContext.Balances.FirstOrDefault().FirstType);

        }

        [TestMethod]
        public void Should_GetShareholdersWithZeroBalance()
        {
            var shareholders = new List<Shareholder>()
            {
                new Shareholder{Id = 1},
                new Shareholder{Id = 2},
                new Shareholder{Id = 3},
                new Shareholder{Id = 4}
            }.AsQueryable();
            this.dataContext.Shareholders.Returns(shareholders);

            var balances = new List<Balance>
            {
                new Balance {Id = 1, BalanceValue = 0},
                new Balance {Id = 2, BalanceValue = 0},
                new Balance {Id = 3, BalanceValue = 1000},
                new Balance {Id = 4, BalanceValue = -1000}
            }.AsQueryable();
            this.dataContext.Balances.Returns(balances);

            foreach (var shareholder in this.bussinesService.GetShareholdersWithZeroBalance())
            {
                var balance = this.dataContext.Balances.SingleOrDefault(w => w.Id == shareholder.Id).BalanceValue;
                Assert.AreEqual(0, balance);
            }
        }

        [TestMethod]
        public void Should_CompletePreparationChangesBeforeSavingTrading()
        {
            #region shareholders
            var shareholder = new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var buyer = new Shareholder
            {
                Id = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var shareholders = new List<Shareholder>()
            {
                shareholder,
                buyer,
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);
            #endregion
            #region balances
            var shareholderBalance = new Balance
            {
                Id = shareholder.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var buyerBalance = new Balance
            {
                Id = buyer.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var balances = new List<Balance>()
            {
                shareholderBalance,
                buyerBalance
            }.AsQueryable();

            this.dataContext.Balances.Returns(balances);
            #endregion
            var trade = new Trade
            {
                Id = 1,
                BuyerId = 1,
                ShareholderId = 2,
                Value = 1,
                ValueType = SharesTypes.FirstType
            };

            var sharesTypes = new List<SharesTypes>()
            {
                SharesTypes.FirstType,
                SharesTypes.SecondType,
                SharesTypes.ThirdType
            };

            foreach (var type in sharesTypes)
            {
                this.bussinesService.PreparationsForUpdatingBalances(shareholderBalance, buyerBalance, type, new Trade
                {
                    ShareholderId = shareholder.Id,
                    BuyerId = buyer.Id,
                    ValueType = type,
                    Value = 1
                });
            }

            //FirstType = 15, SecondType = 30, ThirdType = 300
            //balance start 1000
            //shares 1
            //
            Assert.AreEqual(0, shareholderBalance.FirstType);
            Assert.AreEqual(0, shareholderBalance.SecondType);
            Assert.AreEqual(0, shareholderBalance.ThirdType);

            Assert.AreEqual(1000 + (int)SharesTypes.FirstType + (int)SharesTypes.SecondType + (int)SharesTypes.ThirdType, shareholderBalance.BalanceValue);

            Assert.AreEqual(2, buyerBalance.FirstType);
            Assert.AreEqual(2, buyerBalance.SecondType);
            Assert.AreEqual(2, buyerBalance.ThirdType);

            Assert.AreEqual(1000 - (int)SharesTypes.FirstType - (int)SharesTypes.SecondType - (int)SharesTypes.ThirdType, buyerBalance.BalanceValue);

            //cheking zones
            #region fromUpToDawn
            #region high/black
            shareholderBalance.BalanceValue = -30000;
            buyerBalance.BalanceValue = 30000;
            var countOfShares = 40;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("high", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region middle/black
            countOfShares = 30;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("middle", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region low/black
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("low", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region orange/orange
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("Orange Zone!", buyerBalance.BalanceZone);
            Assert.AreEqual("Orange Zone!", shareholderBalance.BalanceZone);
            #endregion

            #endregion

            #region fromDownToUp

            #region low/black
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("low", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region middle/black
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("middle", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region high/black
            countOfShares = 30;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("high", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region insane/black
            countOfShares = 40;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("insane", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #endregion
        }

        [TestMethod]
        public void Should_ChangingBalanceValues()
        {
            #region shareholders
            var shareholder = new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var buyer = new Shareholder
            {
                Id = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var shareholders = new List<Shareholder>()
            {
                shareholder,
                buyer,
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);
            #endregion
            #region balances
            var shareholderBalance = new Balance
            {
                Id = shareholder.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var buyerBalance = new Balance
            {
                Id = buyer.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var balances = new List<Balance>()
            {
                shareholderBalance,
                buyerBalance
            }.AsQueryable();

            this.dataContext.Balances.Returns(balances);
            #endregion
            var trade = new Trade
            {
                Id = 1,
                BuyerId = 1,
                ShareholderId = 2,
                Value = 1,
                ValueType = SharesTypes.FirstType
            };

            var sharesTypes = new List<SharesTypes>()
            {
                SharesTypes.FirstType,
                SharesTypes.SecondType,
                SharesTypes.ThirdType
            };

            foreach (var type in sharesTypes)
            {
                this.bussinesService.PreparationsForUpdatingBalances(shareholderBalance, buyerBalance, type, new Trade
                {
                    ShareholderId = shareholder.Id,
                    BuyerId = buyer.Id,
                    ValueType = type,
                    Value = 1
                });
            }

            //FirstType = 15, SecondType = 30, ThirdType = 300
            //balance start 1000
            //shares 1
            //
            Assert.AreEqual(0, shareholderBalance.FirstType);
            Assert.AreEqual(0, shareholderBalance.SecondType);
            Assert.AreEqual(0, shareholderBalance.ThirdType);

            Assert.AreEqual(1000 + (int)SharesTypes.FirstType + (int)SharesTypes.SecondType + (int)SharesTypes.ThirdType, shareholderBalance.BalanceValue);

            Assert.AreEqual(2, buyerBalance.FirstType);
            Assert.AreEqual(2, buyerBalance.SecondType);
            Assert.AreEqual(2, buyerBalance.ThirdType);

            Assert.AreEqual(1000 - (int)SharesTypes.FirstType - (int)SharesTypes.SecondType - (int)SharesTypes.ThirdType, buyerBalance.BalanceValue);
        }

        [TestMethod]
        public void Should_ChangingZonesOfShareholderBalances_FromUpToDown()
        {
            #region shareholders
            var shareholder = new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var buyer = new Shareholder
            {
                Id = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var shareholders = new List<Shareholder>()
            {
                shareholder,
                buyer,
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);
            #endregion
            #region balances
            var shareholderBalance = new Balance
            {
                Id = shareholder.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var buyerBalance = new Balance
            {
                Id = buyer.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var balances = new List<Balance>()
            {
                shareholderBalance,
                buyerBalance
            }.AsQueryable();

            this.dataContext.Balances.Returns(balances);
            #endregion
            var trade = new Trade
            {
                Id = 1,
                BuyerId = 1,
                ShareholderId = 2,
                Value = 1,
                ValueType = SharesTypes.FirstType
            };

            var sharesTypes = new List<SharesTypes>()
            {
                SharesTypes.FirstType,
                SharesTypes.SecondType,
                SharesTypes.ThirdType
            };

            foreach (var type in sharesTypes)
            {
                this.bussinesService.PreparationsForUpdatingBalances(shareholderBalance, buyerBalance, type, new Trade
                {
                    ShareholderId = shareholder.Id,
                    BuyerId = buyer.Id,
                    ValueType = type,
                    Value = 1
                });
            }

            //cheking zones
            #region fromUpToDawn
            #region high/black
            shareholderBalance.BalanceValue = -30000;
            buyerBalance.BalanceValue = 30000;
            var countOfShares = 40;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("high", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region middle/black
            countOfShares = 30;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("middle", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region low/black
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("low", buyerBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", shareholderBalance.BalanceZone);
            #endregion

            #region orange/orange
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("Orange Zone!", buyerBalance.BalanceZone);
            Assert.AreEqual("Orange Zone!", shareholderBalance.BalanceZone);
            #endregion

            #endregion
        }

        [TestMethod]
        public void Should_ChangingZonesOfShareholderBalances_FromDawnToUp()
        {
            #region shareholders
            var shareholder = new Shareholder
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var buyer = new Shareholder
            {
                Id = 2,
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "777111777"
            };

            var shareholders = new List<Shareholder>()
            {
                shareholder,
                buyer,
            }.AsQueryable();

            this.dataContext.Shareholders.Returns(shareholders);
            #endregion
            #region balances
            var shareholderBalance = new Balance
            {
                Id = shareholder.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var buyerBalance = new Balance
            {
                Id = buyer.Id,
                FirstType = 1,
                SecondType = 1,
                ThirdType = 1,
                BalanceValue = 1000
            };

            var balances = new List<Balance>()
            {
                shareholderBalance,
                buyerBalance
            }.AsQueryable();

            this.dataContext.Balances.Returns(balances);
            #endregion
            var trade = new Trade
            {
                Id = 1,
                BuyerId = 1,
                ShareholderId = 2,
                Value = 1,
                ValueType = SharesTypes.FirstType
            };

            var sharesTypes = new List<SharesTypes>()
            {
                SharesTypes.FirstType,
                SharesTypes.SecondType,
                SharesTypes.ThirdType
            };

            foreach (var type in sharesTypes)
            {
                this.bussinesService.PreparationsForUpdatingBalances(shareholderBalance, buyerBalance, type, new Trade
                {
                    ShareholderId = shareholder.Id,
                    BuyerId = buyer.Id,
                    ValueType = type,
                    Value = 1
                });
            }

            //cheking zones
            #region fromDownToUp
            shareholderBalance.BalanceValue = 0;
            buyerBalance.BalanceValue = 0;
            #region low/black
            var countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("low", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region middle/black
            countOfShares = 15;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("middle", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region high/black
            countOfShares = 30;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("high", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #region insane/black
            countOfShares = 40;

            this.bussinesService.PreparationsForUpdatingBalances(
                shareholderBalance,
                buyerBalance,
                SharesTypes.ThirdType,
                new Trade
                {
                    Id = 1,
                    ShareholderId = 2,
                    BuyerId = 1,
                    Value = countOfShares,
                    ValueType = SharesTypes.ThirdType
                });

            Assert.AreEqual("insane", shareholderBalance.BalanceZone);
            Assert.AreEqual("Black Zone!", buyerBalance.BalanceZone);
            #endregion

            #endregion
        }
    }
}
