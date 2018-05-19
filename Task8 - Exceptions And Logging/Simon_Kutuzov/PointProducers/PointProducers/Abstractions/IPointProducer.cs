using System;

namespace PointProducers.Abstractions
{
    public interface IPointProducer
    {
        bool KeepRunning { get; set; }
        event EventHandler<IPoint> OnPointProduced;
        void Run(Action<IPoint> onPointProduced);
    }
}
