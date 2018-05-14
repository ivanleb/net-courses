using ExceptionsAndLogging.Abstractions;
using System;

namespace ExceptionsAndLogging
{
    class CustomPointsProducer : PointsProducer
    {
        private Func<decimal, decimal> formula;
        private readonly decimal startFrom;
        private readonly decimal delta;

        protected override IPoint BuildPoint(decimal x)
        {
            return new Point() { X = x, Y = this.formula.Invoke(x) };
        }

        protected override decimal GetNext(decimal x)
        {
            return x + delta;
        }

        protected override decimal WhereToStart
        {
            get
            {
                return startFrom;
            }
        }

        public CustomPointsProducer(Func<decimal, decimal> formula, decimal startFrom, decimal delta)
        {
            this.formula = formula;
            this.startFrom = startFrom;
            this.delta = delta;
        }
    }
}
