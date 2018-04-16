using System;
using Events.Core;
using EventHandler = Events.Core.EventHandler;

namespace Events.App.Implementations
{
    internal class Hero : IHero
    {
        private ConsoleColor color = ConsoleColor.White;

        private bool isActive;

        public Hero(IPoint a)
        {
            Position = a;
            NextPosition = new Point(a.X, a.Y);
            PreviousPosition = new Point(a.X, a.Y);
            isActive = true;
        }

        private IPoint PreviousPosition { get; }
        public IPoint NextPosition { get; }

        public void Switch(object sender, BooleanEventArgs args)
        {
            isActive = !args.ok;
        }

        public void HeroMove(object sender, UserInputEventArgs args)
        {
            //if (!isActive) return;
            switch (args.Key)
            {
                case ConsoleKey.UpArrow:
                    NextPosition.Y--;
                    break;
                case ConsoleKey.DownArrow:
                    NextPosition.Y++;
                    break;
                case ConsoleKey.LeftArrow:
                    NextPosition.X--;
                    break;
                case ConsoleKey.RightArrow:
                    NextPosition.X++;
                    break;
            }
            HeroEvent?.Invoke(this, EventArgs.Empty);
            Update();
            color = ConsoleColor.White;
        }

        public IPoint Position { get; set; }

        public void Draw()
        {
            WriteAt("+", Position.X, Position.Y);
        }

        public event EventHandler HeroEvent;

        public void AndNowImDead(object sender, EventArgs args)
        {
            color = ConsoleColor.Red;
            Update();
            isActive = false;
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

        private void Update()
        {
            if (isActive)
            {
                WriteAt(" ", PreviousPosition.X, PreviousPosition.Y);
                Position.X = NextPosition.X;
                Position.Y = NextPosition.Y;
                PreviousPosition.X = Position.X;
                PreviousPosition.Y = Position.Y;
            }
            else
            {
                NextPosition.X = Position.X;
                NextPosition.Y = Position.Y;
            }
            Console.ForegroundColor = color;
            Draw();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}