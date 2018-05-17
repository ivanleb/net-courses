using PointsGenerator.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointsGenerator.ConsoleApp.Implementations
{
    public class Registry : IRegistry
    {
        public ILoggerService LoggerService { get; set; }
        public IList<IPointProducer> PointProducers { get; set; }
    }
}
