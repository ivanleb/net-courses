using System;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    class StarHero : IHero
    {

        public int PosX { get; set; }

        public int PosY { get; set;}

        public string MarkSymbol
        {
            get
            {
                return "*";
            }

        }

        public void StartListenInput(IUserIteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, GameEventArgs eventArgs)
        {
            CommandEventArgs args = (CommandEventArgs)eventArgs;
            if (args.ReceivedCommand.Key == System.ConsoleKey.LeftArrow)
                PosX--;
            if (args.ReceivedCommand.Key == System.ConsoleKey.RightArrow)
                PosX++;
            if (args.ReceivedCommand.Key == System.ConsoleKey.UpArrow)
                PosY--;
            if (args.ReceivedCommand.Key == System.ConsoleKey.DownArrow)
                PosY++;
        }       
    }
}
