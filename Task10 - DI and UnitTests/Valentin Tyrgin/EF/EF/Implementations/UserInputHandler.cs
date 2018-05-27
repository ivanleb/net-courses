using System;
using EF.Core.Abstractions;
using EF.Core.Implementations;

namespace EF.Implementations
{
    internal class UserInputHandler : IUserInputHandler
    {
        private ITransactionGenerator generator;
        public ConsoleKey Start { get; set; }
        public ConsoleKey Pause { get; set; }
        public ConsoleKey Stop { get; set; }

        public override string ToString()
        {
            return $"Для запуска нажмите: {Start}\n" +
                   $"Для паузы нажмите: {Pause}\n" +
                   $"Для выхода нажмите: {Stop}";
        }

        public UserInputHandler(ITransactionGenerator generator)
        {
            this.generator = generator;
        }

        public async void ProcessUserCommand(object sender, EventArgs args)
        {
            if (((KeyEventArgs)args).KeyPressed == Stop || ((KeyEventArgs)args).KeyPressed == Pause)
            {
                generator.Active = false;
            }
            if (((KeyEventArgs)args).KeyPressed == Start&&generator.Active==false)
            {
                generator.Active = true;
                await generator.Generate();
            }
        }
    }
}