using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionAndLoggingTask8.Abstractions;

namespace ExceptionAndLoggingTask8
{
    class QuadraticProducer : PointProducer
    {
        public QuadraticProducer(ILoggerService loggerService) : base(loggerService)
        {
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
