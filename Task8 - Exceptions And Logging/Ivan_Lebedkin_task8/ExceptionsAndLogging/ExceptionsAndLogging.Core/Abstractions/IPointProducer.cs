using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsAndLogging.Core.Abstractions
{
    public interface IPointProducer
    {
        bool IsContinue { get; set; }
        void Run(Action<IPoint> onPointReceive);
        ILoggerService GetLoggerService();
    }
}
