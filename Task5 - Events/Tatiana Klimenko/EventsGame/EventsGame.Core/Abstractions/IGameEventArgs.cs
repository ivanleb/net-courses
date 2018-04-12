using System;

namespace EventsGame.Core.Abstractions
{
    public interface IGameEventArgs
    {
        ConsoleKeyInfo ReceivedCommand { get; set; }
    }
}
