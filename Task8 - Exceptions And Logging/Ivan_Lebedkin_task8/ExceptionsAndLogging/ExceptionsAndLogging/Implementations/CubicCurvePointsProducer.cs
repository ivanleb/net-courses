using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    class CubicCurvePointsProducer : PointsProducer
    {
        public CubicCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~CubicCurvePointsProducer()
        {
            this.loggerService.Info($"CubicCurvePointsProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * x * x
            };
        }
    }
}
