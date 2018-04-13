using System;
using Events.Core.Abstractions;

namespace Events.Console.Implementations
{
    public class ConsoleUserInteraction : IUserInteraction
    {
        public event EventHandler<EventArgs> InputReceived;

        public void StartListening()
        {
            while (true)
            {
                var key = System.Console.ReadKey().Key;
                
                if (key.Equals(ConsoleKey.Escape))
                {
                    break;
                }

                InputReceived?.Invoke(this, new CommandEventArgs {ReceivedCommand = key});
            }
        }
    }
}