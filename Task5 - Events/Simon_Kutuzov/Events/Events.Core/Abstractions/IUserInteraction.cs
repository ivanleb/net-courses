using System;

namespace Events.Core.Abstractions
{
    public interface IUserInteraction
    {
        void StartListening();
        event EventHandler<EventArgs> InputReceived;
    }
}
