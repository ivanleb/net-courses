using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAndLoggingTask8.Abstractions
{
    public interface IClient
    {
        string Name { get; set; }

        void StartListenProducer(IPointProducer producer);

        void PointProducedHandler(object sender, IPoint p);
    }
}
