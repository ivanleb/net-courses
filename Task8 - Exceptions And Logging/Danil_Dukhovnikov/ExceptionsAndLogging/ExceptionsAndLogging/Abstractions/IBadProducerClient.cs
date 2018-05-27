using ExceptionsAndLogging.Implementations;

namespace ExceptionsAndLogging.Abstractions
{
    internal interface IBadProducerClient
    {
        void StartListenToBadProducer(BadPointProducer producer);
    }
}