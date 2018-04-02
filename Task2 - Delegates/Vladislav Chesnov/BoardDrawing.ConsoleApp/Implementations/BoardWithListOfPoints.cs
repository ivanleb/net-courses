using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class BoardWithListOfPoints : IBoard
    {
        public delegate void DrawFigure(IBoard board);
        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        List<Point> board = new List<Point>();
        List<Point> boardContent = new List<Point>();

        public BoardWithListOfPoints(int boardSizeX, int boardSizeY)
        {
            BoardSizeX = boardSizeX;
            BoardSizeY = boardSizeY;
            Point upperLeftCorner = new Point(0, 0, '+');
            board.Add(upperLeftCorner);
            Point upperRightCorner = new Point(BoardSizeX+1, 0, '+');
            board.Add(upperRightCorner);
            Point lowerLeftCorner = new Point(0, boardSizeY+1, '+');
            board.Add(lowerLeftCorner);
            Point lowerRightCorner = new Point(boardSizeX+1, boardSizeY+1, '+');
            board.Add(lowerRightCorner);
            HorizontalLine upperLine = new HorizontalLine(1, boardSizeX, 0, '-');
            foreach (Point p in upperLine.pList) board.Add(p);
            HorizontalLine lowerLine = new HorizontalLine(1, boardSizeX, boardSizeY+1, '-');
            foreach (Point p in lowerLine.pList) board.Add(p);
            VerticalLine leftLine = new VerticalLine(1, boardSizeY, 0, '|');
            foreach (Point p in leftLine.pList) board.Add(p);
            VerticalLine rightLine = new VerticalLine(1, boardSizeY, boardSizeX+1, '|');
            foreach (Point p in rightLine.pList) board.Add(p);
        }

        public bool Draw(string userChoice, char[] menuItems)
        {
            DrawFigure drawFigure = null;
            char[] arr = userChoice.ToCharArray();
            if (arr.Length < 1 || arr.Length > 4) return false;
            foreach(char c in arr)
            {
                if (!menuItems.Contains(c)) return false;
                switch (c)
                {
                    case '1':
                        drawFigure += DrawPoint;
                        break;
                    case '2':
                        drawFigure += DrawHorizontalLine;
                        break;
                    case '3':
                        drawFigure += DrawVerticalLine;
                        break;
                    case '4':
                        try
                        {
                            ClearBoard();
                        }
                        catch(NullReferenceException e)
                        {                                
                        }                        
                        return true;
                    default:
                        return false;
                }
            }
            drawFigure(this);
            return true;
        }

        public void DrawPoint(IBoard board)
        {
            int coordX;
            coordX = board.BoardSizeX % 4 == 0 ? board.BoardSizeX / 4 : board.BoardSizeX / 4 + 1;
            int coordY;
            coordY = board.BoardSizeY % 4 == 0 ? board.BoardSizeY / 4 : board.BoardSizeY / 4 + 1;
            Point pointToDraw = new Point(coordX, coordY, '+');
            boardContent.Add(pointToDraw);
        }

        public void DrawHorizontalLine(IBoard board)
        {
            HorizontalLine hLineToDraw = new HorizontalLine(1, board.BoardSizeX, board.BoardSizeY / 2, '-');
            foreach (Point p in hLineToDraw.pList) boardContent.Add(p);
        }

        public void DrawVerticalLine(IBoard board)
        {
            VerticalLine vLineToDraw = new VerticalLine(1, board.BoardSizeY, board.BoardSizeX / 2, '|');
            foreach (Point p in vLineToDraw.pList) boardContent.Add(p);
        }

        public void ClearBoard()
        {
            boardContent.Clear();
        }

        public void DrawBoard()
        {
            foreach (Point p in board) p.Draw();
            foreach (Point p in boardContent) p.Draw();
            Console.SetCursorPosition(0, BoardSizeY + 2);
        }        
        
        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}
