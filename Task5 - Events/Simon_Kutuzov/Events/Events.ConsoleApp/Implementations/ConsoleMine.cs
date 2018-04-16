using System;
using System.Windows.Forms;
using Events.Core.Abstractions;

namespace Events.ConsoleApp.Implementations
{
    public class ConsoleMine : IMine
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Mark { get { return 'X'; } }

        private void OnHeroMoved(object sender, EventArgs e)
        {
            var args = (HeroMovedEventArgs)e;

            if ((args.NewX == X) && (args.NewY == Y))
            {
                Console.Clear();
                Console.WriteLine("Hit");
                Console.WriteLine("Game over");
                Environment.Exit(0);
            }
        }

        public void WatchHeroMove(IHero hero)
        {
            hero.HeroMoved += this.OnHeroMoved;
        }
    }
}
