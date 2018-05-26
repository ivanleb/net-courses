using EPAM_homework_EF_Core;
using log4net;
using log4net.Config;
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

            var logger = LogManager.GetLogger("TextLogger");

            var loggerService = new LoggerService(logger);

            using (var dbContext = new TablePerConcreteClass(
              @"Data Source=.;Initial Catalog=SharesCompany;Integrated Security=True"))
            {
                var bussinesService = new BussinesService(dbContext, loggerService);

                Database.SetInitializer(new DbInitializer(bussinesService));

                CancellationTokenSource cts = new CancellationTokenSource();
 
                char c;
                while (!cts.IsCancellationRequested)
                {
                    Task process = new Task(() => new ProcessSimulation(dbContext).Run(bussinesService), cts.Token);

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
