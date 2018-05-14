using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Exceptions_and_Logging.Core;

namespace Exceptions_and_Logging.Implementation
{
    internal abstract class PointProducer:IPointProducer
    {
        protected static Random random = new Random();
        public bool IsContinue { get; set; } = true;
        public event EventHandler<IPoint> OnPointProduced;
        public abstract IPoint CreatePoint();
        public void Run(Action<IPoint> onPointAction)
        {
            while (IsContinue)
            {
                var pt = Logger.RunWithExceptionLogging(CreatePoint,true);
                if (pt != null)
                {
                    onPointAction(pt);
                    OnPointProduced?.Invoke(this,pt);
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
