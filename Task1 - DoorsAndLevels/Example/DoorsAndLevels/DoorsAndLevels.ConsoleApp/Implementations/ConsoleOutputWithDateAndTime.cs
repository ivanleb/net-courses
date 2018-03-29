using DoorsAndLevels.Core.Abstractions;
using System;

namespace DoorsAndLevels.ConsoleApp.Implementations
{
    public class ConsoleOutputWithDateAndTime : IShowMessageToUser
    {
        public void ShowDoorNumbers(int[] doorNumber)
        {
            Console.WriteLine($"->{string.Join("|", doorNumber)}-<");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} -> {message}");
        }
    }
}
