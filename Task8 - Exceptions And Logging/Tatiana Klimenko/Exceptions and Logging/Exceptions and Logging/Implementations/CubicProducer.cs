using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    class CubicProducer : PointsProducer
    {
        public CubicProducer(ILoggerService loggerService) : base(loggerService)
        { }

        ~CubicProducer()
        {
            this.loggerService.Info($"CubicProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * x * x
            };
        }
    }

}
