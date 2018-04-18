using System;
using Task5.Core.Abstractions;

namespace Task5.App.Implementations
{
    public class ConsoleProcessUserInput : IProcessUserInput
    {
        private readonly IMessenger _messager;

        public ConsoleProcessUserInput(IMessenger messager)
        {
            _messager = messager;
        }

        public void StartListening()
        {
            while (true)
            {
                var pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.W:
                        UserInput?.Invoke(this, UserChoise.Down);
                        break;
                    case ConsoleKey.S:
                        UserInput?.Invoke(this, UserChoise.Up);
                        break;
                    case ConsoleKey.A:
                        UserInput?.Invoke(this, UserChoise.Left);
                        break;
                    case ConsoleKey.D:
                        UserInput?.Invoke(this, UserChoise.Right);
                        break;
                    case ConsoleKey.R:
                        UserInput?.Invoke(this, UserChoise.Up);
                        break;
                    default:
                        _messager.ShowError("Invalid input. Try W, A, S, D.");
                        continue;
                }
            }
        }

        public event EventHandler<UserChoise> UserInput;
    }
}
