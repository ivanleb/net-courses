using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingAndExceptions.Abstracrions;

namespace LoggingAndExceptions.Implementation
{
    abstract class PointProducer : IPointProducer
    {
        public bool IsContinue { get; set; }
        protected readonly ILoggerService loggerService;
        public abstract IPoint BuildPoint(decimal x);
        protected PointProducer(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public void Run(Action<IPoint> onPointReceive)
        {
            IsContinue = true;
            decimal x = -5;
            while (IsContinue)
            {
                IPoint point = BuildPoint(x++);
                if (point != null)
                {
                    onPointReceive(point);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
