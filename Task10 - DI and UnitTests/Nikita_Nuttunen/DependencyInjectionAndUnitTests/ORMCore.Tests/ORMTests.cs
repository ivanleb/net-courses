using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ORMCore.Abstractions;
using ORMCore.Entities;

namespace ORMCore.Tests
{
    [TestClass]
    public class ORMTests
    {
        IDataContext dataContext;
        BusinessService businessService;
        Stock stock;
        Client client;

        [TestInitialize]
        public void TestSetup()
        {
            dataContext = Substitute.For<IDataContext>();        
            businessService = new BusinessService(dataContext);
            stock = new Stock() { Id = 1, Type = "Gazprom" };
            client = new Client();            
        }

        [TestMethod]
        public void CanChangeStockType()
        {            
            businessService.ChangeStockType(stock, "Tesla");

            Assert.AreEqual(stock.Type, "Tesla");
        }

        [TestMethod]
        public void CanGetStockById()
        {
            var stock2 = new Stock() { Id = 2, Type = "Tesla" };
            var stocks = new List<Stock>() { stock, stock2 }.AsQueryable();

            dataContext.Stocks.Returns(stocks);

            var recievedStock = businessService.GetStockById(1);

            Assert.AreEqual(stock, recievedStock);
            
        }

        [TestMethod]
        public void CanGetClientById()
        {
            var client1 = new Client() { Id = 1 };
            var clients = new List<Client>() { client1, client }.AsQueryable();

            dataContext.Clients.Returns(clients);
            var recievedClient = businessService.GetClientById(1);

            Assert.AreEqual(client1, recievedClient);
        }

        [TestMethod]
        public void CanGetClientsAmount()
        {
            var clients = new List<Client>() { new Client(), new Client() }.AsQueryable();
            dataContext.Clients.Returns(clients);
            Assert.AreEqual(2, businessService.GetClientsAmount());
        }

        [TestMethod]
        public void CanGetClientsFromOrangeArea()
        {
            var clients = new List<Client>()
            {
                new Client() { Id = 1, Area = "orange" },
                new Client() { Id = 2, Area = "black" },
                new Client() { Id = 3, Area = "orange" }
            }.AsQueryable();
            dataContext.Clients.Returns(clients);
            var orangeClients = businessService.GetClientsFromOrangeArea();

            Assert.AreEqual(2, orangeClients.Count());
            Assert.AreEqual(1, orangeClients.ElementAt(0).Id);
            Assert.AreEqual(3, orangeClients.ElementAt(1).Id);
        }

        [TestMethod]
        public void CanRegisterNewClient()
        {
            businessService.RegisterNewClient(client);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanRegisterNewStock()
        {
            businessService.RegisterNewStock(stock);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Stock>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanRegisterNewDeal()
        {
            var deal = new Deal();

            businessService.RegisterNewDeal(deal);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Deal>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanGetClients()
        {
            var clients = new List<Client> { client, new Client() }.AsQueryable();
            dataContext.Clients.Returns(clients);

            Assert.AreEqual(2, dataContext.Clients.Count());
            Assert.IsTrue(dataContext.Clients.Contains(client));
        }

        [TestMethod]
        public void CanMakeDeals()
        {
            var stock = new Stock() { Type = "Gazprom" }; // costs 1000
            client.Balance = 0;
            client.Stocks = new List<Stock>() { stock };
            var purchaser = new Client() { Balance = 1000, Stocks = new List<Stock>() };

            businessService.MakeDeal(client, purchaser, stock);

            Assert.AreEqual(1000, client.Balance);
            Assert.AreEqual(0, purchaser.Balance);
            Assert.AreEqual(stock, purchaser.Stocks.First());
        }
    }
}
