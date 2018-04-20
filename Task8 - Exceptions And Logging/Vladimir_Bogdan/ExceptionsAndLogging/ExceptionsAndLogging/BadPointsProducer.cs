using ExceptionsAndLogging.Abstractions;
using System;

namespace ExceptionsAndLogging
{
    class BadPointsProducer : CustomPointsProducer
    {
        private Func<decimal, bool> IsBadValue;
        private ILoggerService logger;
        public event EventHandler<IPoint> OnGoodPointProduced;

        public BadPointsProducer(ILoggerService logger, Func<decimal, decimal> formula, decimal startFrom, decimal delta, Func<decimal, bool> isBadValue) : base(formula, startFrom, delta)
        {
            this.logger = logger;
            this.IsBadValue = isBadValue;
        }

        protected override decimal WhereToStart
        {
            get
            {
                return base.WhereToStart;
            }
        }

        protected override IPoint BuildPoint(decimal x)
        {
            return logger.RunWithExceptionLogging<IPoint>(() =>
            {
                if (this.IsBadValue(x))
                {
                    throw new ArgumentException("Bad argument recived.", "x");
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
