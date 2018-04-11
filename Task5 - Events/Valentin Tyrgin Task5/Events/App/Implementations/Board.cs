using System;
using System.Linq;
using Events.Core;

namespace Events.App.Implementations
{
    internal class Board : IBoard
    {
        public Board(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Borders = new IDrawable[]
            {
                new Line(new Point(1, 0), new Point(SizeX, 0)),
                new Line(new Point(1, SizeY), new Point(SizeX, SizeY)),
                new Line(new Point(0, 1), new Point(0, SizeY)),
                new Line(new Point(SizeX, 1), new Point(SizeX, SizeY)),
                new Point(0, 0),
                new Point(SizeX, SizeY),
                new Point(SizeX, 0),
                new Point(0, SizeY)
            };
        }

        private IDrawable[] Borders { get; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public void Draw()
        {
            foreach (var ob in Borders) ob.Draw();
        }

        public void CheckHeroPosition(object sender, EventArgs args)
        {
            var nextHeroStep = ((IHero) sender).NextPosition;
            var stepOnBoarder =
                Borders.Where(x => x.GetType().Name == "Line").Any(y => ((Line) y).Contains(nextHeroStep));

            if (!stepOnBoarder)
            {
                Bump?.Invoke(this, new BooleanEventArgs {ok = false});
                Draw();
                return;
            }
            ChangeBorderColor(nextHeroStep);
            Bump?.Invoke(this, new BooleanEventArgs {ok = true});
        }

        public event BumpToBorder Bump;

        public void WriteAt(string s, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void ChangeBorderColor(IPoint point)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (Line line in 
                Borders.Where(x => x.GetType().Name == "Line"
                                   && ((Line) x).Contains(point)))
                line.Draw();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}