using Game.Core;
using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class ConsoleHero : IHero
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public ConsoleHero(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public char HeroSymbol { get { return '*'; } }

        
        public void StartListenInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
            var args = (CommandEventArgs)eventArgs;

            if (args.ReceivedCommand.Key == ConsoleKey.LeftArrow) PositionX--;
            if (args.ReceivedCommand.Key == ConsoleKey.RightArrow) PositionX++;
            if (args.ReceivedCommand.Key == ConsoleKey.UpArrow) PositionY--;
            if (args.ReceivedCommand.Key == ConsoleKey.DownArrow) PositionY++;
        }
    }
}
