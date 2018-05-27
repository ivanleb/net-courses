using System;
using System.Linq;
using DIAndUnitTests.ConsoleApp;
using DIAndUnitTests.ConsoleApp.Refactoring;
using DIAndUnitTests.Core;
using DIAndUnitTests.Core.Abstractions;
using DIAndUnitTests.Core.Models;
using NUnit.Framework;
using NSubstitute;

namespace DIAndUnitTests.Tests
{
    [TestFixture]
    public class BussinesServiceTests
    {
        private IBusinessService _businessService;
        private IDataContext _dataContext;
        private ILoggerService _loggerService;


        [TestFixtureSetUp]
        public void TestSetup()
        {
            _loggerService = Substitute.For<ILoggerService>();
            _dataContext = Substitute.For<IDataContext>();
            _businessService = new BusinessService(_dataContext, _loggerService);
        }

        [Test]
        public void AddTrader_CorrectTrade_PassTheAddAndSaveChanges()
        {
            //Act
            _businessService.AddTrader("Name", "Surname", 1000, "phoneNumber");
            
            //Assert
            Received.InOrder(() =>
            {
                _dataContext.Add(Arg.Any<Trader>());
                _dataContext.SaveChanges();
            });
        }

        [Test]
        public void AddShare_CorrectShare_PassTheAddAndSavechanged()
        {
            //Arrange
            var trader = new Trader();
            
            //Act
            _businessService.AddShare(trader, "Name", 1000);

            //Assert
            Received.InOrder(() =>
            {
                _dataContext.Received(1).Add(Arg.Any<Share>());
                _dataContext.Received(1).SaveChanges();
            });
        }

        [Test]
        public void AddDeal_CorrectDeal_PassTheAddAndSavechanged()
        {
            //Arrange
            var share = new Share();
            var seller = new Trader() {SharesCollection = {share}};
            var buyer = new Trader();

            //Act
            _businessService.AddDeal(buyer, seller, share);

            //Assert
            Received.InOrder(() =>
            {
                _dataContext.Received(1).Add(Arg.Any<Deal>());
                _dataContext.Received(1).SaveChanges();
            });
        }

        [Test]
        public void AddDeal_CorrectDeal_AreEqualData()
        {
            //Arrange
            var seller = new Trader()
            {
                Name = "NameSeller",
                Surname = "SurnameSeller",
                PhoneNumber = "phoneNumberSeller",
                Balance = 0,
            };

            var share = new Share()
            {
                Name = "TestSare",
                Owner = seller,
                Price = 100
            };
            seller.SharesCollection.Add(share);

            var buyer = new Trader()
            {
                Name = "NameBuyer",
                Surname = "SurnameBuyer",
                PhoneNumber = "phoneNumberBuyer",
                Balance = 80,
            };

            //Act
            _businessService.AddDeal(buyer, seller, share);

            //Assert
            Assert.AreEqual(seller.SharesCollection.Contains(share), false);
            Assert.AreEqual(buyer.SharesCollection.Contains(share), true);
            Assert.AreEqual(TraderService.GetZone(seller), Zone.Green);
            Assert.AreEqual(TraderService.GetZone(buyer), Zone.Black);
            Assert.AreEqual(share.Owner, buyer);
            Assert.AreEqual(buyer.Balance, -20);
            Assert.AreEqual(seller.Balance, 100);
        }

        [Test]
        public void GetOrangeTraders_CorrectData_ReturnThreeTraders()
        {
            //Arrange
            var traders = new[]
            {
                new Trader()
                {
                    Balance = 1000
                },
                new Trader()
                {
                    Balance = 0
                },
                new Trader()
                {
                    Balance = 0
                },
                new Trader()
                {
                    Balance = 0
                },
            }.AsQueryable();

            //Act
            _dataContext.Traders.Returns(traders);
            
            //Assert
            Assert.AreEqual(_businessService.GetOrangeTraders().Count(), 3);
        }
        
        [Test]
        public void GetAllTraders_CorrectData_ReturnFourTraders()
        {
            //Arrange
            var traders = new[]
            {
                new Trader()
                {
                    Balance = 1000
                },
                new Trader()
                {
                    Balance = 0
                },
                new Trader()
                {
                    Balance = 0
                },
                new Trader()
                {
                    Balance = 0
                },
            }.AsQueryable();

            //Act
            _dataContext.Traders.Returns(traders);
            
            //Assert
            Assert.AreEqual(_businessService.GetAllTraders().Count(), 4);
        }
        
        [Test]
        public void AddDeal_IncorrectData_PassTheLoggerErrorAndDontSaveChanges()
        {
            //Arrange
            var buyer = new Trader() {Balance = -10};
            var seller = new Trader();
            var share = new Share();

            //Act
            _businessService.AddDeal(buyer, seller, null);
            
            //Assert
            _loggerService.Received().Error(Arg.Any<ArgumentException>());
            _dataContext.DidNotReceive().SaveChanges();
        }
        
        [Test]
        public void AddDeal_IncorrectData_BuyerLikeSeller()
        {
            //Arrange
            var buyer = new Trader();
            
            //Act
            _businessService.AddDeal(buyer, buyer, null);
            
            //Assert
            _loggerService.Received().Error(Arg.Any<ArgumentException>());
            _dataContext.DidNotReceive().SaveChanges();
        }
    }
}