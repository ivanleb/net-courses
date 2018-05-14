using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using ExceptionsAndLogging.Abstractions;

namespace ExceptionsAndLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);

            var quadraticProducer = new CustomPointsProducer(x => x * x, -3, 1);
            var scalingProducer = new CustomPointsProducer(x => x * 2, -3, 1);
            var cubicBadProducer = new BadPointsProducer(loggerService, x => x * x * x, -3, (decimal)0.5, x => x%2==0 );

            ProducerCancelingManager manager = new ProducerCancelingManager();
            manager.Add(quadraticProducer, 'q');
            manager.Add(scalingProducer, 'w');
            manager.Add(cubicBadProducer, 'e');

            Client client = new Client("BadProducerSubscriber");
            client.StartListenToBadProducer(cubicBadProducer);

            Task.Run(() =>
            {
                quadraticProducer.Run((point) => loggerService.Info($"Quadratic Function {point}"));
            });
            Task.Run(() =>
            {
                scalingProducer.Run((point) => loggerService.Info($"Scaling Function {point}"));
            });
            Task.Run(() =>
            {
                cubicBadProducer.Run((point) => loggerService.Info($"Cubic Function {point}"));
            });

            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);
                manager.CancelProducer(key);
            }
        }

    }
}
