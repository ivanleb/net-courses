using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAndLoggingTask8
{
    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        void Run(Action<IPoint> onPointRecieve, decimal startingValue, decimal step);
    }

    abstract class PointProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnPointProduced;

        protected readonly ILoggerService loggerService;

        protected PointProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public bool IsContinue { get; set; }

        public abstract IPoint BuildPoint(decimal x);

        public void Run(Action<IPoint> onPointRecieve, decimal startingValue, decimal step)
        {
            var x = startingValue;
            IsContinue = true;
            while (IsContinue)
            {
                var point = BuildPoint(x);
                onPointRecieve(point);
                if(point != null)
                {
                    OnPointProduced?.Invoke(this, point);
                }
                x += step;
                System.Threading.Thread.Sleep(1000);
            }

            loggerService.Info($"{GetType()} -> Going to exit");
        }
    }

    class QuadraticProducer : PointProducer
    {
        public QuadraticProducer(ILoggerService loggerService) : base(loggerService)
        {
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

    class CubicProducer : PointProducer
    {
        public CubicProducer(ILoggerService loggerService) : base(loggerService)
        {
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

    class FactorialProducer : PointProducer
    {
        private decimal Factorial(decimal x)
        {
            if (x == 0) return 1;
            return x * Factorial(x - 1);
        }

        public FactorialProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = Factorial(x)
                };
            }, isSilent: true);
        }
    }

    class StrangeProducer : PointProducer
    {
        public StrangeProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            Random rnd = new Random();
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = x / rnd.Next(0, 3)
                };
            }, isSilent: true);
        }
    }
}
