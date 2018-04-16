using System;
using HeroesCore.Abstractions;

namespace HeroesGame.Implementations
{
    class MinHero : IHero
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public string MarkSymbol { get { return "X"; } }

        public void StartListenInput(IUserIteraction input)
        {
            input.HeroTripMine += OnHeroTripMine; 
        }

        private void OnHeroTripMine(IHero mine, GameEventArgs eventArgs)
        {
            HeroToucheMine(mine);  
        }

        public void HeroToucheMine(IHero mine)
        {
            if (mine.PosX == this.PosX && mine.PosY == this.PosY)
            {
                Console.SetCursorPosition(mine.PosX, mine.PosY);
                Console.WriteLine("Hit!");
            }
        }
    }
}
