using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public abstract class BadProducer : IPointProducer
    {
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
                decimal x = rnd.Next(-5, 25);
                var point = BuildPoint(x);
                System.Threading.Thread.Sleep(1000);  
                if (x == 0) continue;
                if (x < 0) continue;
                OnPointProduced?.Invoke(this, point);
                                                              
            }
        }
    }
}
