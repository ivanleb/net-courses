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
            Console.CursorVisible = false;
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
            drawAll += DrawMines;
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
            (model.Hero as ConsoleAppHero).DrawPoint();
        }

        public void DrawMines(IModel model)
        {
            foreach(ConsoleMine mine in model.Mines)
            {
                mine.DrawPoint();
            }
        }

        public void CheckIfHitWithWalls(IModel model)
        {            
            if (model.Hero.PosX > BoardSizeX)
            {
                model.Hero.PosX = BoardSizeX;
                right.DrawShapeRed();
            }
            if (model.Hero.PosX < 1)
            {
                model.Hero.PosX = 1;
                left.DrawShapeRed();
            }

            if (model.Hero.PosY > BoardSizeY)
            {
                model.Hero.PosY = BoardSizeY;
                lower.DrawShapeRed();
            }

            if (model.Hero.PosY < 1)
            {
                model.Hero.PosY = 1;
                upper.DrawShapeRed();
            }
        }

        public void BoardClear()
        {
            Console.Clear();
        }

        public void OnInputRecieved(object sender, GameEventArgs eventArgs)
        {
            Draw(model);
        }

        public void StartListenInput(IUserInteraction userInteraction)
        {
            userInteraction.InputRecieved += OnInputRecieved;
        }
    }
}
