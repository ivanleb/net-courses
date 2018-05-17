using System;
using Exceptions_and_Logging.Abstractions;

namespace Exceptions_and_Logging.Implementations
{
    class Client : IClient
    {
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }

        public void StartListenProducer(IPointProducer producer)
        {
            producer.OnPointProduced += PointProducedHandler;
        }

        public void PointProducedHandler(object sender, IPoint p)
        {
            Console.WriteLine($"Client {Name} recieved a point: {p}");
        }
        
    }
}
