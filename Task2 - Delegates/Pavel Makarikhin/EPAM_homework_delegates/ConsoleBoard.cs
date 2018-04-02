using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using Board;

namespace EPAM_homework_delegates
{
    class ConsoleBoard : IBoard
    {

        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }

        public static int BoardOriginX { get; set; }
        public static int BoardOriginY { get; set; }

        public ConsoleBoard(int sizeX, int sizeY, int originX = 0, int originY = 0)
        {
            BoardSizeX = sizeX;
            BoardSizeY = sizeY;
            BoardOriginX = originX;
            BoardOriginY = originY;
        }

        private static void WriteAt(string str, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(BoardOriginX + x, BoardOriginY + y);
                Console.Write(str);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void DrawHorizontalLine(int y, int lenght)
        {
            WriteAt("+", BoardOriginX,  y);
            WriteAt("+", BoardOriginX + lenght, y);

            for (int i = 1; i < lenght; i++)
                WriteAt("─", BoardOriginX + i, y);
        }

        public void DrawVerticalLine(int x, int lenght)
        {
            WriteAt("+", x, BoardOriginY);
            WriteAt("+", x, BoardOriginY + lenght);

            for (int i = 1; i < lenght; i++)
                WriteAt("│", x, BoardOriginY + i);
        }
        public void DrawPoint(int x, int y)
        {
            WriteAt("+", BoardOriginX + x, BoardOriginY + y);
        }

        public void DrawBoard()
        {
            DrawVerticalLine(BoardOriginX, BoardSizeY);
            DrawVerticalLine(BoardOriginX + BoardSizeX, BoardSizeY);

            DrawHorizontalLine(BoardOriginY, BoardSizeX);
            DrawHorizontalLine(BoardOriginY + BoardSizeY, BoardSizeX);
        }
    }
}
