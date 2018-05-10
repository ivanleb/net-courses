using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{
    class MinusProducer : MainPointProducer
    {
        public MinusProducer(ILoggerService logger) : base(logger)
        {
        }

        public override IPoint CreatePoint(int a)
        {
            return new Point
            {
                X = a,
                Y = a - a
            };
        }
    }
}
