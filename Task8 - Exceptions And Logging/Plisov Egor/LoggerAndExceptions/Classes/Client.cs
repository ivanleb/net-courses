using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerAndExceptions
{
    class Client : IClient
    {
        public string Name { get; set; }

        public Client (string name)
        {
            Name = name;
        }

        public void PointProducedHandler(object sender, IPoint point)
        {
            Console.WriteLine($"{Name} recived point {point}");
        }

        public void StartListening(IPointProducer producer)
        {
            producer.OnPointProduced += PointProducedHandler;
        }
    }
}
