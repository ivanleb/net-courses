using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ConsoleMine : Point, IMine
    {
        public ConsoleMine(int x, int y, char c) : base(x, y, c)
        {
            PosX = x;
            PosY = y;
            Sym = c;
        }

        public void OnHeroStepOnMine(IHero hero, MineArgs args)
        {
            if(hero.PosX == PosX && hero.PosY == PosY)
            {
                Console.SetCursorPosition(0, args.WhereToWrite + 2);
                Console.WriteLine("HIT!");
            }
        }

        public void StartListenHero(IUserInteraction input)
        {
            input.HeroStepsOnMine += OnHeroStepOnMine;
        }
    }
}


