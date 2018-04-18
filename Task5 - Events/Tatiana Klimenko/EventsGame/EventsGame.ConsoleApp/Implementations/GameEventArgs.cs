using System;
using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class GameEventArgs : IGameEventArgs
    {
        public ConsoleKeyInfo ReceivedCommand { get; set; }
    }
}
