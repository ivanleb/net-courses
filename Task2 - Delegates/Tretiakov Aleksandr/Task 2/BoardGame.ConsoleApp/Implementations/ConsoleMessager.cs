using BoardGame.Core.Abstractions;
using System;

namespace BoardGame.ConsoleApp.Implementations;
{
    public class ConsoleMessager : IMessager
    {
        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowInfromtaion(string message)
        {
            Console.WriteLine(message);
        }
    }
}
