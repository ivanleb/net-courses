using System;
using NSubstitute;
using ORM.Core;
using ORM.Core.Model;
using ORM.Core.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ORM.Test
{
    [TestClass]
    public class ORMTesting
    {
        ILoggerService loggerService;
        IRepository repository;
        BusinessService businessService;

        [TestInitialize]
        public void TestSetup()
        {
            this.loggerService = Substitute.For<ILoggerService>();
            this.repository = Substitute.For<IRepository>();
            this.businessService = new BusinessService(repository, loggerService);
        }

        [TestMethod]
        public void ShouldCallAddClientAndSaveChanges()
        {
            businessService.AddTraider("John", "Smith", "(241) 498-7604", 1000M);

            var ExpectedTraderObject = new Trader
            {
                FirstName = "John",
                SecondName = "Smith",
                PhoneNumber = "(241) 498-7604",
                Balance = 1000M,
            };
            Received.InOrder(() =>
            {
                repository.Received(1).Add(Arg.Any<Trader>());
                repository.Received(1).SaveChanges();
            });
        }
    }
}
