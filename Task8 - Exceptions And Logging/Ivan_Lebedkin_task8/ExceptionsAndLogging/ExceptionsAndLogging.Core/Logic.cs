using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Core
{
    public class Logic
    {
        public void Run(IRegistry registry)
        {
            IEnumerable<IPointProducer> pointProducers = registry.PointProducers;

            foreach (var producer in pointProducers)
            {
                Task.Run(() =>
                {
                    var producerType = producer.GetType().Name.Substring(0, producer.GetType().Name.IndexOf("PointsProducer"));
                    producer.Run(point => producer.GetLoggerService().Info($"{producerType}: {point}"));
                });
                System.Threading.Thread.Sleep(5000);
            }
        }

        public void RunWithDispose<T>(IRegistry<T> registry) where T : IDisposable, IPointProducer
        {
            IEnumerable<T> pointProducers = registry.PointProducers;
            foreach (var producer in pointProducers)
            {
                Task.Run(() =>
                {
                    try
                    {
                        var producerType = producer.GetType().Name.Substring(0, producer.GetType().Name.IndexOf("PointsProducer"));
                        producer.Run(point => producer.GetLoggerService().Info($"{producerType}: {point}"));
                    }
                    finally
                    {
                        producer.Dispose();
                    }
                });
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
