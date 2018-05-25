using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SESimulator.Core;
using SESimulator.Abstractions;
using SESimulator.Model;
using System.Collections.Generic;
using System.Linq;

namespace SESimulatorCore.Tests
{
    [TestClass]
    public class BussinesServiceTests
    {
        BussinesService bussinesService;
        IDataContext dataContext;

        [TestInitialize]
        public void TestSetup()
        {
            this.dataContext = Substitute.For<IDataContext>();
            this.bussinesService = new BussinesService(this.dataContext);
        }

        [TestMethod]
        public void CanRegisterNewClient()
        {
            this.bussinesService.RegisterNewClient("FirstName", "SecondName", "PhoneNumber", 1);
            Received.InOrder(() =>
            {
                this.dataContext.Add(Arg.Any<Client>());
                this.dataContext.SaveChanges();
            });
        }

        [TestMethod]
        public void ClientCanMakeDeal()
        {
            Client testSeller = new Client() { Stocks = new HashSet<Stock>() };
            Client testBuyer = new Client() { Stocks = new HashSet<Stock>() };
            Stock s = new Stock() { IsForSale = true };
            testSeller.Stocks.Add(s);
            this.bussinesService.RegisterNewDeal(testSeller, testBuyer, s, 100);
            Assert.IsTrue(testBuyer.Stocks.Contains(s));
            Assert.IsFalse(testSeller.Stocks.Contains(s));
        }

        [TestMethod]
        public void DealShouldBeSavedInDatabase()
        {
            Client testSeller = new Client() { Stocks = new HashSet<Stock>() };
            Client testBuyer = new Client() { Stocks = new HashSet<Stock>() };
            Stock s = new Stock() { IsForSale = true };
            testSeller.Stocks.Add(s);
            this.bussinesService.RegisterNewDeal(testSeller, testBuyer, s, 100);
            Received.InOrder(() =>
            {
                this.dataContext.Add(Arg.Any<Deal>());
                this.dataContext.SaveChanges();
            });
        }

        [TestMethod]
        public void BalanceShouldBeChangedAfterDeal()
        {
            Client testSeller = new Client() { Stocks = new HashSet<Stock>(), Balance = 1000 };
            Client testBuyer = new Client() { Stocks = new HashSet<Stock>(), Balance = 1000 };
            Stock s = new Stock() { IsForSale = true };
            testSeller.Stocks.Add(s);
            this.bussinesService.RegisterNewDeal(testSeller, testBuyer, s, 2000);
            Assert.AreEqual(testSeller.Balance, 3000);
            Assert.AreEqual(testBuyer.Balance, -1000);
        }

        [TestMethod]
        public void ZoneShouldBeChangedInAccordanceWithBalance()
        {
            Client testClient1 = new Client() { Stocks = new HashSet<Stock>(), Balance = 1000 };
            Client testClient2 = new Client() { Stocks = new HashSet<Stock>(), Balance = 1000 };
            Stock s = new Stock() { IsForSale = true };
            testClient1.Stocks.Add(s);
            this.bussinesService.RegisterNewDeal(testClient1, testClient2, s, 1000);
            Assert.AreEqual(testClient2.Zone, "Orange");

            this.bussinesService.RegisterNewDeal(testClient2, testClient1, s, 3000);
            Assert.AreEqual(testClient1.Zone, "Black");
        }

        [TestMethod]
        public void CanRegisterStock()
        {
            Client testClient = new Client() { Stocks = new HashSet<Stock>()};
            this.bussinesService.RegisterNewStockToClient("NewValidStockType", testClient);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}
