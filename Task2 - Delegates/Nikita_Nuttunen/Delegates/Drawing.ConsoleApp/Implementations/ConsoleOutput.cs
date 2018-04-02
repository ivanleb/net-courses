using Drawing.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.ConsoleApp.Implementations
{
    public class ConsoleOutput : IShowMessageToUser
    {
        static int BoardSizeX;
        static int BoardSizeY;
        static int BoardRowNumber;
        static int cursorBottomPosition;

        public void DrawBoard(IBoard board)
        {
            BoardSizeX = board.BoardSizeX;
            BoardSizeY = board.BoardSizeY;
            BoardRowNumber = Console.CursorTop;

            Console.WriteLine();
            DrawLeftSide();
            DrawBottomSide();
            DrawRightSide();
            DrawTopSide();
            Console.SetCursorPosition(0, cursorBottomPosition);
        }

        static void DrawLeftSide()
        {
            WriteAt("+", 0, 0);
            for (int yPosition = 1; yPosition < BoardSizeY - 1; yPosition++)
            {
                WriteAt("|", 0, yPosition);
            }
            WriteAt("+", 0, BoardSizeY - 1);
        }

        static void DrawBottomSide()
        {
            for (int xPosition = 1; xPosition < BoardSizeX; xPosition++)
            {
                WriteAt("-", xPosition, BoardSizeY - 1);
            }
            WriteAt("+", BoardSizeX - 1, BoardSizeY - 1);
            cursorBottomPosition = Console.CursorTop + 1;
        }

        static void DrawRightSide()
        {
            for (int yPosition = BoardSizeY - 2; yPosition > 0; yPosition--)
            {
                WriteAt("|", BoardSizeX - 1, yPosition);
            }
            WriteAt("+", BoardSizeX - 1, 0);
        }

        static void DrawTopSide()
        {
            for (int xPosition = BoardSizeX - 2; xPosition >= 1; xPosition--)
            {
                WriteAt("-", xPosition, 0);
            }
        }

        public static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, BoardRowNumber + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ClearBoard()
        {    
            int screenEndRow = Console.CursorTop;
            for (int yPosition = 1; yPosition < BoardSizeY - 1; yPosition++)
            {
                Console.SetCursorPosition(1, yPosition + BoardRowNumber);
                Console.Write(new string(' ', BoardSizeX - 2));
            }
            Console.SetCursorPosition(0, screenEndRow);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void ClearInput()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
