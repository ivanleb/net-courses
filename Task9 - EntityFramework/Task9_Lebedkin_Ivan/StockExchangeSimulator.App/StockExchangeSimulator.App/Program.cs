using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using StockExchangeSimulator.BL;
using log4net;
using log4net.Config;
using StockExchangeSimulator.App.Implementations;
using StockExchangeSimulator.BL.Contracts;
using StockExchangeSimulator.BL.Domain;
using StockExchangeSimulator.Data.Models;
using StockExchangeSimulator.Data.Repositories;

namespace StockExchangeSimulator.App
{



    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("Logger");
            var loggerService = new LoggerService(logger);

            var transactionInspectors = new List<Func<Transaction, bool>>
            {
                (t) => t != null,
                (t) => t.Seller != null && t.Buyer != null,
                (t) => t.Seller.Id != t.Buyer.Id,
                (t) => t.StocksQuantity >= 0,
                //(t) => t.Buyer.Balance >=0,
                (t) => (t.StocksQuantity <= t.Seller.ClientStocksQuantity)
            };

            var transactionValidators = new List<TransactionValidator>();
            transactionInspectors.ForEach(i => transactionValidators.Add(new TransactionValidator(i)));

            using (var dataContext = new StockExchangeDataContext())//$"data source=(LocalDb)\\MSSQLLocalDB;initial catalog=StockExchangeCDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"))
            {
                using (IContainer container = IoCBuilder.Build(transactionValidators, loggerService, dataContext))
                {
                    var app = container.Resolve<TradeManager>();

                    bool isContinue = true;

                    Console.WriteLine("Press any key for start trading\nPress C for stop trading");

                    Console.ReadKey();

                    Task.Run(() =>
                    {
                        app.Run();
                    });

                    while (true)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.C)
                        {
                            app.IsContinue = false;
                            break;
                        }
                    }

                    dataContext.SaveChanges();

                    app.ShowResults(Console.WriteLine);
                }
            }
        }
    }
}
