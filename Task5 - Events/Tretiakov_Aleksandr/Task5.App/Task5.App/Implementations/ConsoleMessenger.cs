using System;
using Task5.Core.Abstractions;

namespace Task5.App.Implementations
{
    public class ConsoleMessenger : IMessenger
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
