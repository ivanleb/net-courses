using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ORM.Core.Abstractions;
using ORM.Core.Entities;
using ORM.Core;

namespace ORM.Test
{
    [TestClass]
    public class UnitTest1
    {

        BusinessLogic businessLogic;
        IDataContext dataContextRepository;
        ILoggerService loggerService;

        [TestInitialize]
        public void TestSetup()
        {
            dataContextRepository = Substitute.For<IDataContext>();
            loggerService = Substitute.For<ILoggerService>();
            businessLogic = new BusinessLogic(dataContextRepository, loggerService);
        }

        [TestMethod]
        public void CanRegisterNewClient()
        {
            businessLogic.RegisterNewClient(new Client());

            Received.InOrder(() => 
            {
                dataContextRepository.Received(1).Add(Arg.Any<Client>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanRegisterNewStock()
        {
            businessLogic.RegisterNewStock(new Stock());

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Stock>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanRegisterNewDeal()
        {
            var deal = new Deal();

            businessLogic.RegisterNewDeal(deal);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Deal>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanGetRandomClient()
        {
            var client = businessLogic.GetRandomClient();
        }

        [TestMethod]
        public void CanGetClientAmount()
        {
            var clients = new List<Client>()
            {
                new Client(),
                new Client(),
                new Client()
            }.AsQueryable();

            dataContextRepository.Clients.Returns(clients);
            Assert.AreEqual(3, businessLogic.GetClientsAmount());
        }

        [TestMethod]
        public void CanGetClients()
        {
            var clients = new List<Client>()
            {
                new Client(),
                new Client(),
                new Client()
            }.AsQueryable();

            dataContextRepository.Clients.Returns(clients); 
            Assert.AreEqual(3, businessLogic.GetClients().Count());
        }

        [TestMethod]
        public void CanMakeDeal()
        {
            var stock = new Stock() { Type = StockType.GMKNorNik };
            var seller = new Client { Balance = 0, Stocks = new List<Stock>() { stock } };
            var purhaser = new Client { Balance = 20000, Stocks = new List<Stock>() };

            businessLogic.MakeDeal(seller, purhaser, stock);

            Assert.AreEqual(seller.Balance, stock.Cost);
            Assert.AreEqual(purhaser.Balance, 20000 - stock.Cost);
        }
        
        [TestMethod]
        public void CanGetClientsFromOrangeZone()
        {
            var clients = new List<Client>()
            {
                new Client() { Balance = 0 },
                new Client() { Balance = 100},
                new Client() { Balance = -100 }
            }.AsQueryable();

            dataContextRepository.Clients.Returns(clients);
            Assert.AreEqual(1, businessLogic.GetClientsFromOrangeZone().Count());
        }
    }
}
