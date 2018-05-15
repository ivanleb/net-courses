using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Implementations;
using ORMCore.Abstractions;

namespace ORM.Implementations
{
    class UserInput: IUserInput
    {
        ConsoleKey stopKey;

        public UserInput(ConsoleKey stopKey)
        {
            this.stopKey = stopKey;
        }

        public event EventHandler<CommandEventArgs> OnKeyPressed;

        public void ListenToUser()
        {
            var k = new ConsoleKeyInfo();
            while(k.Key != stopKey)
            {
                k = Console.ReadKey();
                CommandEventArgs commandEventArgs = new CommandEventArgs() { PressedKey = k };
                OnKeyPressed?.Invoke(this, commandEventArgs);
            }
        }
    }
}
