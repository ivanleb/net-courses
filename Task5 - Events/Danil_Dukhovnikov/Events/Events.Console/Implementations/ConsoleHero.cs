using System;
using Events.Console.Implementations;
using Events.Core.Abstractions;

namespace Task5_Events.Implementations
{
    public class ConsoleHero : IHero

    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Marker => '+';

        public void StartListeningInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        public event EventHandler<EventArgs> HeroMoved;

        private void OnInputReceived(object sender, EventArgs e)
        {
            var args = (CommandEventArgs) e;

            switch (args.ReceivedCommand)
            {
                case ConsoleKey.LeftArrow when (X > 1):
                    X--;
                    //HeroMoved?.Invoke(this, new HeroMovedEventArgs {NewX = X, NewY = Y});
                    break;
                case ConsoleKey.RightArrow when (X < StaticRegistry.Board.Width - 1):
                    X++;
                    //HeroMoved?.Invoke(this, new HeroMovedEventArgs {NewX = X, NewY = Y});
                    break;
                case ConsoleKey.UpArrow when (Y > 1):
                    Y--;
                    //HeroMoved?.Invoke(this, new HeroMovedEventArgs {NewX = X, NewY = Y});
                    break;
                case ConsoleKey.DownArrow when (this.Y < StaticRegistry.Board.Height - 1):
                    Y++;
                    //HeroMoved?.Invoke(this, new HeroMovedEventArgs {NewX = X, NewY = Y});
                    break;
            }

            HeroMoved?.Invoke(this, new HeroMovedEventArgs {NewX = X, NewY = Y});
        }
    }
}