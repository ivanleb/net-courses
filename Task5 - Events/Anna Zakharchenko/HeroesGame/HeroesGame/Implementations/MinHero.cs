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
            input.InputReceived += OnHeroTripMine; 
        }

        private void OnHeroTripMine(object semder, GameEventArgs eventArgs)
        {
            CommandEventArgs args = (CommandEventArgs)eventArgs;
            HeroToucheMine(args.WithMinModel);  
        }

        public void HeroToucheMine(IModel model)
        {
            if (model.Hero.PosX == this.PosX && model.Hero.PosY == this.PosY)
            {
                Console.SetCursorPosition(model.Hero.PosX, model.Hero.PosY);
                Console.WriteLine("Hit!");
            }
        }
    }
}
