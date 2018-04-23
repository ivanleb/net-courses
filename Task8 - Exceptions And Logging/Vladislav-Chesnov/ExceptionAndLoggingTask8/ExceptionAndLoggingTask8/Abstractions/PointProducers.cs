using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAndLoggingTask8.Abstractions
{
    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        void Run(Action<IPoint> onPointRecieve, decimal startingValue, decimal step);
        event EventHandler<IPoint> OnPointProduced;
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
                if (point != null)
                {
                    OnPointProduced?.Invoke(this, point);
                }
                x += step;
                System.Threading.Thread.Sleep(1000);
            }

            loggerService.Info($"{GetType()} -> Going to exit");
        }
    }
}
