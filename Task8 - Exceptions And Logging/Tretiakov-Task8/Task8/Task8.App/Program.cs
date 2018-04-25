using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging;
using log4net;
using log4net.Config;
using Task8.App.Implementations;

namespace Task8.App
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);

            var client = new Client() {Name = "Client"};
            var trigonometricProducer = new TrigonometricPointsProducer(loggerService);
            var quadricProducer = new QuadraticCurvePointsProducer(loggerService);
            var badProducer = new BadPointsProducer(loggerService);
            trigonometricProducer.OnPointProduced += client.OnPointProduced;
            quadricProducer.OnPointProduced += client.OnPointProduced;
            badProducer.OnPointProduced += client.OnPointProduced;
            loggerService.Info("TrigonometricProducer started");
            Console.WriteLine("Press S to stop and start Quadric producer.");
            Task.Run(() => trigonometricProducer.Run(point => point != null));

            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    trigonometricProducer.IsContinue = false;
                    break;
                }
            }

            loggerService.Info("QuadraticProducer started");
            Console.WriteLine("Press S to stop and start Bad producer.");
            Task.Run(() => quadricProducer.Run(point => point != null));
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    quadricProducer.IsContinue = false;
                    break;
                }
            }

            loggerService.Info("BadProducer started");
            Console.WriteLine("Press S to stop.");
            Task.Run(() => badProducer.Run(point => point != null));
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.S)
                {
                    badProducer.IsContinue = false;
                    break;
                }
            }
            Console.WriteLine("Process's been interrupted.");
            Console.ReadKey();
        }
    }
}
