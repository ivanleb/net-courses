using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using ExceptionsAndLogging.Abstractions;
using ExceptionsAndLogging.Implementations;

namespace ExceptionsAndLogging
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);

            var quadraticProducer = new CustomPointProducer(x => x * x, -3, 1);
            var scalingProducer = new CustomPointProducer(x => x * 2, -3, 1);
            var cubicBadProducer =
                new BadPointProducer(loggerService, x => x * x * x, -3, (decimal) 0.5, x => x % 2 == 0);

            var manager = new ProducerCancelingManager();
            manager.Add(quadraticProducer, 'q');
            manager.Add(scalingProducer, 'w');
            manager.Add(cubicBadProducer, 'e');

            var client = new Client("BadProducerSubscriber");
            client.StartListenToBadProducer(cubicBadProducer);

            Task.Run(action: () =>
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

            var key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);
                manager.CancelProducer(key);
            }
        }

    }
}
