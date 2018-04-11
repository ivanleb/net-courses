using System;
using Events.Core;
using EventHandler = Events.Core.EventHandler;

namespace Events
{
    internal class Mine : IDrawable, IMine
    {
        public Mine(IPoint position)
        {
            Position = position;
        }

        internal bool IsActive { get; set; }

        public void Draw()
        {
            WriteAt("X", Position.X, Position.Y);
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

        public IPoint Position { get; set; }

        public void CheckHeroPosition(object sender, EventArgs args)
        {
            if (((IHero) sender).NextPosition.X == Position.X && ((IHero) sender).NextPosition.Y == Position.Y)
            {
                Boom?.Invoke(this, EventArgs.Empty);
                IsActive = false;
            }
        }

        public event EventHandler Boom;
    }
}