using ExceptionsAndLogging.Abstractions;
using System;

namespace ExceptionsAndLogging
{
    interface IBadProducerClient
    {
        void StartListenToBadProducer(BadPointsProducer producer);
    }
    class Client : IBadProducerClient
    {
        private readonly string name;

        public Client(string name)
        {
            this.name = name;
        }

        public void StartListenToBadProducer(BadPointsProducer producer)
        {
            producer.OnGoodPointProduced += this.Producer_OnGoodPointProduced;
        }

        private void Producer_OnGoodPointProduced(object sender, IPoint e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Client {this.name} has just recived point {e}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
