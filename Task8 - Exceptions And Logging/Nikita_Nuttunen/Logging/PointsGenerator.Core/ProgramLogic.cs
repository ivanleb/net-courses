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

            foreach (var producer in pointProducers)
            {
                Task.Factory.StartNew(() =>
                {
                    var functionName = producer.GetType().Name.Substring(0, producer.GetType().Name.IndexOf("PointsProducer"));
                    producer.Run(point => loggerService.Info($"{functionName}: {point}"));                    
                });
            }
        }
    }
}
