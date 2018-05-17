using System;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Exceptions_and_Logging.Implementations;

namespace Exceptions_and_Logging
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
            var badProducer = new BadProducer(loggerService);

            var client = new Client("kisa");

            client.StartListenProducer(badProducer);

            Console.WriteLine("Start listening to bad producer...\nPress space bar to exit.");

            Task.Run(() =>
            {
                quadraticProducer.Run((point) => loggerService.Info($"Quadratic function {point}"));
            });

            Task.Run(() =>
            {
                cubicProducer.Run((point) => loggerService.Info($"Cubic function {point}"));
            });

            Task.Run(() =>
            {
                badProducer.Run((point) => loggerService.Info($"Bad function {point}"));
            });

            
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    quadraticProducer.IsContinue = false;
                    cubicProducer.IsContinue = false;
                    badProducer.IsContinue = false;
                }
            }

        }
    }
}
