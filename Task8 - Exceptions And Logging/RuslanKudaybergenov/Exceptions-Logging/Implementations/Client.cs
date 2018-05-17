using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions_Logging.Abstractions;

namespace Exceptions_Logging.Implementations
{
    public class Client
    {
        public void StartListenToBadProducer(BadProducer badProducer)
        {
            badProducer.onPointXProducer += this._onPointProducer;
        }
        
        void _onPointProducer(object sender, IPoint point)
        {
            Console.WriteLine($"Client point: {point}");
        }
    }

}
