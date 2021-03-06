﻿using System;
using System.Collections.Generic;
using System.Linq;
using ORMCore;
using EntityFramework.Implementations;
using log4net;
using log4net.Config;
using System.Threading.Tasks;
using System.Threading;
using StructureMap;
using System.Data.Entity;

namespace EntityFramework
{
    
    class Program
    {
        static void Main(string[] args)
        {

            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");
            



            //var loggerService = new LoggerService(logger);
            Container container = new Container((c) =>
            {
                c.For<IDataContext>().Use<TPTContext>().Ctor<string>().Is("Data Source=.;Initial Catalog=TablePerTypeExampleDb2;Integrated Security=True");
                //c.For<BussinesService>().Use<BussinesService>().Ctor<IDataContext>().Is(new TPTContext("Data Source=.;Initial Catalog=TablePerTypeExampleDb2;Integrated Security=True"));
                c.For<IBussinesService>().Use<BussinesService>();
                c.For<EntityFramework.Interfaces.ILoggable>().Use<LoggerService>().Ctor<ILog>().Is(logger);
            });
            
            var loggerService = container.GetInstance<EntityFramework.Interfaces.ILoggable>();

            var bussinesService = container.GetInstance<BussinesService>();

            var isContinue = true;

            Task.Run(() =>
            {
                while (isContinue)
                {
                    RunEmitation(bussinesService, loggerService);
                    Thread.Sleep(1000);
                }
            });
            Console.ReadKey();
            isContinue = false;

            var shareholders = bussinesService.GetShareholdersWithZeroBalance();

            if (shareholders.Count() < 1)
                Console.WriteLine("No shareholders with zero balance");
            else
                foreach (var shareholder in shareholders)
                {
                    Console.WriteLine($"id: {shareholder.Id}| balance: 0.00");
                }


            Console.WriteLine("Table Per Type Done");
            Console.ReadLine();
        }

        static void RunEmitation(BussinesService bussinesService, Interfaces.ILoggable loggerService)
        {
            
            var shareholders = bussinesService.GetAllShareholders();
            
            List<Shareholder> shareholdersList = new List<Shareholder>();
            foreach (var shareholder in shareholders)
                shareholdersList.Add(shareholder);

            Random random = new Random();

            var randomShareholderIndex = random.Next(0, shareholdersList.Count());

            var randomShareholderA = shareholdersList[randomShareholderIndex];

            shareholdersList.RemoveAt(randomShareholderIndex);

            randomShareholderIndex = random.Next(0, shareholdersList.Count()); 

            var randomShareholderB = shareholdersList[randomShareholderIndex];
            
            var arrayOfSharesTypes = Enum.GetValues(typeof(SharesTypes));

            var randomSharesType = (SharesTypes)arrayOfSharesTypes.GetValue(random.Next(arrayOfSharesTypes.Length));

            var trade = new Trade
            {
                ShareholderId = randomShareholderA.Id,
                BuyerId = randomShareholderB.Id,
                Value = random.Next(1, 30),
                ValueType = randomSharesType
            };

            bussinesService.RegisterNewTrade(
                trade,
                randomShareholderA, 
                randomShareholderB);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("log info: ");
            loggerService.Info($"->{trade.ToString()}<-");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(trade);
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine($"Shareholder's " +
                $"\n\tbalance value: {bussinesService.GetMostWantedBalanceById(randomShareholderA.Id).BalanceValue.ToString()}" +
                $"\n\tbalance zone: {bussinesService.GetMostWantedBalanceById(randomShareholderA.Id).BalanceZone.ToString()}");

            Console.WriteLine($"Buyer's " +
                $"\n\tbalance value: {bussinesService.GetMostWantedBalanceById(randomShareholderB.Id).BalanceValue.ToString()}" +
                $"\n\tbalance zone: {bussinesService.GetMostWantedBalanceById(randomShareholderB.Id).BalanceZone.ToString()}");
        }

    }
    
}
