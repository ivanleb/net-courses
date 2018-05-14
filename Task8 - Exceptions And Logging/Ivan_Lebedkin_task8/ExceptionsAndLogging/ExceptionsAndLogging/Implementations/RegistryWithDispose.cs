using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    public class RegistryWithDispose : IRegistry<SinCurvePointsProducer>
    {
        public IEnumerable<SinCurvePointsProducer> PointProducers { get; set; }
        public RegistryWithDispose(IEnumerable<SinCurvePointsProducer> pointProducers)
        {
            PointProducers = pointProducers;
        }
    }
}
