using BoardGame.Core.Abstractions;
using System;

namespace BoardGame.ConsoleApp.Implementations;
{
    public class ConsoleProcessUserInput : IProcessUserInput
    {
        private readonly IMessager _messager;
        public ConsoleProcessUserInput(IMessager messager)
        {
            _messager = messager;
        }

        public UserChoise GetUserChoise()
        {
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    _messager.ShowError("Invalid input. It must be digits.");
                    continue;
                }

                if (!"1 2 3 4".Contains(input.ToString()))
                {
                    _messager.ShowError("Invalid input. Only 1 to 4 allowed.");
                    continue;
                }

                return (UserChoise)(input - 1);
            }
        }
    }
}
