using System;
using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    class BadProducer : PointsProducer
    {
        public BadProducer(ILoggerService loggerService) : base(loggerService)
        {  }

        public override IPoint BuildPoint(decimal x)
        {
            Random r = new Random();

            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = x / r.Next(-3, 5)
                };
            }, isSilent: true);

        }
    }
}
