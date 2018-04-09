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
        delegate void DrawFigure(IModel model);
        DrawFigure drawAll;

        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }
        List<Point> corneres = new List<Point>();
        HorizontalLine upper;
        HorizontalLine lower;
        VerticalLine left;
        VerticalLine right;
        List<Shape> borders = new List<Shape>();
        List<Point> boardContent = new List<Point>();
        IModel model;

        public BoardWithListOfPoints(IModel model)
        {
            this.model = model;
        }

        public void PrepareBoard(int boardSizeX, int boardSizeY)
        {
            BoardSizeX = boardSizeX;
            BoardSizeY = boardSizeY;
            Point upperLeftCorner = new Point(0, 0, '+');
            corneres.Add(upperLeftCorner);
            Point upperRightCorner = new Point(BoardSizeX + 1, 0, '+');
            corneres.Add(upperRightCorner);
            Point lowerLeftCorner = new Point(0, boardSizeY + 1, '+');
            corneres.Add(lowerLeftCorner);
            Point lowerRightCorner = new Point(boardSizeX + 1, boardSizeY + 1, '+');
            corneres.Add(lowerRightCorner);
            upper = new HorizontalLine(1, boardSizeX, 0, '-');
            borders.Add(upper);
            lower = new HorizontalLine(1, boardSizeX, boardSizeY + 1, '-');
            borders.Add(lower);
            left = new VerticalLine(1, boardSizeY, 0, '|');
            borders.Add(left);
            right = new VerticalLine(1, boardSizeY, boardSizeX + 1, '|');
            borders.Add(right);
                        
            drawAll += DrawBoard;
            drawAll += CheckIfHitWithWalls;
            drawAll += DrawHero;
        }

        public void Draw(IModel model)
        {
            BoardClear();
            if(drawAll != null)
            {
                drawAll(model);
            }
        }
        public void DrawBoard(IModel model)
        {
            foreach (var p in borders) p.DrawShape();
            foreach (var p in corneres) p.DrawPoint();
        }

        public void DrawHero(IModel model)
        {
            foreach(ConsoleAppHero hero in model.Heroes)
            {
                hero.DrawPoint();
            }
        }

        public void CheckIfHit(IModel model)
        {

        }

        public void CheckIfHitWithWalls(IModel model)
        {
            foreach(ConsoleAppHero hero in model.Heroes)
            {
                if (hero.PosX > BoardSizeX)
                {
                    hero.PosX = BoardSizeX;
                    right.DrawShapeRed();
                }
                if (hero.PosX < 1)
                {
                    hero.PosX = 1;
                    left.DrawShapeRed();
                }

                if (hero.PosY > BoardSizeY)
                {
                    hero.PosY = BoardSizeY;
                    lower.DrawShapeRed();
                }

                if (hero.PosY < 1)
                {
                    hero.PosY = 1;
                    upper.DrawShapeRed();
                }
            }
        }

        public void BoardClear()
        {
            Console.Clear();
        }

        public void StartListenInput(IUserInteraction input)
        {
            input.InputRecieved += OnInputRecieved;
        }

        public void OnInputRecieved(object sender, GameEventArgs eventArgs)
        {
            Draw(model);
        }

        //public bool IsHit(Point p)
        //{
        //    foreach(Point point in board)
        //    {
        //        if (p.IsHit(point))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}




    }
}
