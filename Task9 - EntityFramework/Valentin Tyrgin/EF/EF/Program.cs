using System;
using EF.Core;
using EF.Implementations;

namespace EF
{
    internal class Program
    {
        private static void Main()
        {
            Logger.InitLogger();
            using (var db = new StockExchangeDataContext("DBConnection"))
            {
                var bs = new BusinessService(db);

                db.Status = Status.GetStatusInstance();

                var gen = new RandomTransactionGenerator(bs);

                Console.CancelKeyPress += (sender, args) =>
                {
                    gen.Active = false;
                    args.Cancel = true;
                };

                gen.Generate();

                bs.GetAllClients().ShowAll(Traider.Header);
                bs.GetBlackZoneTraiders().ShowAll(Traider.Header);
                bs.GetOrangeZoneTraiders().ShowAll(Traider.Header);
                bs.GetAllOperations().ShowAll(Operation.Header);
            }
            Console.WriteLine("\nСорян за такое завершение :(");
            Console.ReadLine();
        }
    }
}