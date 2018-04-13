using System;
using Events.Console.Implementations;
using Events.Core.Abstractions;

namespace Task5_Events.Implementations
{
    public class ConsoleMine : IMine
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Mark => 'x';
        
        public void WatchHeroMove(IHero hero)
        {
            hero.HeroMoved += OnHeroMoved;
        }

        private void OnHeroMoved(object sender, EventArgs e)
        {
            var args = (HeroMovedEventArgs) e;

            if (args.NewX.Equals(X) && args.NewY.Equals(Y))
            {
                Console.Clear();
                Console.WriteLine("Hit");
                Console.WriteLine("Game over");
                Environment.Exit(0);
            }
        }
    }
}