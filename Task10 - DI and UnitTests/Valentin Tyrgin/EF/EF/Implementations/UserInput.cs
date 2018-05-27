using System;
using EF.Core.Abstractions;
using EF.Core.Implementations;

namespace EF.Implementations
{
    internal class UserInput : IUserInput
    {
        private IUserInputHandler _uh;

        public UserInput(IUserInputHandler userHandler)
        {
            _uh = userHandler;
        }
        public void ListenToUser()
        {
            ConsoleKey keyPressed = default(ConsoleKey);
            while (keyPressed != _uh.Stop)
            {
                keyPressed = Console.ReadKey().Key;
                KeyPressed?.Invoke(this, new KeyEventArgs {KeyPressed = keyPressed});
            }
        }

        public event EventHandler KeyPressed;
    }
}