using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using ORM.Core;
using ORM.Core.Model;
using ORM.Core.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ORM.Test
{
    [TestClass]
    public class ORMTesting
    {
        ILoggerService loggerService;
        IRepository repository;
        BusinessService businessService;

        [TestInitialize]
        public void TestSetup()
        {
            this.loggerService = Substitute.For<ILoggerService>();
            this.loggerService.When(l => l.RunWithExceptionLogging(Arg.Any<Action>())).Do(info => info.Arg<Action>().Invoke());
            this.repository = Substitute.For<IRepository>();
            this.businessService = new BusinessService(repository, loggerService);
        }

        [TestMethod]
        public void ShouldGetTraiders()
        {
            var traders = new List<Trader> { new Trader(), new Trader() }.AsQueryable<Trader>();
            repository.Traders.Returns(traders);

            Assert.AreEqual(repository.Traders.Count(), 2);
        }

        [TestMethod]
        public void ShouldCallAddTraderAndSaveChanges()
        {
            businessService.AddTrader("John", "Smith", "(241) 498-7604", 1000M);

            Received.InOrder(() =>
            {
                repository.Received(1).Add(Arg.Any<Trader>());
                repository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldCallAddListingAndSaveChanges()
        {
            businessService.AddListing("Test Inc.", 100M);

            Received.InOrder(() =>
            {
                repository.Received(1).Add(Arg.Any<Listing>());
                repository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldCallAddShareAndSaveChanges()
        {
            var trader = new Trader();
            var listing = new Listing();

            businessService.AddShare(listing, trader);

            Received.InOrder(() =>
            {
                repository.Received(1).Add(Arg.Any<Share>());
                repository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldBeErrorWhenBuyerIsSeller()
        {
            var trader = new Trader();

            businessService.MakeDeal(trader, trader, null);

            loggerService.Received().Error(Arg.Any<ArgumentException>());
            repository.DidNotReceive().SaveChanges();
        }

        [TestMethod]
        public void ShouldNotBeAbleToBuyWithNegativeBalance()
        {
            var buyer = new Trader { Balance = -100M, };
            var seller = new Trader();

            businessService.MakeDeal(buyer, seller, null);

            loggerService.Received().Error(Arg.Any<ArgumentException>());
            repository.DidNotReceive().SaveChanges();
        }

        [TestMethod]
        public void ShouldNotBeAbleToSellShareNotFromPortfolio()
        {
            var buyer = new Trader();
            var seller = new Trader();
            var share = new Share();

            businessService.MakeDeal(buyer, seller, share);

            loggerService.Received().Error(Arg.Any<ArgumentException>());
            repository.DidNotReceive().SaveChanges();
        }

        [TestMethod]
        public void ShouldMakeDealAndReassignZones()
        {
            var buyer = new Trader
            {
                FirstName = "Jalen",
                SecondName = "Good",
                PhoneNumber = "(222) 730-9441",
                Balance = 90M,
            };
            var seller = new Trader
            {
                FirstName = "Titus",
                SecondName = "Patel",
                PhoneNumber = "(940) 342-2889",
                Balance = -10M,
            };
            var listing = new Listing
            {
                Name = "Test Inc.",
                Price = 100M,
            };
            var share = new Share
            {
                Listing = listing,
                Owner = seller,
            };
            buyer.AssignZone();
            seller.AssignZone();
            seller.Portfolio.Add(share);

            businessService.MakeDeal(buyer, seller, share);

            Assert.AreEqual(seller.Portfolio.Contains(share), false);
            Assert.AreEqual(buyer.Portfolio.Contains(share), true);
            Assert.AreEqual(share.Owner, buyer);
            Assert.AreEqual(buyer.Balance, 90 - share.Listing.Price);
            Assert.AreEqual(seller.Balance, -10 + share.Listing.Price);
            Assert.AreEqual(seller.Zone, Zone.Green);
            Assert.AreEqual(buyer.Zone, Zone.Black);
            Received.InOrder(() =>
            {
                repository.Received(1).Add(Arg.Any<Deal>());
                repository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldReturnTradersInOrangeZone()
        {
            var traders = new List<Trader>
            {
                new Trader
                {
                    FirstName = "Jalen",
                    SecondName = "Good",
                    PhoneNumber = "(222) 730-9441",
                    Balance = 10000M,
                },
                new Trader
                {
                    FirstName = "Titus",
                    SecondName = "Patel",
                    PhoneNumber = "(940) 342-2889",
                    Balance = -10M,
                },
                new Trader
                {
                    FirstName = "Mercedes",
                    SecondName = "Wheeler",
                    PhoneNumber = "(521) 146-6336",
                    Balance = 0M,
                },
                new Trader
                {
                    FirstName = "Allan",
                    SecondName = "Robbins",
                    PhoneNumber = "(865) 647-8174",
                    Balance = -1M,
                },
                new Trader
                {
                    FirstName = "Izabella",
                    SecondName = "Jefferson",
                    PhoneNumber = "(744) 905-6715",
                    Balance = 0M,
                },
            }.AsQueryable<Trader>();

            foreach (var trader in traders)
                trader.AssignZone();

            repository.Traders.Returns(traders);

            Assert.AreEqual(businessService.GetTraidersInOrangeZone().Count(), 2);
        }
    }
}
 