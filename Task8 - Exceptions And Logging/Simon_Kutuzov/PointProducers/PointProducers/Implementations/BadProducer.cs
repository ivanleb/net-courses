using System;
using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    public class BadProducer : PointProducer
    {
        private readonly Random rng;

        public BadProducer(ILoggerService logger) : base(logger)
        {
            x = 2.0;
            y = 2.0;
            rng = new Random();
        }

        public override IPoint BuildPoint()
        {
            var new_point = new Point { X = x * rng.NextDouble(), Y = y * rng.NextDouble(), };

            if (new_point.X < 1.0 || new_point.Y < 1.0)
                throw new ArgumentException("Clients won't like this numbers");

            return new_point;
        }
    }
}
