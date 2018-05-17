namespace PointProducers.Abstractions
{
    public interface IClient
    {
        string Name { get; set; }
        void OnPointReceived(object sender, IPoint point);
    }
}
