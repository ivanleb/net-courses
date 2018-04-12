using System;
using Events.Core.Abstractions;

namespace Events.ConsoleApp.Implementations
{
    public class ConsoleUserInteraction : IUserInteraction
    {
        public event EventHandler<EventArgs> InputReceived;
        
        public void StartListening()
        {
            while (true)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, StaticRegistry.board.Height + 1);
                    break;
                }

                InputReceived?.Invoke(this, new CommandEventArgs { ReceivedCommand = key });
            }
        }
    }
}
