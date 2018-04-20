using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointsGenerator.Core.Abstractions;

namespace PointsGenerator.ConsoleApp.Implementations
{
    class MyFunctionPointsProducer : BadProducer
    {
        public MyFunctionPointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                var point = new Point
                {
                    X = x, Y = (decimal)Math.Sqrt((double)x) / x
                };
                return point;

            }, isSilent: true);
        }
    }
}
