using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;

namespace Events.Implementations
{
    class ColoredConsoleHero : IHero
    {
        public char Mark { get { return '+'; } set { } }
        public int PosX { get; set; }
        public int PosY { get; set; }
        private ConsoleKey Up, Down, Left, Right;
        public ConsoleColor Color { get; protected set; }

        private bool isDead = false;
        public bool IsDead { get { return isDead; } protected set { isDead = value; } }

        private IBoard Board;

        public ColoredConsoleHero(IBoard board, int x, int y, ConsoleColor color, ConsoleKey up, ConsoleKey down, ConsoleKey left, ConsoleKey right)
        {
            this.Board = board;
            Color = color;
            PosX = x;
            PosY = y;
            Up = up;
            Down = down;
            Right = right;
            Left = left;
        }

        private bool allowMove;
        public void DenyMoving()
        {
            this.allowMove = false;
        }
        public event MovingHandler TryMove;
        public event MovingHandler OnMove;

        public void ListenToTheInput(IUserInput input)
        {
            input.OnInput += OnCommandRecieved;
        }
        private void OnCommandRecieved(IUserInput sender, InputEventArgs args)
        {
            this.allowMove = true;
            bool movingAttemp = false;
            ConsoleKey command = args.input.Key;
            MovingArgs arg = new MovingArgs();
            if (command == this.Up)
            {
                movingAttemp = true;
                if (this.PosY > 0) { arg.dy = -1; }
            }
            if (command == this.Down)
            {
                movingAttemp = true;
                if (this.PosY < this.Board.Height) { arg.dy = 1; }
            }
            if (command == this.Left)
            {
                movingAttemp = true;
                if (this.PosX > 0) { arg.dx = -1; }
            }
            if (command == this.Right)
            {
                movingAttemp = true;
                if (this.PosX < this.Board.Width) { arg.dx = 1; }
            }
            if (movingAttemp)
            {
                this.TryMove?.Invoke(this, arg);
                if (this.allowMove)
                {
                    this.PosX += arg.dx;
                    this.PosY += arg.dy;
                    this.OnMove?.Invoke(this, arg);
                }
            }
        }

        public void ListenToTheOtherHeroes(IEnumerable<IHero> heroes)
        {
            foreach (var hero in heroes)
            {
                if (hero!=this)
                {
                    hero.TryMove += Hero_TryMove;
                }
            }
        }
        private void Hero_TryMove(IHero sender, MovingArgs args)
        {
            if ((sender.PosX + args.dx == this.PosX) & (sender.PosY + args.dy == this.PosY)) sender.DenyMoving();
        }
    }
}
