using EPAM_homework_EF_Core;
using log4net;
using log4net.Config;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPAM_homework_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var container = new Container(new DIContainer());

            var loggerService = container.GetInstance<ILoggerService>();

            using (var dbContext = container.GetInstance<IDataContext>())
            {
                var bussinesService = container.With(dbContext).With(loggerService).GetInstance<BussinesService>();

                Database.SetInitializer(new DbInitializer(bussinesService));

                CancellationTokenSource cts = new CancellationTokenSource();
 
                char c;
                while (!cts.IsCancellationRequested)
                {
                    Task process = new Task(() => container.GetInstance<IProcess>().Run(bussinesService), cts.Token);

                    switch (c = Console.ReadKey().KeyChar)
                    {
                        case 'd':
                            foreach (Client client in dbContext.Clients.ToList())
                                Console.WriteLine(client.ToString());
                            break;
                        case 's':
                            cts.Cancel();
                            break;
                        case ' ':
                            process.Start();
                            //Thread.Sleep(5000);
                            break;
                    }
                }
                /*
                foreach (Client client in dbContext.Clients.ToList())
                    Console.WriteLine($"{client.Id} - {client.FirstName} {client.LastName} {client.Number}");
                    */
            }

            Console.ReadLine();
        }
    }
}
