using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORMCore;
using ORMCore.Repositories;
using ORMCore.Model;
using System.Linq;
using NSubstitute;
using System.Collections.Generic;

namespace ORMTests
{
    [TestClass]
    public class ORMTests
    {
        IModelRepository dataContextRepository;

        BuisnessService buisnessService;

        [TestInitialize]
        public void TestSetup()
        {
            this.dataContextRepository = Substitute.For<IModelRepository>();

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

        [TestMethod]
        public void ShoudReturnClients()
        {
            var clients = new List<Client>()
            {
                new Client(){Name = "a" },
                new Client(){ Name = "b" },
                new Client(){Name = "c"}
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);

            var clientsFromMethod = buisnessService.GetAllClients();

            Assert.AreEqual(3, clientsFromMethod.Count());
        }

        [TestMethod]
        public void ShoudReturnClientsInOrangeZone()
        {
            var clients = new List<Client>()
            {
                new Client(){Name = "a", ClientZone = Zone.Green },
                new Client(){ Name = "b", ClientZone = Zone.Orange },
                new Client(){Name = "c", ClientZone = Zone.Orange }
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);

            var clientsInOrangeZone = buisnessService.GetClientsInOrangeZone();

            Assert.AreEqual(2, clientsInOrangeZone.Count());
        }

        [TestMethod]
        public void ShoudReturnClientsInBlackZone()
        {
            var clients = new List<Client>()
            {
                new Client(){Name = "a", ClientZone = Zone.Green },
                new Client(){ Name = "b", ClientZone = Zone.Orange },
                new Client(){Name = "c", ClientZone = Zone.Black },
                new Client(){Name = "d", ClientZone = Zone.Black}
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);

            var clientsInBlackZone = buisnessService.GetClientsInBlackZone();

            Assert.AreEqual(2, clientsInBlackZone.Count());
        }

        [TestMethod]
        public void ShoudGetStockTypes()
        {
            var stockTypes = new List<StockType>()
            {
                new StockType(){Name = "a"},
                new StockType(){ Name = "b"},
                new StockType(){Name = "c"},
                new StockType(){Name = "d"}
            }.AsQueryable();
            dataContextRepository.StockTypes.Returns(stockTypes);

            var stockTypesFromMethod = buisnessService.GetStockTypes();

            Assert.AreEqual(4, stockTypesFromMethod.Count());
        }

        [TestMethod]
        public void ShoudGetClientStocksById()
        {
            var clients = new List<Client>()
            {
                new Client(){Name = "a", Id = 1},
                new Client(){ Name = "b", Id = 2, ClientStocks = new List<Stock>(){ new Stock(), new Stock(), new Stock() } },
                new Client(){Name = "c", Id = 3},
                new Client(){Name = "d", Id = 4, ClientStocks = new List<Stock>(){ new Stock(), new Stock() } }
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);

            var secondClientStocks = buisnessService.GetClientStocksById(2);
            var fourthClientStocks = buisnessService.GetClientStocksById(4);

            Assert.AreEqual(3, secondClientStocks.Count());
            Assert.AreEqual(2, fourthClientStocks.Count());
        }

        [TestMethod]
        public void ShoudGetDeals()
        {
            var deals = new List<Deal>()
            {
                new Deal(){},
                new Deal(){},
                new Deal(){},
                new Deal(){}
            }.AsQueryable();
            dataContextRepository.Deals.Returns(deals);

            var dealsFromMethod = buisnessService.GetDeals();

            Assert.AreEqual(4, dealsFromMethod.Count());
        }
    }
}
