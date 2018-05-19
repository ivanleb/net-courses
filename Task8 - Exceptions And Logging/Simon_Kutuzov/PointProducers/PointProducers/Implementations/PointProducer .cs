using System;
using PointProducers.Abstractions;

namespace PointProducers.Implementations
{
    public abstract class PointProducer : IPointProducer
    {
        protected readonly ILoggerService loggerService;
        protected double x;
        protected double y;
        public bool KeepRunning { get; set; }
        public event EventHandler<IPoint> OnPointProduced;

        public abstract IPoint BuildPoint();

        protected PointProducer(ILoggerService logger)
        {
            this.KeepRunning = true;
            this.loggerService = logger;
        }

        public void Run(Action<IPoint> onPointProduced)
        {
            while (KeepRunning)
            {
                var point = loggerService.RunWithExceptionLogging(BuildPoint, true);

                if (point != null)
                {
                    onPointProduced(point);
                    OnPointProduced?.Invoke(this, point);
                }

                System.Threading.Thread.Sleep(1000);
            }

            loggerService.Info($"{this.GetType()} is done.");
        }
    }
}
