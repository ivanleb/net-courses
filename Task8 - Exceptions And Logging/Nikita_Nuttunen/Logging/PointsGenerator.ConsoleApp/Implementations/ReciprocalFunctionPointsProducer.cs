using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointsGenerator.Core.Abstractions;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public class ReciprocalFunctionPointsProducer : PointProducer
    {
        public ReciprocalFunctionPointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = 1 / x
                };
            }, isSilent: true);
        }
    }
}
