using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{

    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        void Start(Action<IPoint> onPointRecieve, int startValue, int step);
        event EventHandler<IPoint> OnPointProduced;
    }

    abstract class MainPointProducer : IPointProducer
    {
        public event EventHandler<IPoint> OnPointProduced;
        protected readonly ILoggerService loggerService;

        protected MainPointProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public bool IsContinue { get; set; }

        public abstract IPoint CreatePoint(int X);

        public void Start(Action<IPoint> onPointRecieve, int startValue, int step)
        {
            int a = startValue;
            IsContinue = true;

            while (IsContinue)
            {
                IPoint point = CreatePoint(a);
                onPointRecieve(point);
                if (point != null)
                {
                    OnPointProduced?.Invoke(this, point);
                }
                a += step;
                System.Threading.Thread.Sleep(1000);
            }

            loggerService.Info($"{GetType()} -> to exit");
        }

    }
}
