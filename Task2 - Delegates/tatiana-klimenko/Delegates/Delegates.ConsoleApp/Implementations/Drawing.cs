using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class Drawing : IDrawing
    {
        private static int origRow;
        private static int origCol;

        public void DrawBoard(IBoard board)
        {
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            //corners
            WriteAt("+", 0, 0);
            WriteAt("+", 0, board.BoardSizeY - 1);
            WriteAt("+", board.BoardSizeX - 1, 0);
            WriteAt("+", board.BoardSizeX - 1, board.BoardSizeY - 1);
            //top line
            for (int x = 1; x < board.BoardSizeX - 1; x++)
            {
                WriteAt("-", x, 0);
            }
            //bottom line
            for (int x = 1; x < board.BoardSizeX - 1; x++)
            {
                WriteAt("-", x, board.BoardSizeY - 1);
            }
            //left line
            for (int y = 1; y < board.BoardSizeY - 1; y++)
            {
                WriteAt("|", 0, y);
            }
            //right line
            for (int y = 1; y < board.BoardSizeY - 1; y++)
            {
                WriteAt("|", board.BoardSizeX - 1, y);
            }
            Console.WriteLine();
        }

        private static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DrawDot(IBoard board)
        {
            int currentRow = Console.CursorTop;
            WriteAt("+", board.BoardSizeX / 4, board.BoardSizeY / 4);
            Console.SetCursorPosition(0, currentRow);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            int currentRow = Console.CursorTop;
            for (int x = 1; x < board.BoardSizeX - 1; x++)
            {
                WriteAt("-", x, board.BoardSizeY / 2);
            }
            Console.SetCursorPosition(0, currentRow);
        }

        public void DrawVerticalLine(IBoard board)
        {
            int currentRow = Console.CursorTop;
            for (int y = 1; y < board.BoardSizeY - 1; y++)
            {
                WriteAt("|", board.BoardSizeX / 2, y);
            }
            Console.SetCursorPosition(0, currentRow);
        }
    }
}
