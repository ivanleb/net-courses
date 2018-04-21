using log4net;
using log4net.Config;
using PointsGenerator.ConsoleApp.Implementations;
using PointsGenerator.Core;
using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            var loggerService = new LoggerService(logger);
            var badFunction = new MyFunctionPointsProducer(loggerService);
            var pointProducers = new List<IPointProducer>()
            {                
                new ReciprocalFunctionPointsProducer(loggerService),
                badFunction
            };
            Client client = new Client();
            client.StartListeningToProducer(badFunction);

            Console.WriteLine("Points will be producing in 3 seconds" +
                "\nPress Enter to stop producing...");
            System.Threading.Thread.Sleep(2000);

            ProgramLogic.Run(new Registry()
            {
                LoggerService = loggerService,
                PointProducers = pointProducers
            });            

            Console.ReadLine();

            foreach (var generator in pointProducers)
            {
                generator.IsContinue = false;
            }
            Console.ReadLine();
        }
    }
}
