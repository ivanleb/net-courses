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
            var exceptionIndicator = new ExceptionIndicator(loggerService);
            var pointProducers = new List<IPointProducer>()
            {
                new MyFunctionPointsProducer(loggerService),
                new ReciprocalFunctionPointsProducer(loggerService)
            };

            foreach (var producer in pointProducers)
            {
                var badProducer = producer as BadProducer;
                if (badProducer != null)
                {
                    badProducer.OnNegativeXProduced += exceptionIndicator.WriteException;
                    badProducer.OnZeroXProduced += exceptionIndicator.WriteException;
                }
                exceptionIndicator.Producers.Add(producer);
            }

            Console.WriteLine("Press Enter to stop generators...");
            System.Threading.Thread.Sleep(2000);

            ProgramLogic.Run(new Registry()
            {
                LoggerService = loggerService,
                ExceptionIndicator = exceptionIndicator,
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
