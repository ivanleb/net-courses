using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    public class RandomPointProducer : PointsProducer
    {
        private Random rnd;
        public RandomPointProducer(ILoggerService loggerService) : base(loggerService)
        {
            rnd = new Random();
        }

        public override IPoint BuildPoint(decimal x)
        {
            return this.loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = (decimal)rnd.Next((int)(x * 2)),
                    Y = (decimal)rnd.Next((int)(x * 2)) 
                };
            }
            );
        }
    }
}
