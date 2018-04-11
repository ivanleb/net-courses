using Game.Core;
using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class ConsoleMine : IMine
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public ConsoleMine(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public char MineSymbol { get { return 'X'; } }

        public void StartListenHero(IUserInteraction userInteraction)
        {
            userInteraction.StepOnMine += HeroTouchedMine;
        }

        public void HeroTouchedMine(IHero hero, GameEventArgs eventArgs)
        {
            if (hero.PositionX == this.PositionX && hero.PositionY == this.PositionY)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hit!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
