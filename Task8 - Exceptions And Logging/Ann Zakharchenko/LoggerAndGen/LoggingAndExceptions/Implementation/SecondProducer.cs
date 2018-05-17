using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingAndExceptions.Abstracrions;

namespace LoggingAndExceptions.Implementation
{
    class SecondProducer : PointProducer
    {
        public SecondProducer(ILoggerService loggerService) : base(loggerService) { }
        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = (decimal)Math.Sqrt((double)(7M - x))/(x + 4M)
                };
            }, isSilent: true);
        }
    }
}
