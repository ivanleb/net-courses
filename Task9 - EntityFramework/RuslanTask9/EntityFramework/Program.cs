using System;
using System.Collections.Generic;
using System.Linq;
using ORMCore;
using EntityFramework.Implementations;
using log4net;
using log4net.Config;
using System.Threading.Tasks;
using System.Threading;

namespace EntityFramework
{
    
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new TPTContext("Data Source=.;Initial Catalog=TablePerTypeExampleDb2;Integrated Security=True"))
            {
                var bussinesService = new BussinesService(dbContext);

                XmlConfigurator.Configure();

                var logger = LogManager.GetLogger("SampleTextLogger");

                var loggerService = new LoggerService(logger);

                CreateShareholdersWithConnectedBalances(bussinesService);

                //var shareholder = bussinesService.GetMostWantedShareholdersById(6);

                //var buyer = bussinesService.GetMostWantedShareholdersById(8);

                //for (int i = 1; i < 4; i++)
                //    bussinesService.RegisterNewTrade(new Trade
                //    {
                //        ShareholderId = shareholder.Id,
                //        BuyerId = buyer.Id,
                //        Value = 2,
                //        ValueType = "ThirdType"
                //    }, shareholder, buyer);

                var isContinue = true;
                Task.Run(() =>
                {
                    while(isContinue)
                    {
                        RunEmitation(bussinesService, loggerService);
                        Thread.Sleep(10000);
                    }
                });
                Console.ReadKey();
                isContinue = false;
            }

            Console.WriteLine("Table Per Type Done");
            Console.ReadLine();
        }

        static void RunEmitation(BussinesService bussinesService, Interfaces.ILoggable loggerService)
        {
            
            var shareholders = bussinesService.GetMostWantedShareholders();

            //var firstTwoShareholders = c.OrderBy(w => w.Id).Take(2);

            //var secondTwoShareholders = c.OrderBy(w=>w.Id).Skip(2).Take(2);
            List<Shareholder> shareholdersList = new List<Shareholder>();
            foreach (var shareholder in shareholders)
                shareholdersList.Add(shareholder);

            Random random = new Random();

            var randomShareholderIndex = random.Next(0, shareholdersList.Count());//random.Next(0, 2);

            var randomShareholderA = shareholdersList[randomShareholderIndex];//firstTwoShareholders.OrderBy(w=>w.Id).Skip(randomShareholder).First();

            shareholdersList.RemoveAt(randomShareholderIndex);

            randomShareholderIndex = random.Next(0, shareholdersList.Count()); //random.Next(0, 2);

            var randomShareholderB = shareholdersList[randomShareholderIndex];//secondTwoShareholders.OrderBy(w=>w.Id).Skip(randomShareholder).First();

            var countOfSharesTypes = Enum.GetNames(typeof(SharesTypes)).Count();

            var randomIndexOfSharesTypes = random.Next(0, countOfSharesTypes);

            var randomSharesType = Enum.GetNames(typeof(SharesTypes))[randomIndexOfSharesTypes];

            var trade = new Trade
            {
                ShareholderId = randomShareholderA.Id,
                BuyerId = randomShareholderB.Id,
                Value = random.Next(1, 5),
                ValueType = randomSharesType
            };

            bussinesService.RegisterNewTrade(
                trade,
                randomShareholderA, 
                randomShareholderB);

            loggerService.Info($"->{trade.ToString()}<-");

            Console.WriteLine(trade);
        }
        

        static void CreateShareholdersWithConnectedBalances(BussinesService bussinesService)
        {
            List<Shareholder> c = new List<Shareholder>()
                {
                    new Shareholder()
                    {
                        Id = 1,
                        FirstName = "ppp",
                        LastName = "vvvv",
                        PhoneNumber = "111"
                    },

                    new Shareholder()
                    {
                        Id = 2,
                        FirstName = "ppp",
                        LastName = "vvvv",
                        PhoneNumber = "222"
                    },

                    new Shareholder()
                    {
                        Id = 3,
                        FirstName = "ppp",
                        LastName = "vvvv",
                        PhoneNumber = "333"
                    },

                    new Shareholder()
                    {
                        Id = 4,
                        FirstName = "ppp",
                        LastName = "vvvv",
                        PhoneNumber = "444"
                    }
                };

            foreach (var shareholder in c)
                bussinesService.RegisterNewShareholder(shareholder);
        }
        
    }
    
}
