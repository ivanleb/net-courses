using System;
using System.Collections.Generic;
using Delegates.Core.Abstractions;
using Task2_Delegates_ConsoleApplication.Implementations;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleBoard : IBoard
    {
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        
        public int BoardSizeX { get; }
        public int BoardSizeY { get; }
        public IList<IPoint> BoardPoints { get; private set; }

        public ConsoleBoard(int boardSizeX, int boardSizeY, int startX = 0, int startY = 0)
        {
            StartX = startX;
            StartY = startY;
            
            BoardSizeX = boardSizeX;
            BoardSizeY = boardSizeY;
            BoardPoints = new List<IPoint>();
            

            var upperLeftCorner = new ConsolePoint(StartX, StartY, "+");
            BoardPoints.Add(upperLeftCorner);
            var upperRightCorner = new ConsolePoint(BoardSizeX + StartX, startY, "+");
            BoardPoints.Add(upperRightCorner);
            var lowerLeftCorner = new ConsolePoint(StartX, BoardSizeY + StartY, "+");
            BoardPoints.Add(lowerLeftCorner);
            var lowerRightCorner = new ConsolePoint(BoardSizeX + StartX, BoardSizeY + StartY, "+");
            BoardPoints.Add(lowerRightCorner);

            
            
            for (var i = 1; i < BoardSizeX; i++)
            {
                BoardPoints.Add(new ConsolePoint(StartX + i, StartY, "-"));
                BoardPoints.Add(new ConsolePoint(StartX + i, BoardSizeY + StartY, "-"));
            }

            for (var i = 1; i < BoardSizeY; i++)
            {
                BoardPoints.Add(new ConsolePoint(StartX, StartY + i, "|"));
                BoardPoints.Add(new ConsolePoint(BoardSizeX + StartX, StartY + i, "|"));
            }
        }

        public void AddOnBoard(DrawType drawType)
        {
            IDraw draw = null;
            
            switch (drawType)
            {
                case DrawType.Point:
                    draw = new ConsoleDrawPoint();
                    break;
                case DrawType.VerticalLine:
                    draw = new ConsoleDrawVerticalLine();
                    break;
                case DrawType.HorizontalLine:
                    draw = new ConsoleDrawHorizontLine();
                    break;
                case DrawType.Stop:
                    throw new ArgumentOutOfRangeException(nameof(drawType), drawType, null);
                    break;
                case DrawType.Error:
                    throw new ArgumentOutOfRangeException(nameof(drawType), drawType, null);
                    break;
                case DrawType.Clear:
                    draw = new ConsoleBoard(BoardSizeX, BoardSizeY, StartX, StartY);
                    BoardPoints = new ConsoleBoard(BoardSizeX, BoardSizeY, StartX, StartY).BoardPoints;
                    break;
                default:
                    draw = new ConsoleBoard(BoardSizeX, BoardSizeY, StartX, StartY);
                    break;
                    
            }

            draw?.Draw(this);
        }      
        
        void IDraw.Draw(IBoard board)
        {
            Console.Clear();
            
            foreach (var point in board.BoardPoints)
            {
                point.Draw();
            }
            
            Console.SetCursorPosition(0, board.BoardSizeY + board.StartY + 2);
        }
    }
}