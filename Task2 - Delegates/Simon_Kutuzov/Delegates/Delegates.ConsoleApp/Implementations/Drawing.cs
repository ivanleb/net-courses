using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class Drawing : IDrawing
    {
        int menuSize = 7;

        void ReturnCursor(IBoard board) { Console.SetCursorPosition(0, board.Height + menuSize); }

        void WriteAt(char s, int x, int y)
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

        public void DrawVecticalLine(IBoard board)
        {
            for (int y = 1; y < board.Height; y++)
            {
                WriteAt('|', board.Width / 2, y);
            }

            ReturnCursor(board);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            for (int x = 1; x < board.Width; x++)
            {
                WriteAt('-', x, board.Height / 2);
            }

            ReturnCursor(board);
        }

        public void DrawDot(IBoard board)
        {
            WriteAt('*', board.Width / 4, board.Height / 4);
            ReturnCursor(board);
        }

        public void Reset(IBoard board)
        {
            Console.Clear();

            WriteAt('+', 0, 0);
            WriteAt('+', 0, board.Height);
            WriteAt('+', board.Width, 0);
            WriteAt('+', board.Width, board.Height);

            for (int x = 1; x < board.Width; x++)
            {
                WriteAt('-', x, 0);
                WriteAt('-', x, board.Height);
            }

            for (int y = 1; y < board.Height; y++)
            {
                WriteAt('|', 0, y);
                WriteAt('|', board.Width, y);
            }

            Console.Write("\n\n");
        }
    }
}
