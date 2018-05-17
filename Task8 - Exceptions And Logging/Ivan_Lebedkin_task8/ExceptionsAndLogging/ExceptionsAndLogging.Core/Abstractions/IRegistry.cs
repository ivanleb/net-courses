using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsAndLogging.Core.Abstractions
{
    public interface IRegistry
    {
        //IEnumerable<ILoggerService> LoggerServices { get; set; }
        IEnumerable<IPointProducer> PointProducers { get; set; }
    }
}
