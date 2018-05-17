using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM_homework_exceptions_and_logging
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("TextLogger");
            var badProducerLogger = LogManager.GetLogger("BadProducerLogger");

            var loggerService = new LoggerService(logger);
            var badLoggerService = new LoggerService(badProducerLogger);

            var quadraticProducer = new QuadraticCurvePointProducer(loggerService);
            var xCosProducer = new XCosCurvePointProducer(loggerService);

            var client = new Client();
            var badProducer = new BadProducer(badLoggerService);

            badProducer.OnNumberGenerated += client.onReceivedPoint;

            var loggingException = new LoggingException(loggerService);

            quadraticProducer.OnXDividedByThree += loggingException.CatchException;
            quadraticProducer.OnYDividedByTwo += loggingException.CatchException;
            xCosProducer.OnXDividedByThree += loggingException.CatchException;
            xCosProducer.OnYDividedByTwo += loggingException.CatchException;
            
            Task.Run(() =>
            {
                quadraticProducer.Run((point) => loggerService.Info($"Quadratic function{point}"));
            }, quadraticProducer.CancellationTokenSource.Token);

            Task.Run(() =>
            {
                xCosProducer.Run((point) => loggerService.Info($"XCos function{point}"));
            }, xCosProducer.CancellationTokenSource.Token);
            
            Task.Run(() => { badProducer.Run((point) => badProducer.onPointReceived(point)); }, badProducer.CancellationTokenSource.Token);

            char c;

            while ((c = Console.ReadKey().KeyChar) != 'q')
            {
                switch (c)
                {
                    case '1':
                        quadraticProducer.CancellationTokenSource.Cancel();
                        break;
                    case '2':
                        xCosProducer.CancellationTokenSource.Cancel();
                        break;
                    case '3':
                        badProducer.CancellationTokenSource.Cancel();
                        break;
                }
            }
        }
    }
}
