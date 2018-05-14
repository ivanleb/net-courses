using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPAM_homework_exceptions_and_logging
{
    interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

    class Point : IPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }

    interface IPointProducer
    {
        //bool IsContinue { get; set; }
        CancellationTokenSource CancellationTokenSource { get; set; }

        void Run(Action<IPoint> onPointReceive);
    }

    abstract class PointProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnXDividedByThree;
        public event EventHandler<IPoint> OnYDividedByTwo;

        protected readonly ILoggerService loggerService;

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public abstract IPoint BuildPoint(decimal x);

        //public bool IsContinue { get; set; }

        protected PointProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
            CancellationTokenSource = new CancellationTokenSource();
        }

        public void Run(Action<IPoint> onPointReceive)
        {
            decimal x = -10;

            while (!CancellationTokenSource.IsCancellationRequested)
            {
                IPoint point = BuildPoint(x++);

                if (point.X % 3 == 0)
                    OnXDividedByThree?.Invoke(this, point);

                if (point.Y % 2 == 0)
                    OnYDividedByTwo?.Invoke(this, point);

                onPointReceive(point);

                Thread.Sleep(1000);
            }
        }
    }

    class QuadraticCurvePointProducer : PointProducer
    {
        public QuadraticCurvePointProducer(ILoggerService loggerService) : base(loggerService)
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

    class XCosCurvePointProducer : PointProducer
    {
        public XCosCurvePointProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = x * (decimal)Math.Cos((double)x)
            };
        }
    }

    class BadProducer : PointProducer
    {
        public event Action<IPoint> OnNumberGenerated;

        public void onPointReceived(IPoint point)
        {
            if (point.Y > 0)
                OnNumberGenerated(point);
            else
                loggerService.Info($"Bad producer don't succeed {point}");
        }

        public BadProducer(ILoggerService loggerService) : base(loggerService)
        { }

        public override IPoint BuildPoint(decimal x)
        {
            var point =  new Point
            {
                X = x,
                Y = (decimal)(Math.Pow(-1, (double)x % 2) * Math.Sin((double)x)) / (x == 0? 1 : x)
            };

            return point;
        }
    }

    class Client
    {
        public void onReceivedPoint(IPoint point)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(point.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class LoggingException
    {
        private readonly ILoggerService loggerService;
        public List<PointProducer> Producers { get; set; }

        public LoggingException(ILoggerService loggerService)
        {
            Producers = new List<PointProducer>();

            this.loggerService = loggerService;
        }

        public void CatchException(object sender, IPoint e)
        {
            loggerService.Info($"Point {e.ToString()} catched by {sender.ToString()}");
        }
    }
}
