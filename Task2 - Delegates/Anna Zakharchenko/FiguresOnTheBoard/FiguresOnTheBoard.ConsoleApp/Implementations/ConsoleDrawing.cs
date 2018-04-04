using System;
using FiguresOnTheBoard.Core.Abstractions;

namespace FiguresOnTheBoard.ConsoleApp.Implementations
{
    public class ConsoleDrawing : IDrawing
    {
        public void DriwInPosition(string symbol, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawBoard(IBoard board)
        {
            Console.Clear();
            DriwInPosition("+", 1, 1);
            DriwInPosition("+", 1, board.BoardSizeY);
            DriwInPosition("+", board.BoardSizeX, 1);
            DriwInPosition("+", board.BoardSizeX, board.BoardSizeY);

            for (int i = 2; i < board.BoardSizeX; i++)
            {
                DriwInPosition("-", i, 1);
                DriwInPosition("-", i, board.BoardSizeY);
            }

            for (int j = 2; j < board.BoardSizeY; j++)
            {
                DriwInPosition("|", 1, j);
                DriwInPosition("|", board.BoardSizeX, j);
            }
            Console.WriteLine();

        }

        public void DrawDot(IBoard board)
        {
            int x = (((board.BoardSizeX + 1) / 2) + 1) / 2;
            int y = ((board.BoardSizeY + 1) / 2 + 1) / 2;
            DriwInPosition("+", x, y);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            int y = (board.BoardSizeY + 1) / 2;
            for (int i = 2; i <= board.BoardSizeX - 1; i++)
            {
                DriwInPosition("-", i, y);
            }
        }

        public void DrawVerticalLine(IBoard board)
        {
            int x = (board.BoardSizeX + 1) / 2;
            for (int i = 2; i <= board.BoardSizeY - 1; i++)
            {
                DriwInPosition("|", x, i);
            }
        }
    }
}
