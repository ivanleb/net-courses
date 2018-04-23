using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionAndLoggingTask8.Abstractions;

namespace ExceptionAndLoggingTask8
{
    class Client: IClient
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Name} recieved point{p}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
