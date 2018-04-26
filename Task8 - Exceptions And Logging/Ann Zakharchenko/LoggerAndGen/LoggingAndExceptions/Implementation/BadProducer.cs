using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingAndExceptions.Abstracrions;

namespace LoggingAndExceptions.Implementation
{
    class BadProducer : PointProducer
    {
        public event EventHandler<IPoint> OnGoodPointReceived; 

        public BadProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        public new void Run(Action<IPoint> onPointReceive)
        {
            IsContinue = true;
            decimal x = 12;
            while (IsContinue)
            {
                IPoint point = BuildPoint(x++);
                if (point != null)
                {
                    onPointReceive(point);
                    OnGoodPointReceived?.Invoke(this, point);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        public override IPoint BuildPoint(decimal x)
        {
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = x,
                    Y = (decimal)Math.Sqrt((double)(x *x*x - 1))/((decimal)Math.Sqrt((double)(x - 6M))*(x*x - 9M))
                };
            }, isSilent: true);
        }
    }
}
