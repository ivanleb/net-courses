using System;
using ORM.Core.Abstractions;

namespace ORM.ConsoleApp.Implementations
{
    internal class ConsoleUserInput : IUserInput
    {
        private readonly ConsoleKey _keyToStopListening;

        public ConsoleUserInput(ConsoleKey keyToStopListening)
        {
            this._keyToStopListening = keyToStopListening;
        }

        public event EventHandler<ConsoleKey> OnUserInputRecieved;

        public void ListenToUser()
        {
            var c = new ConsoleKeyInfo();
            while (c.Key != this._keyToStopListening)
            {
                c = Console.ReadKey(true);
                OnUserInputRecieved?.Invoke(this, c.Key);
            }
        }
    }
}
