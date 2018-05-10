using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{
    class BadProducer : MainPointProducer
    {
        public BadProducer(ILoggerService logger) : base(logger)
        {
        }

        public override IPoint CreatePoint(int a)
        {
            Random rnd = new Random();
            return loggerService.RunWithExceptionLogging(() =>
            {
                return new Point
                {
                    X = a,
                    Y = a / rnd.Next(0, 3)
                };
            }, isSilent: true);
        }
    }
}
