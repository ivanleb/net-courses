using System;
using System.Collections.Generic;
using Events.Core;

namespace Events.App.Implementations
{
    internal class Mines : IDrawable, IMines
    {
        private static readonly Random random = new Random();
        private readonly IBoard board;

        public Mines(int count, IBoard board)
        {
            this.board = board;
            FewMines = new List<IMine>(count);
            for (var i = 0; i < count; i++)
                FewMines.Add(new Mine(new Point(random.Next(1, board.SizeX - 1), random.Next(1, board.SizeY - 1))));
        }

        public void Draw()
        {
            foreach (var mine in FewMines)
                mine.Draw();
        }

        public void WriteAt(string s, int x, int y)
        {
            throw new NotImplementedException();
        }

        public List<IMine> FewMines { get; set; }

        public void OnExplosion(object sender, EventArgs args)
        {
            ((Mine) sender).Position.X = random.Next(1, board.SizeX - 1);
            ((Mine) sender).Position.Y = random.Next(1, board.SizeY - 1);
            Draw();
        }
    }
}