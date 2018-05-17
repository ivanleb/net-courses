namespace Exceptions_and_Logging.Abstractions
{
    interface IClient
    {
        string Name { get; set; }

        void StartListenProducer(IPointProducer producer);

        void PointProducedHandler(object sender, IPoint p);
    }
}
