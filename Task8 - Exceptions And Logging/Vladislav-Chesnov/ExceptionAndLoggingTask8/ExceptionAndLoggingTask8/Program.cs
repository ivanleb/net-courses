using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net;
using System.IO;

namespace ExceptionAndLoggingTask8
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            var loggerService = new LoggerService(logger);

            var quadraticProducer = new QuadraticProducer(loggerService);
            var cubicProducer = new CubicProducer(loggerService);
            var strangeProducer = new BadProducer(loggerService);

            var client1 = new Client("First client");

            client1.StartListenProducer(strangeProducer);

            Console.WriteLine("Starting 3 generators: quadratic, cubic and bad. To stop them press 1, 2 or 3 respectively");
            Task.Run(() =>
            {
                quadraticProducer.Run((point) => loggerService.Info($"Quadratic Function {point}"), 0, 1);
            });
            Task.Run(() =>
            {
                cubicProducer.Run((point) => loggerService.Info($"Cubic Function {point}"), 0, 1);
            });
            Task.Run(() =>
            {
                strangeProducer.Run((point) => loggerService.Info($"Bad function {point}"), 0, 1);
            });

            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1) quadraticProducer.IsContinue = false;
                if (key.Key == ConsoleKey.D2) cubicProducer.IsContinue = false;
                if (key.Key == ConsoleKey.D3) strangeProducer.IsContinue = false;
            }
        }
    }
}
