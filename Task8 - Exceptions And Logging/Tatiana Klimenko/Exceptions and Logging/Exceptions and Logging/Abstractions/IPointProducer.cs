using System;

namespace Exceptions_and_Logging.Abstractions
{
    interface IPointProducer
    {
        bool IsContinue { get; set; }

        void Run(Action<IPoint> onPointReceive);

        event EventHandler<IPoint> OnPointProduced;
    }
}
