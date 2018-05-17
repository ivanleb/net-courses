using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    class QuadraticProducer : PointsProducer
    {
        public QuadraticProducer(ILoggerService loggerService) : base(loggerService)
        { }

        ~QuadraticProducer()
        {
            this.loggerService.Info($"QuadraticProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * x
            };
        }
    }
}
