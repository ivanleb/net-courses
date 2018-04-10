using System;
using System.Collections.Generic;
using RuslanTask5.Abstractions;

namespace RuslanTask5.Implementations
{
    class DrawAllComponents : IDrawing
    {
        public void Draw(char symbol, int positionX, int positionY)
        {
            try
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(symbol);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine("\n\n");
        }

        public  void DrawBoard(IBoard board)
        {
            try
            {
                for (int line = board.StartBoardPositionX; line <= board.SizeX; line++)
                    for (int topBottomLine = board.StartBoardPositionY; topBottomLine <= board.SizeY; topBottomLine += board.SizeY)
                        Draw('*', line, topBottomLine);

                for (int line = board.StartBoardPositionY + 1; line < board.SizeY; line++)
                    for (int leftRightLine = board.StartBoardPositionX; leftRightLine <= board.SizeX; leftRightLine += board.SizeX)
                        Draw('*', leftRightLine, line);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public  void DrawHero(IHero hero)
        {
            Draw(hero.Marker, hero.PositionX, hero.PositionY);
        }

        public  void BoardReactionOnHero(IBoard board, IHero hero)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (hero.PositionX == board.StartBoardPositionX)
                for (int boardSide = board.StartBoardPositionY; boardSide <= board.SizeY; boardSide++)
                    Draw('*', board.StartBoardPositionX, boardSide);
            if (hero.PositionX == board.StartBoardPositionX + board.SizeX)
                for (int boardSide = board.StartBoardPositionY; boardSide <= board.SizeY; boardSide++)
                    Draw('*', board.StartBoardPositionX + board.SizeX, boardSide);
            if (hero.PositionY == board.StartBoardPositionY)
                for (int boardSide = board.StartBoardPositionX; boardSide <= board.SizeX; boardSide++)
                    Draw('*', boardSide, board.StartBoardPositionY);
            if (hero.PositionY == board.StartBoardPositionY + board.SizeY)
                for (int boardSide = board.StartBoardPositionX; boardSide <= board.SizeX; boardSide++)
                    Draw('*', boardSide, board.StartBoardPositionY + board.SizeY);
            Console.ResetColor();
        }


    }

}

