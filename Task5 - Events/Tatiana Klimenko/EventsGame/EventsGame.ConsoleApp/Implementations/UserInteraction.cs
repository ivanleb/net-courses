using System;
using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class UserInteraction : IUserInteraction
    {
        public event GameEventHandler InputReceived;

        public void StartListening()
        {
            while (true)
            {
                var keyInfo = Console.ReadKey();


                InputReceived?.Invoke(this, new GameEventArgs()
                {
                    ReceivedCommand = keyInfo
                });
            }
        }
    }
}
