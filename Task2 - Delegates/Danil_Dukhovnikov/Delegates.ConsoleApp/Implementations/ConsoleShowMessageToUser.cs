using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleShowMessageToUser : IShowMessageToUser
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowMenu()
        {
            const string menuAsString = @"1. Draw point
2. Draw Horizontal line
3. Draw Vertical line
4. Clear
5. Exit";
            Console.WriteLine(menuAsString);
        }
    }
}