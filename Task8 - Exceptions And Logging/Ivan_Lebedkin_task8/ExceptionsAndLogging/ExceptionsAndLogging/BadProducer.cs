using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;
using ExceptionsAndLogging.Implementations;


namespace ExceptionsAndLogging
{
    class BadPointsProducer : PointsProducer
    {
        public event EventHandler<IPoint> onBadPointProduced;
        public event EventHandler<IPoint> OnEqualZero;
        public event EventHandler<IPoint> OnMoreTenYProduced;

        public BadPointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            return this.loggerService.RunWithExceptionLogging<IPoint>(() =>
            {
                if (x < 0)
                {
                    throw new ArgumentException("Bad point");                    
                }

                if (x > 20)
                {
                    throw new ArgumentOutOfRangeException("more than 20");
                }
                return new Point()
                { X = x, Y = rnd.Next((int) (x * x)) };
            }, isSilent: true);
        }

        override public void Run(Action<IPoint> onPointReceive)
        {
            IsContinue = true;
            decimal x = rnd.Next(-5,20);
            while (IsContinue)
            {
                var point = this.BuildPoint(x++);
                if (point != null)
                {
                    System.Threading.Thread.Sleep(500);
                    //Console.WriteLine($"bad prod {point.ToString()}");
                    if (IsPrime(x))
                    {
                        onBadPointProduced?.Invoke(this, point);
                        continue;
                    }

                    if (equalZero(point.X))
                    {
                        OnEqualZero?.Invoke(this, point);
                        continue;
                    }

                    if (moreThanTen(point.Y))
                    {
                        OnMoreTenYProduced?.Invoke(this, point);
                        continue;
                    }

                    onPointReceive(point);//передача в логгер
                }                
            }
        }

        Func<decimal, bool> IsPrime = n =>
        {
            if (n > 1)
            {
                return Enumerable.Range(1, (int)n).Where(x => (int)n % x == 0)
                                 .SequenceEqual(new[] { 1, (int)n });
            }
            return false;
        };

        Func<decimal, bool> equalZero = x => x == 0;
        Func<decimal, bool> moreThanTen = x => x > 10;
    }
}
