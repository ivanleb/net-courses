using System;
using System.Collections.Generic;
using Task5.Core.Abstractions;
using Task5.Core.Models;

namespace Task5.App.Implementations
{
    public class ConsoleBoardHandler : IBoardHandler
    {
        public Hero Hero { get; set; } = new Hero {PositionX = 1, PositionY = 1, Symbol = "+"};
        public Board Board { get; set; } = new Board() {SizeX = 100, SizeY = 20};
        public IEnumerable<Bomb> Bombs { get; set; }
        private int LastPosX { get; set; } = 1;
        private int LastPosY { get; set; } = 1;

        public void DrawAll()
        {
            PointDraw(0, 0, "*");
            PointDraw(0, Board.SizeY, "*");
            PointDraw(Board.SizeX, 0, "*");
            PointDraw(Board.SizeX, Board.SizeY, "*");

            Console.ForegroundColor = Board.IsTopWallTouched ? ConsoleColor.DarkRed : ConsoleColor.White;
            HorizontalLineDraw(Board, 1, 0);
            Console.ForegroundColor = Board.IsBottomWallTouched ? ConsoleColor.DarkRed : ConsoleColor.White;
            HorizontalLineDraw(Board, 1, Board.SizeY);
            Console.ForegroundColor = Board.IsLeftWallTouched ? ConsoleColor.DarkRed : ConsoleColor.White;
            VerticalLineDraw(Board, 0, 1);
            Console.ForegroundColor = Board.IsRightWallTouched ? ConsoleColor.DarkRed : ConsoleColor.White;
            VerticalLineDraw(Board, Board.SizeX, 1);
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var bomb in Bombs)
            {
                PointDraw(bomb.PositionX, bomb.PositionY, bomb.Symbol);
            }

            PointDraw(LastPosX, LastPosY, " ");
            PointDraw(Hero.PositionX, Hero.PositionY, Hero.Symbol);

            Console.SetCursorPosition(0, Board.SizeY + 1);
        }

        public bool IsValidMove(UserChoise userChoise)
        {
            var hero = MakeFakeMove(userChoise);

            if (hero.PositionX == Board.SizeX)
                return false;
            if (hero.PositionY == Board.SizeY)
                return false;
            if (hero.PositionX == 0)
                return false;
            if (hero.PositionY == 0)
                return false;

            return true;
        }

        private Hero MakeFakeMove(UserChoise userChoise)
        {
            LastPosX = Hero.PositionX;
            LastPosY = Hero.PositionY;
            var hero = new Hero() {PositionX = Hero.PositionX, PositionY = Hero.PositionY};
            hero.OnMove(this, userChoise);
            return hero;
        }

        void HorizontalLineDraw(Board drawedBoard, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < drawedBoard.SizeX - 1; i++)
            {
                Console.Write("-");
            }
        }

        void PointDraw(int left, int top, string symbol)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(symbol);
        }

        void VerticalLineDraw(Board drawedBoard, int left, int top)
        {
            for (int i = 0; i < drawedBoard.SizeY - 1; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write("|");
            }
        }

    }
}
