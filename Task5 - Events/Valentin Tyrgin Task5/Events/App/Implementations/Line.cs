using System;
using Events.Core;

namespace Events.App.Implementations
{
    internal class Line : IDrawable
    {
        public Line(Point a, Point b)
        {
            Start = a;
            End = b;
        }

        public Point Start { get; set; }
        public Point End { get; set; }

        public void Draw()
        {
            if (Start.X != End.X)
            {
                if (Start.X > End.X)
                {
                    var t = Start.X;
                    Start.X = End.X;
                    End.X = t;
                }
                for (var i = Start.X; i < End.X; i++)
                    WriteAt("-", i, Start.Y);
            }
            if (Start.Y != End.Y)
            {
                if (Start.Y > End.Y)
                {
                    var t = Start.Y;
                    Start.Y = End.Y;
                    End.Y = t;
                }
                for (var i = Start.Y; i < End.Y; i++)
                    WriteAt("|", Start.X, i);
            }
        }

        public bool Contains(IPoint point)
        {
            return 
                Start.X == End.X && point.X == Start.X
                ||
                Start.Y == End.Y && point.Y == Start.Y;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}