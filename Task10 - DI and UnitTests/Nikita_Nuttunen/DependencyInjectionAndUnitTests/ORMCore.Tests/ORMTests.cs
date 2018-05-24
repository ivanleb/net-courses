using System;
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
        ILoggerService loggerService;

        BusinessService businessService;


        [TestInitialize]
        public void TestSetup()
        {
            dataContext = Substitute.For<IDataContext>();
            loggerService = Substitute.For<ILoggerService>();
            businessService = new BusinessService(dataContext);            
        }

        [TestMethod]
        public void CanRegisterNewClient()
        {
            var client = new Client { Name = "Bill", Surname = "Black", Balance = 200000, PhoneNumber = "89111113321", Area = "Green" };

            businessService.RegisterNewClient(client);
            Received.InOrder(() =>
            {
                dataContext.Received(1).Add(Arg.Any<Client>());
                dataContext.Received(1).SaveChanges();
            });
        }

        /*[TestMethod]
        public void CanChangeStockType()
        {
            var stock = new Stock() { Id = 1, Type = "Gazprom" };

            businessService.RegisterNewStock(stock);
            businessService.ChangeStockType(stock, "Tesla");

            Assert.AreEqual("Tesla", stock.Id);
        }

        [TestMethod]
        public void CanGetUserById()
        {
            var client = new Client { Id = 1, Name = "Bill", Surname = "Black", Balance = 200000, PhoneNumber = "89111113321", Area = "Green" };

            businessService.RegisterNewClient(client);
            var id = businessService.GetClientById(1);

            Assert.AreEqual(1, id);
        }*/


    }
}
