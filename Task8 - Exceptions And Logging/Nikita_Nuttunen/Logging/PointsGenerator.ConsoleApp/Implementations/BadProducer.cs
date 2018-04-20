using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public abstract class BadProducer : IPointProducer
    {
        public event Action<object, IPoint> OnNegativeXProduced;
        public event Action<object, IPoint> OnZeroXProduced;
        public event Action<object, IPoint> OnPointProduced;

        Random rnd = new Random();

        protected readonly ILoggerService loggerService;

        public bool IsContinue { get; set; }

        public abstract IPoint BuildPoint(decimal x);

        protected BadProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public void Run(Action<IPoint> onPointReceiver)
        {
            IsContinue = true;
            while (IsContinue)
            {
                decimal x = rnd.Next(-5, 20);
                var point = BuildPoint(x);

                if (point.X == 0) OnZeroXProduced?.Invoke(this, point);
                if (point.X < 0) OnNegativeXProduced?.Invoke(this, point);
                                
                onPointReceiver(point);
                OnPointProduced?.Invoke(this, point);

                System.Threading.Thread.Sleep(1000);                
            }

        }
    }
}
