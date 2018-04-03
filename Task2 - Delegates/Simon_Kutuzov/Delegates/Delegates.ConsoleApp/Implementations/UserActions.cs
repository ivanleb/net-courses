using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class UserActions : IUserActions
    {
        public void SetBoardSize(IBoard board)
        {
            string[] tokens = Console.ReadLine().Split();
            board.Width = int.Parse(tokens[0]);
            board.Height = int.Parse(tokens[1]);
        }

        public int SelectMenuOption()
        {
            int currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, currentLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);

            return int.Parse(Console.ReadLine());
        }
    }
}
