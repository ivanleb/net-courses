using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    public class ReciprocalProducer : PointProducer
    {
        public ReciprocalProducer(ILoggerService logger) : base(logger)
        {
            x = 4.0;
            y = -4.0;
        }

        public override IPoint BuildPoint()
        {
            var new_point = new Point { X = 1 / x, Y = 1 / y };
            x -= 0.5;
            y += 0.5;

            if (double.IsInfinity(new_point.X) || double.IsInfinity(new_point.Y))
                throw new System.DivideByZeroException();

            return new_point;
        }
    }
}
