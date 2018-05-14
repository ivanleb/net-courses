using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{
    public interface IClient
    {
        string Name { get; set; }

        void StartListening(IPointProducer producer);

        void PointProducedHandler(object sender, IPoint point);
    }
}
