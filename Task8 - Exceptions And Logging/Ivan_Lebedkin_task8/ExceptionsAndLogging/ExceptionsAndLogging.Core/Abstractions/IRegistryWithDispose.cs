using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionsAndLogging.Core.Abstractions
{
    public interface IRegistry<T> where T : IDisposable, IPointProducer
    {
        IEnumerable<T> PointProducers { get; set; }
    }
}
