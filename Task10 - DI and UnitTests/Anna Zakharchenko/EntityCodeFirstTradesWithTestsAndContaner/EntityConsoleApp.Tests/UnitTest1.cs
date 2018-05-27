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
                NameTypeOfStock = "DIXY Group",
                Cost = 1300
            };
            Client seller = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899",
                Zone = ClientZoneOfBalance.Green,
                Stocks = new List<Stock>() { stock}
            };
            Client buyer = new Client
            {
                FirstName = "Mary",
                LastName = "Poppins",
                Balance = 300,
                PhoneNumber = "765899",
                Zone = ClientZoneOfBalance.Green,
                Stocks = new List<Stock>()
            };
            
            Trade trade = bussinesService.GetNewTrade(seller, buyer, stock);

            Assert.AreEqual(2300, seller.Balance);
            Assert.AreEqual(-1000, buyer.Balance);
            Assert.AreEqual(ClientZoneOfBalance.Black, buyer.Zone);
            Assert.AreEqual(ClientZoneOfBalance.Green, seller.Zone);
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
                Balance = -1000,
                PhoneNumber = "765899",
                Zone = ClientZoneOfBalance.Black,
                Stocks = new List<Stock>() { stock}
            };
            Client buyer = new Client
            {
                FirstName = "Mary",
                LastName = "Poppins",
                Balance = 1300,
                Zone = ClientZoneOfBalance.Green,
                Stocks = new List<Stock>() { stock }
            };

            Trade trade = new Trade() { Seller=seller,Buyer=buyer,StockFromSeller=stock};

            bussinesService.RegisterNewTrade(trade);

            Received.InOrder(() =>
            {
                dataContextRepository.Received(1).Add(Arg.Any<Trade>());
                dataContextRepository.Received(2).Update(Arg.Any<Client>());
                dataContextRepository.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void ShouldRegisterStock()
        {
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899",
                Zone=ClientZoneOfBalance.Green,
                Stocks = new List<Stock>()
            };
            Dictionary<string, decimal> allStocksType = new Dictionary<string, decimal>
            {
            {"Bul", 3800},
            {"Bosch", 2000}
            };

            foreach (var stock in allStocksType)
            {
                bussinesService.RegisterNewStock(client, stock);
            }

            Assert.AreEqual(2, client.Stocks.Count());           
        }

        [TestMethod]
        public void ShouldRegisterClient()
        {
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
                new Client(){FirstName="Abu",LastName="Daby", Balance=0,PhoneNumber="743843",Zone=ClientZoneOfBalance.Orange },
                new Client(){FirstName="Nujy",LastName="fros",Balance=7474,PhoneNumber="7373833",Zone=ClientZoneOfBalance.Green},
                new Client(){FirstName="Huba",LastName="Buba", Balance=0,PhoneNumber="78899",Zone=ClientZoneOfBalance.Orange}
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
                PhoneNumber = "765899",
                Stocks = new List<Stock>()
            };
            Stock stock = bussinesService.GetSellerStock(client);

            Assert.AreEqual(null, stock);
        }

        [TestMethod]
        public void ShouldGetRandomSellerStock()
        {
            Stock stock = new Stock()
            {
                NameTypeOfStock = "Pollyanna",
                Cost = 1300
            };
           
            Client client = new Client()
            {
                FirstName = "Moon",
                LastName = "Pilot",
                Balance = 1000,
                PhoneNumber = "765899",
                Stocks = new List<Stock>() { stock }
            }; 

            Stock stockFromMethod = bussinesService.GetSellerStock(client);
            Assert.AreEqual("Pollyanna", stockFromMethod.NameTypeOfStock);
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
