using System;
using Delegates.Core.Abstractions;

namespace Delegates.App.Implementations
{
    internal class Utils : IUtils
    {
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void WriteOutsideBoard(IBoard board, string s)
        {
            WriteAt(s, 0, board.Height + 1);
        }
        public void Clean()
        {
            Console.Clear();            
        }
    }
}