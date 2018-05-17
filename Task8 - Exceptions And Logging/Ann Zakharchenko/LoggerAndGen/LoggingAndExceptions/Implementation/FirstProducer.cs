using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingAndExceptions.Abstracrions;

namespace LoggingAndExceptions.Implementation
{
    class FirstProducer : PointProducer
    {
        public FirstProducer(ILoggerService loggerService) : base(loggerService) { }
        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = 150M / (x + 3M) / (x - 7M)
                };   
            }, isSilent: true);
        }
    }
}
