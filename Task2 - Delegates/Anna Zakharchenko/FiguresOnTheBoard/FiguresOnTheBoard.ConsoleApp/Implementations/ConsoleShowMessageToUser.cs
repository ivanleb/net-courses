using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class ConsoleShowMessageToUser : IShowMessageToUser
    {
        public void ShowInstructionForUser(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowHelloToUser()
        {
            Console.WriteLine();

            string message = "Hello!\n" +
                "Choose an option from the list:\n" +
                "1 - draw a point\n" +
                "2 - draw a vertical line\n" +
                "3 - draw a horizontal line\n" +
                "0 - quit the game\n";

            Console.WriteLine(message);
        }
    }
}
