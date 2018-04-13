using System;
using Delegates.Core.Abstractions;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleDrawing : IDrawing
    {
        private static void WriteAt(int x, int y, char s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

        private static void ReturnCursor(IBoard board)
        {
            Console.SetCursorPosition(0, board.BoardSizeY + 3
                                         );
        }
        
        public void DrawPoint(IBoard board)
        {
            WriteAt(board.BoardSizeX / 5, board.BoardSizeY / 5, '+');
            ReturnCursor(board);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeX; i++)
            {
                WriteAt(i, board.BoardSizeY / 2, '-');
            }
            
            ReturnCursor(board);
        }

        public void DrawVerticalLine(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeY; i++)
            {
                WriteAt(board.BoardSizeX / 2, i, '|');
            }
            
            ReturnCursor(board);
        }

        public void DrawBoard(IBoard board)
        {
            Console.Clear();
            
            WriteAt(0, 0, '+');
            WriteAt(0, board.BoardSizeY, '+');
            WriteAt(board.BoardSizeX, 0, '+');
            WriteAt(board.BoardSizeX, board.BoardSizeY, '+');
            
            for (var i = 1; i < board.BoardSizeX; i++)
            {
                WriteAt(i, 0, '-');
                WriteAt(i, board.BoardSizeY, '-');
            }

            for (var i = 1; i < board.BoardSizeY; i++)
            {
                WriteAt(0, i, '|');
                WriteAt(board.BoardSizeX, i, '|');
            }
            
            ReturnCursor(board);
        }
    }
}