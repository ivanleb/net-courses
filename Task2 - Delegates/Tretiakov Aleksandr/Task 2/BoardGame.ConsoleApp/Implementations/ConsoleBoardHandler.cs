using System;
using BoardGame.Core.Abstractions;
using BoardGame.Core.Models;

namespace BoardGame.ConsoleApp.Implementations;
{
    public class ConsoleBoardHandler : IBoardHandler
    {
        public Draw DrawBoard { get; private set; }

        private ConsoleBoardHandler()
        {

        }

        public static ConsoleBoardHandler GetInstance()
        {
            var handler = new ConsoleBoardHandler();
            handler.DrawBoard = BoardDraw;
            return handler;
        }

        static void BoardDraw(Board drawedBoard)
        {
            Console.Clear();
            PointDraw(0, 0);
            PointDraw(0, drawedBoard.SizeY);
            PointDraw(drawedBoard.SizeX, 0);
            PointDraw(drawedBoard.SizeX, drawedBoard.SizeY);

            HorizontalLineDraw(drawedBoard, 1, 0);
            HorizontalLineDraw(drawedBoard, 1, drawedBoard.SizeY);

            VerticalLineDraw(drawedBoard, 0, 1);
            VerticalLineDraw(drawedBoard, drawedBoard.SizeX, 1);

            if (drawedBoard.IsVerticalLineShown)
                VerticalLineDraw(drawedBoard, (int)drawedBoard.SizeX / 2, 1);

            if (drawedBoard.IsHorizontalLineShown)
                HorizontalLineDraw(drawedBoard, 1, (int)drawedBoard.SizeY / 2);

            if (drawedBoard.IsPointShown)
                PointDraw((int)drawedBoard.SizeX / 4, (int)drawedBoard.SizeY / 4);

            Console.SetCursorPosition(0, drawedBoard.SizeY + 1);
        }

        static void HorizontalLineDraw(Board drawedBoard, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < drawedBoard.SizeX - 1; i++)
            {
                Console.Write("-");
            }
        }

        static void PointDraw(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("+");
        }

        static void VerticalLineDraw(Board drawedBoard, int left, int top)
        {
            for (int i = 0; i < drawedBoard.SizeY - 1; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write("|");
            }
        }
    }
}
