using System;

namespace Events.Core.Abstractions
{
    public interface IHero
    {
        int X { get; set; }
        int Y { get; set; }
        char Marker { get; }

        void StartListeningInput(IUserInteraction input);
        event EventHandler<EventArgs> HeroMoved;
    }
}