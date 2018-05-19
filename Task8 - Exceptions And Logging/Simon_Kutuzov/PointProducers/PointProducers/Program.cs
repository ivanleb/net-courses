using System;
using System.Threading.Tasks;
using PointProducers.Implementations;

namespace PointProducers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Esc' to stop producing points.");
            log4net.Config.XmlConfigurator.Configure();
            var loggerService = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            var reciprocalProducer = new ReciprocalProducer(loggerService);
            var sqrtProducer = new SqrtProducer(loggerService);
            var badProducer = new BadProducer(loggerService);
            var client = new Client("Steve", loggerService);

            badProducer.OnPointProduced += client.OnPointReceived;

            Task.Run(() =>
            {
                reciprocalProducer.Run((point) => loggerService.Info($"Reciprocal producer made {point}"));
            });
            Task.Run(() =>
            {
                sqrtProducer.Run((point) => loggerService.Info($"Sqrt producer made {point}"));
            });
            Task.Run(() =>
            {
                badProducer.Run((point) => loggerService.Info($"Bad producer made {point}"));
            });

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    reciprocalProducer.KeepRunning = false;
                    sqrtProducer.KeepRunning = false;
                    badProducer.KeepRunning = false;
                    break;
                }
            }
        }
    }
}
