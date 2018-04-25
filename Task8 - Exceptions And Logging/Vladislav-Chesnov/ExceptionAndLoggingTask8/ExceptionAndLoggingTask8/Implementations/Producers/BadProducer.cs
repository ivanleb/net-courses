using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionAndLoggingTask8.Abstractions;

namespace ExceptionAndLoggingTask8
{   
    class BadProducer : PointProducer
    {
        public BadProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            Random rnd = new Random();
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = x / rnd.Next(0, 3)
                };
            }, isSilent: true);
        }
    }
}
