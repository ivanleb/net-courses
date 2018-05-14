using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Implementations;
using ExceptionsAndLogging.Core;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");
            var loggerService = new LoggerService(logger);
            

            var quadraticProducer = new QuadraticCurvePointsProducer(loggerService); 
            var cubicProducer = new CubicCurvePointsProducer(loggerService);
            var squareProducer = new SquareCurvePointsProducer(loggerService);
            var badProducer = new BadPointsProducer(loggerService);
            var randomProducer = new RandomPointProducer(loggerService);

            var client = new Client();
            badProducer.onBadPointProduced += client.onPointReceive;
            badProducer.OnEqualZero += client.onPointReceive;
            badProducer.OnMoreTenYProduced += client.onPointReceive;
            var pointProducers = new List<IPointProducer>() { quadraticProducer/*, cubicProducer, squareProducer,*/, badProducer};

            Console.WriteLine("Press Enter for escape log");
            System.Threading.Thread.Sleep(2000);

            Registry registry = new Registry(pointProducers);

            new Logic().Run(registry);
            var sinCurveProducer = new SinCurvePointsProducer(loggerService);
            var disposePointProducers = new List<SinCurvePointsProducer>() { sinCurveProducer };
            RegistryWithDispose disposeRegistry = new RegistryWithDispose(disposePointProducers);
            new Logic().RunWithDispose(disposeRegistry);


            Console.WriteLine("Press enter to run GC, step 1:");
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine("Press enter to run GC, step 1:done");
            Console.ReadLine();
        }
    }
}
