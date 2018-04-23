using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionsAndLogging.Core.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    public class SinCurvePointsProducer : PointsProducer, IDisposable
    {
        public SinCurvePointsProducer(ILoggerService loggerService) : base(loggerService)
        {
        }

        ~SinCurvePointsProducer()
        {
            this.loggerService.Info($"SinCurvePointsProducer will remove from heap");
            this.Cleanup(isClearManaged: false);
        }

        public override IPoint BuildPoint(decimal x)
        {
            return new Point
            {
                X = x,
                Y = (decimal)Math.Sin((double)x)
            };
        }

        public void Dispose()
        {
            // see the best practice at https://msdn.microsoft.com/ru-ru/library/b1yfkh5e(v=vs.100).aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1
            this.Cleanup(isClearManaged: true);
            GC.SuppressFinalize(this);
        }

        private bool isEmpty = false;

        protected virtual void Cleanup(bool isClearManaged)
        {
            if (!isEmpty)
            {
                if (isClearManaged)
                {
                    // free managed resources
                    Console.WriteLine("disposing here");
                }

                // free unmanaged resources

                isEmpty = true;
            }
        }
    }
}
