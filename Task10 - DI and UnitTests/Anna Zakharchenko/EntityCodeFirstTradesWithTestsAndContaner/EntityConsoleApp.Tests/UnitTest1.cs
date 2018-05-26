using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityCore;
using NSubstitute;
using EntityCore.Abstractions;
using EntityCore.Model;
using System.Collections.Generic;
using System.Linq;

namespace EntityConsoleApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        IDataContextRepository dataContextRepository;
        BussinesService bussinesService;
        ILoggerService loggerService;

        [TestInitialize]
        public void TestSetup()
        {
            dataContextRepository = Substitute.For<IDataContextRepository>();
            loggerService = Substitute.For<ILoggerService>();
            bussinesService = new BussinesService(dataContextRepository, loggerService);
        }

        [TestMethod]
        public void ShouldMakeTrade()
        {
            Stock stock = new Stock()
            {
                NameTypeOfStock = "Pollyanna",
                Cost = 1300
            };
            Client seller = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000
            };
            Client buyer = new Client
            {
                FirstName = "Mary",
                LastName = "Poppins",
                Balance = 300
            };
            seller.Stocks.Add(stock);

            Trade trade = bussinesService.GetNewTrade(seller, buyer, stock);

            Assert.AreEqual(2300, seller.Balance);
            Assert.AreEqual(-1000, buyer.Balance);
        }

        [TestMethod]
        public void ShouilRegisterNewTrade()
        {
            Stock stock = new Stock()
            {
                NameTypeOfStock = "Pollyanna",
                Cost = 1300
            };
            Client seller = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = -1000
            };
            Client buyer = new Client
            {
                FirstName = "Mary",
                LastName = "Poppins",
                Balance = 1300
            };
            seller.Stocks.Add(stock);
            Trade trade = bussinesService.GetNewTrade(seller, buyer, stock);

            bussinesService.RegisterNewTrade(trade);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Trade>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldChangeZoneForClient()
        {
            Client seller = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = -1000
            };
            Assert.AreEqual(ClientZoneOfBalance.Black, seller.Zone);

            seller.Balance = 0;
            Assert.AreEqual(ClientZoneOfBalance.Orange, seller.Zone);

            seller.Balance = 29393;
            Assert.AreEqual(ClientZoneOfBalance.Green, seller.Zone);
        }

        [TestMethod]
        public void ShouldRegisterStock()
        {
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899"
            };
            KeyValuePair<string, decimal> typeStock = new KeyValuePair<string, decimal>( "Yandex", 21918 );

            bussinesService.RegisterNewStock(client, typeStock);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Stock>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldRegisterClient()
        {
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899"
            };
            bussinesService.RegisterNewClient("Moon", "Pilot", "86409876", 56567);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Client>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldReturnCliensInOrangeZone()
        {
            IQueryable clients = new List<Client>()
            {
                new Client(){FirstName="Abu",LastName="Daby", Balance=0,PhoneNumber="743843" },
                new Client(){FirstName="Nujy",LastName="fros",Balance=7474,PhoneNumber="7373833"},
                new Client(){FirstName="Huba",LastName="Buba", Balance=0,PhoneNumber="78899"}
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);
            IQueryable<Client> recivedClients = bussinesService.GetClientsFromOrangeZone();

            Assert.AreEqual(2, recivedClients.Count());
        }

        [TestMethod]
        public void ShouldGetSellerStockWithotStocks()
        {
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899"
            };
            Stock stock = bussinesService.GetSellerStock(client);

            Assert.AreEqual(null, stock);
        }

        [TestMethod]
        public void ShouldGetRandomSellerStock()
        {
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899"
            };
            Stock stock = new Stock()
            {
                NameTypeOfStock = "Pollyanna",
                Cost = 1300
            };
            client.Stocks.Add(stock);
            Stock stock1 = new Stock()
            {
                NameTypeOfStock = "Lazurit",
                Cost = 1460
            };
            client.Stocks.Add(stock1);
            Stock stock2 = new Stock()
            {
                NameTypeOfStock = "Lerom",
                Cost = 393
            };
            client.Stocks.Add(stock2);
            Stock stock3 = new Stock()
            {
                NameTypeOfStock = "WoodHoff",
                Cost = 7549
            };
            client.Stocks.Add(stock3);

            Stock stockFromMethod = bussinesService.GetSellerStock(client);

            Received.InOrder(() => dataContextRepository.Received(1).Add(Arg.Any<Stock>()));
        }

        [TestMethod]
        public void SouldReturnClient()
        {
            IQueryable clients = new List<Client>()
            {
                new Client(){FirstName="Abu",LastName="Daby", Balance=0,PhoneNumber="743843" ,Id=0},
                new Client(){FirstName="Nujy",LastName="fros",Balance=7474,PhoneNumber="7373833",Id=1},
                new Client(){FirstName="Huba",LastName="Buba", Balance=0,PhoneNumber="78899",Id=2}
            }.AsQueryable();
            dataContextRepository.Clients.Returns(clients);

            Client client = bussinesService.GetClient(1);

            Assert.AreEqual("fros", client.LastName);
        }
    }
}
