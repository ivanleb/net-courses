using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_Logging.Implementations;
using log4net;
using log4net.Config;

namespace Exceptions_Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            var loggerService = new LoggerService(logger);

            var taskManager = new TaskManager(loggerService);

            var tasksDictionary = new Dictionary<string, TaskManager.Functions>();
            tasksDictionary.Add("first thread", TaskManager.Functions.Linear);
            tasksDictionary.Add("second thread", TaskManager.Functions.Cube);

            Client client = new Client();
            taskManager.AddBadProducerWithConnectedClient(client);


            foreach (var task in tasksDictionary)
            {
                taskManager.AddThread(task.Value, task.Key);
            }

            taskManager.Run();

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    taskManager.Stop();
                    break;
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            GC.Collect();
            Console.ReadKey();
        }
    }
}
