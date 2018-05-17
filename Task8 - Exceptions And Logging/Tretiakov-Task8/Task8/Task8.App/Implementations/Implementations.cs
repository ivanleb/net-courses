using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExceptionsAndLogging;
using Task8.Core;

namespace Task8.App.Implementations
{
    abstract class PointsProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnPointProduced;

        protected readonly ILoggerService LoggerService;

        protected abstract IPoint BuildPoint(decimal x);

        protected PointsProducer(ILoggerService loggerService)
        {
            this.LoggerService = loggerService;
        }

        public bool IsContinue { get; set; }

        public void Run(Func<IPoint, bool> isPointValid)
        {
            decimal x = 0;
            IsContinue = true;

            while (IsContinue)
            {
                var point = LoggerService.RunWithExceptionLogging(() => this.BuildPoint(x++), isSilent: true);

                if (isPointValid(point))
                {
                    OnPointProduced?.Invoke(this, point);
                }
            }

            this.LoggerService?.Info($"{this.GetType()} -> Going to exit...");
        }
    }

    class QuadraticCurvePointsProducer : PointsProducer
    {
        public QuadraticCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {

        }

        protected override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * x
            };
        }
    }

    class TrigonometricPointsProducer : PointsProducer
    {
        public TrigonometricPointsProducer(ILoggerService loggerService) : base(loggerService)
        {

        }

        protected override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = (decimal)Math.Sin((double)x),
                Y = (decimal)Math.Cos((double)x)
            };
        }
    }

    class BadPointsProducer : PointsProducer
    {
        public BadPointsProducer(ILoggerService loggerService) : base(loggerService)
        {

        }

        protected override IPoint BuildPoint(decimal x)
        {
            var random = new Random();
            var point = new Point
            {
                X = Pow(x, random.Next(5)),
                Y = Pow(x, random.Next(5)),
            };
            if (point.X.Equals(point.Y))
                throw new Exception($"Bad thing happend. Point was {point}");
            return point;
        }

        private decimal Pow(decimal x, decimal y)
        {
            if (y == 0)
                return 1;
            if (y == 1)
                return x;
            for (int i = 0; i < y - 1; i++)
            {
                x *= x;
            }
            return x;
        }
    }

    public class Client
    {
        public string Name { get; set; }
        public void OnPointProduced(object sender, IPoint point)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{ Name ?? "Unknown" } received { point }");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
