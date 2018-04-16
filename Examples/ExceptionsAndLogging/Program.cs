using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExceptionsAndLogging
{
    interface IPoint
    {
        decimal X { get; set; }
        decimal Y { get; set; }
    }

    struct Point : IPoint
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
        bool IsContinue { get; set; }

        void Run(Action<IPoint> onPointReceive);
    }

    abstract class PointsProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnZeroXProduced;
        public event EventHandler<IPoint> OnZeryYProduced;

        protected readonly ILoggerService loggerService;

        public bool IsContinue { get; set; }

        public abstract IPoint BuildPoint(decimal x);

        protected PointsProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public void Run(Action<IPoint> onPointReceive)
        {
            IsContinue = true;

            decimal x = -2;

            while (IsContinue)
            {
                var point = this.BuildPoint(x++);

                if (point.X == 0)
                {
                    OnZeroXProduced?.Invoke(this, point);
                }

                if (point.Y == 0)
                {
                    OnZeryYProduced?.Invoke(this, point);
                }

                onPointReceive(point);

                System.Threading.Thread.Sleep(1000);
            }

            this.loggerService.Info($"{this.GetType()} -> Going to exit...");
        }
    }

    class QuadraticCurvePointsProducer : PointsProducer
    {
        public QuadraticCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~QuadraticCurvePointsProducer()
        {
            this.loggerService.Info($"QuadraticCurvePointsProducer will remove from heap");
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

    class CubicCurvePointsProducer : PointsProducer
    {
        public CubicCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~CubicCurvePointsProducer()
        {
            this.loggerService.Info($"CubicCurvePointsProducer will remove from heap");
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

    class SquareCurvePointsProducer : PointsProducer
    {
        public SquareCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~SquareCurvePointsProducer()
        {
            this.loggerService.Info($"SquareCurvePointsProducer will remove from heap");
        }

        public override IPoint BuildPoint(decimal x)
        {
            return this.loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = (decimal)Math.Sqrt((double)x)
                };

            }, isSilent: true);
        }
    }

    class SinCurvePointsProducer : PointsProducer, IDisposable
    {
        public SinCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~SinCurvePointsProducer()
        {
            this.loggerService.Info($"SinCurvePointsProducer will remove from heap");
            this.Cleanup(isClearManaged: false);
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = (decimal)Math.Sin((double)x)
            };
        }

        public void Dispose()
        {
            // see the best practice at https://msdn.microsoft.com/ru-ru/library/b1yfkh5e(v=vs.100).aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1
            this.Cleanup(isClearManaged: true);
            //GC.SuppressFinalize(this);
        }

        private bool isEmpty = false;

        protected virtual void Cleanup(bool isClearManaged)
        {
            if (!isEmpty)
            {
                if (isClearManaged)
                {
                    // free managed resources
                }

                // free unmanaged resources

                isEmpty = true;
            }
        }
    }

    class Bomb
    {
        private readonly ILoggerService loggerService;

        public List<PointsProducer> Producers { get; set; }

        public Bomb(ILoggerService loggerService)
        {
            this.Producers = new List<PointsProducer>();
            this.loggerService = loggerService;
        }

        public void Fire(object sender, IPoint e)
        {
            this.loggerService.Info($"!@#$%%% fired for {sender.GetType()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("SampleTextLogger");

            var loggerService = new LoggerService(logger);

            var quadraticProducer = new QuadraticCurvePointsProducer(loggerService);
            var cubicProducer = new CubicCurvePointsProducer(loggerService);
            var squareProducer = new SquareCurvePointsProducer(loggerService);

            var bomb = new Bomb(loggerService);

            quadraticProducer.OnZeroXProduced += bomb.Fire;
            cubicProducer.OnZeroXProduced += bomb.Fire;

            bomb.Producers.Add(quadraticProducer);

            Task.Run(() => 
            {
                quadraticProducer.Run((point) => loggerService.Info($"Quadratic Function {point}"));
            });

            Task.Run(() =>
            {
                cubicProducer.Run((point) => loggerService.Info($"Cubic Function {point}"));
            });

            Task.Run(() =>
            {
                squareProducer.Run((point) => loggerService.Info($"Square Function {point}"));
            });


            System.Threading.Thread.Sleep(5000);
            quadraticProducer.IsContinue = false;

            System.Threading.Thread.Sleep(5000);
            cubicProducer.IsContinue = false;

            System.Threading.Thread.Sleep(5000);
            squareProducer.IsContinue = false;

            System.Threading.Thread.Sleep(2000);

            loggerService.Info("Remove pointers to producers");
            quadraticProducer = null;
            cubicProducer = null;
            squareProducer = null;

            loggerService.Info("Run a IDisposable object...");

            Task.Run(() =>
            {
                using (var sinCuvrePointsProducer = new SinCurvePointsProducer(loggerService))
                {
                    Task.Run(() =>
                    {
                        sinCuvrePointsProducer.Run((point) => loggerService.Info($"Sin Function {point}"));
                    });

                    System.Threading.Thread.Sleep(5000); ;

                    sinCuvrePointsProducer.IsContinue = false;
                }
            });

            Console.WriteLine("Press enter to run GC, step 1:");
            Console.ReadLine();
            GC.Collect();
            Console.WriteLine("Press enter to run GC, step 1:done");
            Console.ReadLine();
        }
         
    }
}
