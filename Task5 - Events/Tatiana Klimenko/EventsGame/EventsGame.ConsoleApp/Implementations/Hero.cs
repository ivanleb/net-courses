using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsGame.Core.Abstractions;

namespace EventsGame.ConsoleApp.Implementations
{
    class Hero : IHero
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        private IBoard Board { get; set; }

        public void SetupHeroInitialPosition(int x, int y, IBoard board)
        {
            PosX = x;
            PosY = y;
            Board = board;
        }

        public void StartListenInput(IUserInteraction input)
        {
            input.InputReceived += OnInputReceived;
        }

        private void OnInputReceived(object sender, IGameEventArgs eventArgs)
        {
            if (eventArgs.ReceivedCommand.Key == System.ConsoleKey.LeftArrow)
                if (PosX != 1) PosX--;
            if (eventArgs.ReceivedCommand.Key == System.ConsoleKey.RightArrow)
                if (PosX != Board.SizeX - 2) PosX++;
            if (eventArgs.ReceivedCommand.Key == System.ConsoleKey.UpArrow)
                if (PosY != 1) PosY--;
            if (eventArgs.ReceivedCommand.Key == System.ConsoleKey.DownArrow)
                if (PosY != Board.SizeY - 2) PosY++;
        }
    }
}
