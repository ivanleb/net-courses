using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp.Implementations
{
    public class ItemsBuilder : IItemsBuilder
    {
        public void DrawHorizontalLine(IBoard board)
        {
            int currentLineCursor = Console.CursorTop;
            for (int i = 1; i < board.BoardSizeX - 1; i++)
            {
                ConsoleOutput.WriteAt("-", i, board.BoardSizeY / 2);
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void DrawPoint(IBoard board)
        {
            int currentLineCursor = Console.CursorTop;
            ConsoleOutput.WriteAt("+", board.BoardSizeX / 4, (board.BoardSizeY / 4));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void DrawVerticalLine(IBoard board)
        {
            int currentLineCursor = Console.CursorTop;
            for (int i = 1; i < board.BoardSizeY - 1; i++)
            {
                ConsoleOutput.WriteAt("|", board.BoardSizeX / 2, i);
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
