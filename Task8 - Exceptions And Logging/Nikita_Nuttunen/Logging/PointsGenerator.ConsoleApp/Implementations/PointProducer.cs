using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public abstract class PointProducer : IPointProducer
    {
        Random rnd = new Random();

        protected readonly ILoggerService loggerService;

        public bool IsContinue { get; set; }

        public abstract IPoint BuildPoint(decimal x);

        protected PointProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public void Run(Action<IPoint> onPointReceiver)
        {
            IsContinue = true;
            while (IsContinue)
            {
                decimal x = rnd.Next(-1, 1);
                var point = BuildPoint(x);

                onPointReceiver(point);

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
