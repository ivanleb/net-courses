using System;
using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    class SqrtProducer : PointProducer
    {
        public SqrtProducer(ILoggerService logger) : base(logger)
        {
            x = 10.0;
            y = -1.0;
        }

        public override IPoint BuildPoint()
        {
            var new_point = new Point { X = Math.Sqrt(x), Y = Math.Sqrt(y)};
            x -= 0.5;
            y += 0.5;

            if (double.IsNaN(new_point.X) || double.IsNaN(new_point.Y))
                throw new NotFiniteNumberException("Attempted to calculate square root of a negative number");

            return new_point;
        }
    }
}
