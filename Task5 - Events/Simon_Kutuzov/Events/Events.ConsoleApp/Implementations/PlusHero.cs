using System;
using Events.Core.Abstractions;

namespace Events.ConsoleApp.Implementations
{
    public class PlusHero : IHero
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Marker { get { return '+'; } }

        public event EventHandler<EventArgs> HeroMoved;

        private void OnInputReceived(object sender, EventArgs e)
        {
            var args = (CommandEventArgs)e;

            if ((args.ReceivedCommand == System.ConsoleKey.LeftArrow) && (this.X > 1))
            {
                this.X--;
                HeroMoved?.Invoke(this, new HeroMovedEventArgs { NewX = X, NewY = Y });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.RightArrow) && (this.X < StaticRegistry.board.Width - 1))
            {
                this.X++;
                HeroMoved?.Invoke(this, new HeroMovedEventArgs { NewX = X, NewY = Y });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.UpArrow) && (this.Y > 1))
            {
                this.Y--;
                HeroMoved?.Invoke(this, new HeroMovedEventArgs { NewX = X, NewY = Y });
            }
            if ((args.ReceivedCommand == System.ConsoleKey.DownArrow) && (this.Y < StaticRegistry.board.Height - 1))
            {
                this.Y++;
                HeroMoved?.Invoke(this, new HeroMovedEventArgs { NewX = X, NewY = Y });
            }
        }

        public void StartListeningInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }
    }
}
