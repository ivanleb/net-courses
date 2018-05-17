using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    abstract public class PointsProducer : IPointProducer
    {
        protected Random rnd;
        protected readonly ILoggerService loggerService;
        public event EventHandler<IPoint> onPointReceive;
        public bool IsContinue { get; set; }
        public abstract IPoint BuildPoint(decimal x);

        public PointsProducer(ILoggerService loggerService)
        {
            rnd = new Random();
            this.loggerService = loggerService;
        }
        public ILoggerService GetLoggerService()
        {
            return loggerService;
        }
        virtual public void Run(Action<IPoint> onPointReceive)
        {
            IsContinue = true;

            decimal x = 1; // rnd.Next(10);

            while (IsContinue)
            {
                var point = this.BuildPoint(x++);
                if (point != null)
                {
                    onPointReceive(point);//передача в логгер
                }
                System.Threading.Thread.Sleep(500);
            }

            this.loggerService.Info($"{this.GetType()} -> Going to exit...");
        }


    }
    
}
