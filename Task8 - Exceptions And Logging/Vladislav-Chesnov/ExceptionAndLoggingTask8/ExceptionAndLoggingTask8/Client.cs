using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAndLoggingTask8
{
    class Client
    {
        string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }

        public void StartListenProducer(PointProducer producer)
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
