using System;
using ORMDatabaseCore;
using ORMDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq;
using System.Collections.Generic;


namespace ORMDatabaseCore.Test
{
    [TestClass]
    public class BussinesServiceTest
    {
        IDataContex dataContex;
        BussinesService bussinesService;
        Traider testTraider;
        IQueryable balancesList;
        IQueryable traidersList;

        [TestInitialize]
        public void TestSetup()
        {
            this.dataContex = Substitute.For<IDataContex>();
            this.bussinesService = new BussinesService(this.dataContex);
            this.testTraider = new Traider() { ID = 1, FirstName = "testName", Surname = "testSurname", PhoneNum = "testPhoneNum" };

            balancesList = new List<TraiderBalance>()
            {
                new TraiderBalance{ ID = 1, Balance = 0 },
                new TraiderBalance{ ID = 2, Balance = 0 },
                new TraiderBalance{ ID = 3, SimpleType = 20, PreferenceShare = 30, Balance = 2000},
                new TraiderBalance{ ID = 4, SimpleType = 20, PreferenceShare = 30, Balance = 2000}
             }.AsQueryable();

            dataContex.TraiderBalances.Returns(balancesList);

            traidersList = new List<Traider>()
            {
                new Traider{ ID = 1 },
                new Traider{ ID = 2 },
                new Traider{ ID = 3 },
                new Traider{ ID = 4 }
            }.AsQueryable();

            dataContex.Traiders.Returns(traidersList);
        }

        [TestMethod]
        public void TryRegisterNewClient()
        {
            bussinesService.AddNewTraiderWithStarterPack(testTraider);
            dataContex.Add(Arg.Any<Traider>());
            dataContex.SaveChanges();
        }

        [TestMethod]
        public void ZoneChangeAfterChangeBalance()
        {
            TraiderBalance testTraider1 = new TraiderBalance() { ID = 1, SimpleType = 1, PreferenceShare = 1, Balance = 2000, Zone = "Green" };
            TraiderBalance testTraider2 = new TraiderBalance() { ID = 2, SimpleType = 1, PreferenceShare = 1, Balance = 2000, Zone = "Green" };
            Deal testDeal1 = new Deal() { ID_seller = 1, ID_buyer = 2, SharesType = SharesType.SimpleType, Price = 150 };
            Deal testDeal2 = new Deal() { ID_seller = 1, ID_buyer = 2, SharesType = SharesType.SimpleType, Price = 50 };

            this.bussinesService.UpdatingBalance(testTraider1, testTraider2, SharesType.SimpleType, testDeal1);

            Assert.AreEqual(testTraider2.Zone, "Orange");

            this.bussinesService.UpdatingBalance(testTraider1, testTraider2, SharesType.SimpleType, testDeal2);
            Assert.AreEqual(testTraider2.Zone, "Red");
        }

        [TestMethod]
        public void TryGetZeroBalanceTraider()
        {
            foreach (var balanceTr in bussinesService.GetZeroBalanceTraider())
            {
                var balance = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == balanceTr.ID).Balance;
                Assert.AreEqual(0, balance);
            }

        }

        [TestMethod]
        public void TryRegisterNewDeal()
        {
            Deal testDeal = new Deal() { ID = 1, ID_seller = 3, ID_buyer = 4, SharesType = SharesType.SimpleType, Price = 10 };
            bussinesService.RegisterNewDeal(testDeal);
            var balance = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == 3).Balance;
            Assert.AreEqual(balance, 2100);
            balance = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == 4).Balance;
            Assert.AreEqual(balance, 1900);
        }

        [TestMethod]
        public void SharesAmountChangeAfterDeal()
        {
            Deal testDeal1 = new Deal() { ID = 1, ID_seller = 3, ID_buyer = 4, SharesType = SharesType.SimpleType, Price = 10 };
            Deal testDeal2 = new Deal() { ID = 1, ID_seller = 3, ID_buyer = 4, SharesType = SharesType.PreferenceShare, Price = 5 };
            bussinesService.RegisterNewDeal(testDeal1);
            var simpleTypeShare = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == 3).SimpleType;
            Assert.AreEqual(simpleTypeShare, 10);
            bussinesService.RegisterNewDeal(testDeal2);
            var preferenceTypeShare = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == 3).PreferenceShare;
            Assert.AreEqual(preferenceTypeShare, 25);
        }

        [TestMethod]
        public void TryBuyInRedZone()
        {
            Deal testdeal = new Deal() { ID = 1, ID_seller = 3, ID_buyer = 1, SharesType = SharesType.SimpleType, Price = 1 };
            bussinesService.RegisterNewDeal(testdeal);
            var balance = dataContex.TraiderBalances.SingleOrDefault(w => w.ID == 1).Balance;
            Assert.AreEqual(balance, 0);
        }
    }
}
