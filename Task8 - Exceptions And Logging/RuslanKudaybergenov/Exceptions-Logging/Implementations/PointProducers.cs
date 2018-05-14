using System;
using Exceptions_Logging.Abstractions;

namespace Exceptions_Logging.Implementations
{
    public abstract class PointProducers : IDisposable, IPointProducer
    {
        public event EventHandler<IPoint> onPointXProducer;
        protected virtual void PointXProducer(IPoint e)
        {
            EventHandler<IPoint> handler = onPointXProducer;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<IPoint> onPointYProducer;
        protected virtual void PointYProducer(IPoint e)
        {
            EventHandler<IPoint> handler = onPointYProducer;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected readonly ILogable loggerService;

        public abstract IPoint BuildPoint(double x);

        public bool IsContinue { get; set; }

        protected PointProducers(ILogable loggerService, string instanceName)
        {
            IsContinue = true;
            this.instanceName = instanceName;
            this.loggerService = loggerService;
        }

        public double startX = -4;
        public double stepX = 0.5;

        virtual public void Run(Action<IPoint> onPointReceive)
        {

            while (IsContinue)
            {
                var point = this.BuildPoint(startX += stepX);

                PointXProducer(point);
                PointYProducer(point);
                onPointReceive(point);

                System.Threading.Thread.Sleep(1000);

            }

            this.loggerService.Info($"{this.GetType()} -> Going to exit...");
            Dispose();

        }

        private bool isDisposed = false;

        protected string instanceName;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    Console.WriteLine($"[{this.instanceName}] Removed");
                }
                else
                {
                    Console.WriteLine($"[{instanceName}].Base.Dispose(false)");
                }
                isDisposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~PointProducers()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }

    public class LinearFunctionProducer : PointProducers
    {
        public LinearFunctionProducer(ILogable loggerService, string instanceName) : base(loggerService, instanceName)
        {
        }

        ~LinearFunctionProducer()
        {
            this.loggerService.Info($"{this.instanceName} will remove from heap");
            this.Dispose(false);
        }

        public override IPoint BuildPoint(double x) => new Point { X = x, Y = x };

    }

    public class CubeFunctionProducer : PointProducers
    {
        public CubeFunctionProducer(ILogable loggerService, string instanceName) : base(loggerService, instanceName)
        {
        }

        ~CubeFunctionProducer()
        {
            this.loggerService.Info($"{this.instanceName} will remove from heap");
            this.Dispose(false);
        }

        public override IPoint BuildPoint(double x) => new Point { X = x, Y = x * x * x };
    }

    public class BadProducer : PointProducers
    {
        public BadProducer(ILogable loggerService, string instanceName) : base(loggerService, instanceName)
        {
        }
        ~BadProducer()
        {
            this.loggerService.Info($"{this.instanceName} will remove from heap");
            this.Dispose(false);
        }
        public override IPoint BuildPoint(double x)
        {
            return loggerService.RunWithExceptionLogging<IPoint>(() =>
            {
                if (x == 0)
                {
                    IsContinue = false;
                    throw new ArgumentException($"X is invalid value={x}", "x");
                }
                return new Point { X = x, Y = x };
            }, true);
        }

    }
}


