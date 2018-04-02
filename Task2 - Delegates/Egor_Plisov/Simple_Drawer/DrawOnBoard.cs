using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Drawer
{
    class DrawOnBoard : IBoard
    {
        private Random randome = new Random();
        private const int minNum = 20;
        private const int maxNum = 25;
        public int x;
        private int y;
        private int curNumOfX = 0;
        private int curNumOfY = 0;


        public DrawOnBoard()
        {
            x = randome.Next(minNum, maxNum);
            y = randome.Next(minNum, maxNum);

        }


        private static void WriteAt(string s, int xPos, int yPos)
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(s);
        }

        public void DrawBoarder()
        {
            
            Console.SetCursorPosition(0, 0);
            do
            {
                if (curNumOfX == 0 || curNumOfX == x)
                {
                    WriteAt("+", curNumOfX++, 0);
                } else {
                    WriteAt("-", curNumOfX++, 0);
                }

            } while (curNumOfX < x);

            do
            {
                if (curNumOfY == 0 || curNumOfY == y)
                {
                    WriteAt("+", x, curNumOfY++);
                } else {
                    WriteAt("|", x, curNumOfY++);
                }
            } while (curNumOfY < y);

            do
            {
                if (curNumOfX == 0 || curNumOfX == x)
                {
                    WriteAt("+", curNumOfX--, y);
                } else {
                    WriteAt("-", curNumOfX--, y);
                }
            } while (curNumOfX != 0);

            do
            {
                if (curNumOfY == 0 || curNumOfY == y)
                {
                    WriteAt("+", 0, curNumOfY--);
                } else {
                    WriteAt("|", 0, curNumOfY--);
                }
            } while (curNumOfY != 0);

            Console.SetCursorPosition(0, x + 1);
        }

        public void DrawPoint()
        {
            WriteAt("+", x / 4, y / 4);
        }

        public void DrawHorizontalLine()
        {
            curNumOfX = 1;
            int centerY = y / 2;
            do
            {
                WriteAt("-", curNumOfX++, centerY);
            } while (curNumOfX < x);
        }

        public void DrawVerticalLine()
        {
            curNumOfY = 1;
            int centerX = x / 2;
            do
            {
                WriteAt("|", centerX, curNumOfY++);
            } while (curNumOfY < y);
        }
    }
}
