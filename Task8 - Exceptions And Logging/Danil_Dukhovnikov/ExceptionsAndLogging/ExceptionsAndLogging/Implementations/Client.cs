using System;
using ExceptionsAndLogging.Abstractions;

namespace ExceptionsAndLogging.Implementations
{
    internal class Client : IBadProducerClient
    {
        private readonly string _name;

        public Client(string name)
        {
            this._name = name;
        }

        public void StartListenToBadProducer(BadPointProducer producer)
        {
            producer.OnGoodPointProduced += this.Producer_OnGoodPointProduced;
        }

        private void Producer_OnGoodPointProduced(object sender, IPoint e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Client {this._name} has just recived point {e}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
