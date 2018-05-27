using System;
using ExceptionsAndLogging.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    internal class CustomPointProducer : PointProducer
    {
        private readonly Func<decimal, decimal> _function;
        private readonly decimal _increment;

        protected override IPoint BuildPoint(decimal x)
        {
            return new Point() { X = x, Y = this._function.Invoke(x) };
        }

        protected override decimal GetNext(decimal x)
        {
            return x + _increment;
        }

        protected override decimal WhereToStart { get; }

        public CustomPointProducer(Func<decimal, decimal> function, decimal startFrom, decimal increment)
        {
            this._function = function;
            this.WhereToStart = startFrom;
            this._increment = increment;
        }
    }
}
