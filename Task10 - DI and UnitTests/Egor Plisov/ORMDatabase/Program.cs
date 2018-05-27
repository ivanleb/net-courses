using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ORMDatabaseCore;
using log4net;
using log4net.Config;
using StructureMap;
using StructureMap.Pipeline;



namespace ORMDatabase
{
    public class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            Container container = new Container(_ =>
                {
                    _.For<BussinesService>().Use<BussinesService>();
                    _.For<ILoggerService>().Use<LoggerService>().Ctor<ILog>().Is(LogManager.GetLogger("SampletextLogger"));
                    _.For<IDataContex>().Use<MyDbContex>().Ctor<string>().Is(@"Integrated Security=SSPI;Persist Security Info=False;User ID=egor;Initial Catalog=MyBD;Data Source=DESKTOP-I6NBG5H\SQLEXPRESS");
                });

            
            BussinesService bussinesService = container.GetInstance<BussinesService>();
            LoggerService loggerService = new LoggerService(LogManager.GetLogger("SampletextLogger"));

            AddNewTraiders(bussinesService);

            bool isContinue = true;
            Task.Run(() =>
            {
                while (isContinue)
                   {
                        RunTradeDay(bussinesService, loggerService);
                        Thread.Sleep(1000);
                    }
                });

                Console.ReadKey();
                isContinue = false;

                IQueryable<Traider> zeroBalanceTraiders = bussinesService.GetZeroBalanceTraider();

                if (zeroBalanceTraiders.Count() == 0)
                    Console.WriteLine("All traiders are good:)");
                if (zeroBalanceTraiders.Count() != 0 )
                    foreach( Traider bankrot in zeroBalanceTraiders)
                    {
                        Console.WriteLine($"Traider ID: {bankrot.ID} looser, his balanse 0");
                    }

            

            Console.WriteLine("Table created");
            Console.ReadLine();
        }

        public static void AddNewTraiders(BussinesService bussinesService)
        {
            IDataContex dataContext = bussinesService.GetDataContex();
            Traider existTraider = dataContext.Traiders.AsEnumerable().LastOrDefault();

            int newID = 0;

            if (existTraider != null)
                newID = existTraider.ID;

            List<Traider> traiders = new List<Traider>()
            {
                new Traider()
                {
                    ID = ++newID,
                    FirstName = "Black",
                    Surname = "Whiteson",
                    PhoneNum = "34872642891"
                },
                new Traider()
                {
                    ID = ++newID,
                    FirstName = "Red",
                    Surname = "Greenson",
                    PhoneNum = "56454"
                },
                new Traider()
                {
                    ID = ++newID,
                    FirstName = "Homer(Doy!)",
                    Surname = "Simpson",
                    PhoneNum = "1241414"
                },
                new Traider()
                {
                    ID = ++newID,
                    FirstName = "Marge",
                    Surname = "Simpson",
                    PhoneNum = "67868313"
                },
                new Traider()
                {
                    ID = ++newID,
                    FirstName = "Bart(Ay caramba!)",
                    Surname = "Simpson",
                    PhoneNum = "231897193"
                }
            };

            foreach (Traider traider in traiders)
                bussinesService.AddNewTraiderWithStarterPack(traider);
        }

        static void RunTradeDay (BussinesService bussinesService, ILoggerService loggerService)
        {
            var traiders = bussinesService.GetMostWantedTraider();

            List<Traider> traidersList = new List<Traider>();
            foreach (Traider traider in traiders)
            {
                traidersList.Add(traider);
            }

            Random rnd = new Random();
            int sellerIndex = rnd.Next(0, traidersList.Count);
            Traider seller = traidersList.ElementAt(sellerIndex);

            traidersList.RemoveAt(sellerIndex);

            int buyerIndex = rnd.Next(0, traidersList.Count);
            Traider buyer = traidersList.ElementAt(buyerIndex);

            var arrayOfSharesTypes = Enum.GetValues(typeof(SharesType));
            var randomSharesType = (SharesType)arrayOfSharesTypes.GetValue(rnd.Next(arrayOfSharesTypes.Length));
            Deal deal = new Deal
            {
                ID_seller = seller.ID,
                ID_buyer = buyer.ID,
                Price = rnd.Next(1, 100),
                SharesType = randomSharesType
            };

            bussinesService.RegisterNewDeal(deal);

            string sellerName = bussinesService.GetMostWantedTraiderNameById(deal.ID_seller).FirstName +
                bussinesService.GetMostWantedTraiderNameById(deal.ID_seller).Surname;
            string buyerName = bussinesService.GetMostWantedTraiderNameById(deal.ID_buyer).FirstName +
                bussinesService.GetMostWantedTraiderNameById(deal.ID_buyer).Surname;

            Console.WriteLine($"Deal done!: Seller: {sellerName} | Buyer: {buyerName} | ShareType: {deal.SharesType} | Price: {deal.Price}");
            loggerService.Info($"Seller: {sellerName} | Buyer: {buyerName} | ShareType: {deal.SharesType} | Price: {deal.Price}");

        }
    }
}
