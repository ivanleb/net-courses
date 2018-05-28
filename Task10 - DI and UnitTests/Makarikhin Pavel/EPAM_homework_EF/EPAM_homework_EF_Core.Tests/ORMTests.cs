using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EPAM_homework_EF_Core.Tests
{
    [TestClass]
    public class ORMTests
    {
        IDataContext dataContext;
        BussinesService bussinesService;
        Client client1, client2;

        [TestInitialize]
        public void Initialize()
        {
            dataContext = Substitute.For<IDataContext>();

            var Shares = new List<Share>()
            {
                new Share() { ShareId = 0, ShareName = "abc", ShareCost = 20},
                new Share() {ShareId = 1, ShareName = "def", ShareCost = 35}
            };

            dataContext.Shares.Returns(Shares.AsQueryable());

            client1 = new Client()
            {
                Id = 0,
                FirstName = "TestDroid",
                LastName = "Test",
                Balance = 250,
                Number = "128476617285",
                Zone = "White"
            };

            client2 = new Client()
            {
                Id = 1,
                FirstName = "abc",
                LastName = "dfe",
                Balance = 250,
                Number = "813516515",
                Zone = "Black"
            };

            var WhiteZoneClients = new List<WhiteZoneClient>()
            {
                new WhiteZoneClient() {ClientId = client1.Id},
                new WhiteZoneClient() {ClientId = client2.Id},
            };

            dataContext.WhiteZoneClients.Returns(WhiteZoneClients.AsQueryable());

            var OrangeZoneClients = new List<OrangeZoneClient>()
            {
                new OrangeZoneClient() {ClientId = 2, Timeout = 25}
            };

            dataContext.OrangeZoneClients.Returns(OrangeZoneClients.AsQueryable());

            var BlackZoneClients = new List<BlackZoneClient>()
            {
                new BlackZoneClient() {ClientId = 1, Penalty = 150}
            };

            dataContext.BlackZoneClients.Returns(BlackZoneClients.AsQueryable());

            var ClientShares = new List<ClientShare>()
            {
                new ClientShare(){ClientId = 0, ShareId = 0, Amount = 35},
                new ClientShare(){ClientId = 0, ShareId = 1, Amount = 25},
                new ClientShare(){ClientId = 1, ShareId = 0, Amount = 10},
                new ClientShare(){ClientId = 1, ShareId = 1, Amount = 15},
            };

            dataContext.ClientsShares.Returns(ClientShares.AsQueryable());

            bussinesService = Substitute.For<BussinesService>(dataContext, Substitute.For<ILoggerService>());
        }

        [TestMethod]
        public void CanRegisterClient()
        {
            bussinesService.RegisterNewClient(client1.FirstName, client1.LastName, client1.Number, client1.Balance);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Add(Arg.Any<WhiteZoneClient>());
                dataContext.Received(1).SaveChanges();
            });
        }
        [TestMethod]
        public void CanRegisterShare()
        {
            bussinesService.RegisterShare("abc");

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Share>());
                dataContext.Received(1).SaveChanges();
            });
        }
        [TestMethod]
        public void CanRegisterClientShares()
        {
            bussinesService.RegisterClientShares(client1);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<ClientShare>());
                dataContext.Received(1).Add(Arg.Any<ClientShare>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanAddClientToWhiteZone()
        {
            bussinesService.AddClientToWhiteZone(client1);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Update(client1);
                dataContext.Received(1).Add(Arg.Any<WhiteZoneClient>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanAddClientToOrangeZone()
        {
            bussinesService.AddClientToOrangeZone(client1);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Update(client1);
                dataContext.Received(1).Add(Arg.Any<OrangeZoneClient>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanAddClientToBlackZone()
        {
            bussinesService.AddClientToBlackZone(client1);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Update(client1);
                dataContext.Received(1).Add(Arg.Any<BlackZoneClient>());
                dataContext.Received(1).SaveChanges();
            });
        }

        [TestMethod]
        public void CanDeleteClientFromZone()
        {
            bussinesService.DeleteClientFromZone(client1);
            bussinesService.DeleteClientFromZone(client2);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Remove(Arg.Any<WhiteZoneClient>());
                dataContext.Received(1).Remove(Arg.Any<BlackZoneClient>());
            });
        }

        [TestMethod]
        public void CanCreateDeal()
        {
            bussinesService.CreateDeal(client1, client2);

            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<DealHistory>());
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).Update(Arg.Any<ClientShare>());
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).Update(Arg.Any<ClientShare>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Add(Arg.Any<DealHistory>());
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).Update(Arg.Any<ClientShare>());
                dataContext.Received(1).Update(Arg.Any<Client>());
                dataContext.Received(1).Update(Arg.Any<ClientShare>());
                dataContext.Received(1).SaveChanges();
            });
        }
    }
}
