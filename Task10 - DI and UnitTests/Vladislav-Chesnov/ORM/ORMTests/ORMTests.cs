using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMCore;
using ORMCore.Repositories;
using ORMCore.Model;
using NSubstitute;

namespace ORMTests
{
    [TestClass]
    public class ORMTests
    {
        IDataContextRepository dataContextRepository;

        BuisnessService buisnessService;

        [TestInitialize]
        public void TestSetup()
        {
            this.dataContextRepository = Substitute.For<IDataContextRepository>();

            this.buisnessService = new BuisnessService(dataContextRepository);
        }

        [TestMethod]
        public void ShouldCallAddClientAndSaveChangesInOrder()
        {
            buisnessService.AddNewClient("Boba", "Biba", "777", 1000);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Client>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldAddStockToClient()
        {
            Client client = new Client();
            StockType stockType = new StockType() { Name = "Test" };

            buisnessService.AddStockToClient(stockType.Name, client);

            Assert.AreEqual(1, client.ClientStocks.Count);
        }

        [TestMethod]
        public void ShouldAddNewStockType()
        {
            buisnessService.AddNewStockType("Test", 10);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<StockType>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldMoveClientToOrangeZoneAfterDeal()
        {
            StockType stockType = new StockType() { Name = "TestType", Cost = 100 };
            Stock stock = new Stock() { Type = stockType };
            Client buyer = new Client() { Name = "Dummy1", Balance = 100 };
            Client seller = new Client() { Name = "Dummy2", Balance = 0 };
            seller.ClientStocks.Add(stock);

            Deal deal = new Deal() { Seller = seller, Buyer = buyer, Stock = stock, Sum = stock.Type.Cost };

            buisnessService.NewDeal(deal);

            Assert.AreEqual(Zone.Orange, buyer.ClientZone);
        }

        [TestMethod]
        public void ShouldMoveClientToBlackZoneAfterDeal()
        {
            StockType stockType = new StockType() { Name = "TestType", Cost = 150 };
            Stock stock = new Stock() { Type = stockType };
            Client buyer = new Client() { Name = "Dummy1", Balance = 100 };
            Client seller = new Client() { Name = "Dummy2", Balance = 0 };
            seller.ClientStocks.Add(stock);

            Deal deal = new Deal() { Seller = seller, Buyer = buyer, Stock = stock, Sum = stock.Type.Cost };

            buisnessService.NewDeal(deal);

            Assert.AreEqual(Zone.Black, buyer.ClientZone);
        }

        [TestMethod]
        public void ShouldChangeClientsBalanceAndStockListsAfterDeal()
        {
            StockType stockType = new StockType() { Name = "TestType", Cost = 100 };
            Stock stock = new Stock() { Type = stockType };
            Client buyer = new Client() { Name = "Dummy1", Balance = 100 };
            Client seller = new Client() { Name = "Dummy2", Balance = 0 };
            seller.ClientStocks.Add(stock);

            Deal deal = new Deal() { Seller = seller, Buyer = buyer, Stock = stock, Sum = stock.Type.Cost };

            buisnessService.NewDeal(deal);

            Assert.AreEqual(0, buyer.Balance);
            Assert.AreEqual(100, seller.Balance);
            Assert.AreEqual(1, buyer.ClientStocks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfStockCantBeSelled()
        {
            StockType stockType = new StockType() { Name = "TestType", Cost = 100 };
            Stock stock = new Stock() { Type = stockType };
            Client buyer = new Client() { Name = "Dummy1", Balance = 100 };
            Client seller = new Client() { Name = "Dummy2", Balance = 0 };

            Deal deal = new Deal() { Seller = seller, Buyer = buyer, Stock = stock, Sum = stock.Type.Cost };

            buisnessService.NewDeal(deal);
        }

        [TestMethod]
        public void ShouldAddAndSaveDeal()
        {
            StockType stockType = new StockType() { Name = "TestType", Cost = 100 };
            Stock stock = new Stock() { Type = stockType };
            Client buyer = new Client() { Name = "Dummy1", Balance = 100 };
            Client seller = new Client() { Name = "Dummy2", Balance = 0 };
            seller.ClientStocks.Add(stock);

            Deal deal = new Deal() { Seller = seller, Buyer = buyer, Stock = stock, Sum = stock.Type.Cost };

            buisnessService.NewDeal(deal);
            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Deal>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }
    }
}
