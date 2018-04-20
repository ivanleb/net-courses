using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.Core.Abstractions
{
    public interface IRegistry
    {
        ILoggerService LoggerService { get; set; }
        IList<IPointProducer> PointProducers { get; set; }
        IExceptionIndicator ExceptionIndicator { get; set; }
    }
}
