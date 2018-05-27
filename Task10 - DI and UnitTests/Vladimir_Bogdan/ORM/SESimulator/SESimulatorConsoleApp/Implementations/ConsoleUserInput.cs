using SESimulator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESimulatorConsoleApp.Implementations
{
    class ConsoleUserInput : IUserInput
    {
        private ConsoleKey keyToStopListening;

        public ConsoleUserInput(ConsoleKey keyToStopListening)
        {
            this.keyToStopListening = keyToStopListening;
        }

        public event EventHandler<ConsoleKey> OnUserInputRecieved;

        public void ListenToUser()
        {
            var c = new ConsoleKeyInfo();
            while (c.Key != this.keyToStopListening)
            {
                c = Console.ReadKey(true);
                OnUserInputRecieved?.Invoke(this, c.Key);
            }
        }
    }
}
