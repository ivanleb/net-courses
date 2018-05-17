using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    public class Registry : IRegistry
    {
        public IEnumerable<IPointProducer> PointProducers { get; set; }
        public Registry(IEnumerable<IPointProducer> pointProducers)
        {
            PointProducers = pointProducers;           
        }
    }
}
