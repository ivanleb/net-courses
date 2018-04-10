using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;

namespace Events.Implementations
{
    class ConsoleUserInput : IUserInput
    {
        public event InputEventHandler OnInput;

        public void ListenToUser()
        {
            InputEventArgs arg = new InputEventArgs();
            while (arg.input.Key != ConsoleKey.Escape)
            {
                arg.input = Console.ReadKey(true);
                OnInput?.Invoke(this, arg);
            }
        }
    }
}
