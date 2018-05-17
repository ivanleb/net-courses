using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class UserInput : IUserInput
    {
        public event EventHandler<EventArgs> InputReceived;

        public void StartListen()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, StaticRegistry.board.Height + 1);
                    break;
                }

                InputReceived?.Invoke(this, new TurnEventArgs { ReceivedCommand = key });
            }
        }
    }
}
