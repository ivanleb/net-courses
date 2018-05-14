using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    class SquareCurvePointsProducer : PointsProducer
    {
        public SquareCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~SquareCurvePointsProducer()
        {
            this.loggerService.Info($"SquareCurvePointsProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return this.loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = (decimal)Math.Sqrt((double)x)
                };

            }, isSilent: true);
        }
    }
}
