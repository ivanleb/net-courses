using System;
using System.Collections.Generic;
using Delegates.Core.Abstractions;
using Task2_Delegates_ConsoleApplication.Implementations;

namespace Delegates.ConsoleApp.Implementations
{
    public class ConsoleBoard : IBoard
    {
        private delegate void DrawFigure(IBoard board);
        
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

        public void DrawBoard(DrawType drawType)
        {
            DrawFigure draw = null;
            
            switch (drawType)
            {
                case DrawType.Point:
                    draw += DrawPoint;
                    break;
                case DrawType.VerticalLine:
                    draw += DrawVerticalLine;
                    break;
                case DrawType.HorizontalLine:
                    draw += DrawHorizontalLine;
                    break;
                case DrawType.Stop:
                    throw new ArgumentOutOfRangeException(nameof(drawType), drawType, null);
                    break;
                case DrawType.Error:
                    throw new ArgumentOutOfRangeException(nameof(drawType), drawType, null);
                    break;
                case DrawType.Clear:
                    BoardPoints = new ConsoleBoard(BoardSizeX, BoardSizeY, StartX, StartY).BoardPoints;
                    draw = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(drawType), drawType, null);
            }

            draw?.Invoke(this);
        }

        private static void DrawPoint(IBoard board)
        {
            board.BoardPoints.Add(new ConsolePoint(board.StartX + board.BoardSizeX / 5,
                board.StartY + board.BoardSizeY / 5, "+"));
            
            Draw(board);
        }

        private static void DrawHorizontalLine(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeX; i++)
            {
                board.BoardPoints.Add(new ConsolePoint(board.StartX + i, board.StartY + board.BoardSizeY / 2, "-"));
            }
            
            Draw(board);
        }

        private static void DrawVerticalLine(IBoard board)
        {
            for (var i = 1; i < board.BoardSizeY; i++)
            {
                board.BoardPoints.Add(new ConsolePoint(board.StartX + board.BoardSizeX / 2, board.StartY + i, "|"));
            }
            
            Draw(board);
        }

        private static void Draw(IBoard board)
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