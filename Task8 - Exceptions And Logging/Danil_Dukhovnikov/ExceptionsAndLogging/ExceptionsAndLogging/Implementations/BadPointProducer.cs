using System;
using ExceptionsAndLogging.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    internal class BadPointProducer : CustomPointProducer
    {
        private readonly Func<decimal, bool> _isBadValue;
        private readonly ILoggerService _logger;
        public event EventHandler<IPoint> OnGoodPointProduced;

        public BadPointProducer(ILoggerService logger, Func<decimal, decimal> function, decimal startFrom, decimal increment, Func<decimal, bool> isBadValue) : base(function, startFrom, increment)
        {
            this._logger = logger;
            this._isBadValue = isBadValue;
        }

        protected override decimal WhereToStart => base.WhereToStart;

        protected override IPoint BuildPoint(decimal x)
        {
            return _logger.RunWithExceptionLogging<IPoint>(() =>
            {
                if (this._isBadValue(x))
                {
                    throw new ArgumentException("Bad argument recived.", nameof(x));
                }
                var point = base.BuildPoint(x);
                this.OnGoodPointProduced?.Invoke(this, point);
                return point;
            }, isSilent: true);
        }

        protected override decimal GetNext(decimal x)
        {
            return base.GetNext(x);
        }
    }
}
