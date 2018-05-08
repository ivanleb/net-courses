using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockExchangeSimulator.DataModel;
using StockExchangeSimulator.Implementations;
using StockExchangeSimulator.Core.Abstractions;
using StockExchangeSimulator.Core.DataModel;
using StockExchangeSimulator.Core;
using log4net;
using log4net.Config;

namespace StockExchangeSimulator
{
    class Program
    {
        public static void FillClientsToDB(IQueryable<Client> clients, StockExchangeContex stockExchangeContex)
        {
            foreach (var client in clients)
            {
                stockExchangeContex.Clients.Add(client);
            }

            stockExchangeContex.SaveChanges();
        }

        public static void ClearBD(StockExchangeContex stockExchangeContex)
        {

            var clients = from client in stockExchangeContex.Clients
                          select client;
            stockExchangeContex.Clients.RemoveRange(clients);

            var stocks = from stock in stockExchangeContex.Stocks
                         select stock;
            stockExchangeContex.Stocks.RemoveRange(stocks);

            var transactions = from transaction in stockExchangeContex.Transactions
                               select transaction;
            stockExchangeContex.Transactions.RemoveRange(transactions);

            stockExchangeContex.SaveChanges();
        }


        static void Main(string[] args)
        {
            //filling clients and stocks tables from .json
            IInitialClientsDataModel dataModel = new InitialisationClientFromJSON(".\\data.json");
            var stocks = dataModel.Stocks;
            var clients = dataModel.Clients;

            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("Logger");
            var loggerService = new LoggerService(logger);

            using (StockExchangeContex contex = new StockExchangeContex())
            {
                if (contex.Clients != null || contex.Clients.ToList().Count != 0)
                {
                    ClearBD(contex);
                }
                FillClientsToDB(clients, contex);

                Random rnd = new Random();

                Func<Client> getClient = () =>
                {
                    var rndIndex = contex.Clients.First().Id + rnd.Next(0, contex.Clients.ToList().Count);
                    var choosedClients = from client in contex.Clients
                                         where client.Id == rndIndex && client.Stock != null
                                         select client;
                    return choosedClients.First();
                };

                Action<Transaction> fillTransaction = (t) =>
                {
                    t.Buyer.Balance -= t.Stock.Price * t.StocksQuantity;

                    t.Seller.Balance += t.Stock.Price * t.StocksQuantity;

                    t.Seller.ClientStocksQuantity -= t.StocksQuantity;

                    contex.Transactions.Add(t);
                    contex.SaveChanges();
                };

                List<Func<Transaction, bool>> TransactionInspectors = new List<Func<Transaction, bool>> {
                    (t) => t!=null ,
                    (t) => t.Seller !=null && t.Buyer!=null,
                    (t) => t.Seller.Id != t.Buyer.Id,
                    (t) => t.StocksQuantity >= 0,
                    //(t) => t.Buyer.Balance >=0,
                    (t)=> (t.StocksQuantity <= t.Seller.ClientStocksQuantity)
                };

                RandomTransactionGenerator rndTransactionGenerator = new RandomTransactionGenerator(loggerService);

                rndTransactionGenerator.onTakeClient += getClient;
                rndTransactionGenerator.onTransactionGenerate += fillTransaction;

                Registry registry = new Registry();
                registry.DataContext = contex;
                registry.LoggerService = loggerService;
                registry.TransactionGenerator = rndTransactionGenerator;
                //registry.CheckTransaction = CheckTransaction;
                registry.CheckTransaction = TransactionInspectors;
                Console.WriteLine("Press any key for start trading\nPress key \"c\" for stop trading");
                Console.ReadKey();
                Task.Run(() => new TradeLogic().Run(registry));

                while (true)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.C)
                    {
                        rndTransactionGenerator.isContinue = false;
                        break;
                    }
                }
                contex.SaveChanges();
            }

            using (StockExchangeContex contex = new StockExchangeContex())
            {
                var traders = contex.Clients.GetOrangeZoneClients();
                Console.WriteLine("\nClients with null balance:");
                foreach (var trader in traders)
                {
                    Console.WriteLine($"{trader.FirstName} {trader.SurName} , Balance = {trader.Balance}, Stock fund = {trader.ClientStocksQuantity}");
                }

                var banbankrupts = contex.Clients.GetBlackZoneClients();
                Console.WriteLine("\nClients with negative balance:");
                foreach (var banbankrupt in banbankrupts)
                {
                    Console.WriteLine($"{banbankrupt.FirstName} {banbankrupt.SurName} , Balance = {banbankrupt.Balance}, Stock fund = {banbankrupt.ClientStocksQuantity}");
                }

                Console.WriteLine("\nAll clients:");
                foreach (var client in contex.Clients)
                {
                    Console.WriteLine($"{client.FirstName} {client.SurName} , Balance = {client.Balance}, Stock fund = {client.ClientStocksQuantity}");
                }
                Console.WriteLine("Press ay key for exit ");
                Console.ReadKey();
            }
        }
    }
}
