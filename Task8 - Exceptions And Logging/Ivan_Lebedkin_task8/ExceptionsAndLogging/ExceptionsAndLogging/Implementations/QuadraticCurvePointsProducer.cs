using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    public class QuadraticCurvePointsProducer : PointsProducer
    {
        public QuadraticCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~QuadraticCurvePointsProducer()
        {
            this.loggerService.Info($"QuadraticCurvePointsProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * x
            };
        }
    }
}
