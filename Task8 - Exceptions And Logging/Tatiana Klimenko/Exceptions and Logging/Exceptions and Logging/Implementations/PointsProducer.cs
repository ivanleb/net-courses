using System;
using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    abstract class PointsProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnPointProduced;

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

            decimal x = 1;

            while (IsContinue)
            {
                var point = this.BuildPoint(x++);
               
                onPointReceive(point);

                if (point != null)
                    OnPointProduced?.Invoke(this, point);

                System.Threading.Thread.Sleep(1000);
            }

            this.loggerService.Info($"{this.GetType()} -> Going to exit...");
        }
    }
}
