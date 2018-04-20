using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.Core
{
    public static class ProgramLogic
    {
        public static void Run(IRegistry registry)
        {
            IList<IPointProducer> pointProducers = registry.PointProducers;
            ILoggerService loggerService = registry.LoggerService;
            IExceptionIndicator exceptionIndicator = registry.ExceptionIndicator;

            foreach (var producer in pointProducers)
            {
                exceptionIndicator.Producers.Add(producer);
            }


            foreach (var producer in pointProducers)
            {
                Task.Factory.StartNew(() =>
                {
                    producer.Run(point => loggerService.Info($"{point}"));                    
                });
            }
        }
    }
}
