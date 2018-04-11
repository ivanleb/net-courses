using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;
using System.Windows.Forms;

namespace Events.Implementations
{
    class ConsoleMine : IMine
    {
        public int PosX { get; set; }
        
        public int PosY { get; set; }
        
        public event ExplosionHandler OnExplosion;
        public ConsoleMine(int x, int y)
        {
            PosX = x;
            PosY = y;
            this.OnExplosion += ConsoleMine_OnExplosion;
        }

        private void ConsoleMine_OnExplosion(IMine sender, ExplosionEventArgs args)
        {
            MessageBox.Show("Hit!");
        }

        public void ListenToTheHeroes(IEnumerable<IHero> heroes)
        {
            foreach (var hero in heroes)
            {
                hero.OnMove += Hero_OnMove;
            }
        }

        private void Hero_OnMove(IHero sender, MovingArgs args)
        {
            var coloredSender = sender as ColoredConsoleHero;
            if ((sender.PosX==this.PosX) & (sender.PosY == this.PosY))
            {
                OnExplosion?.Invoke(this, new ExplosionEventArgs() { corpse = sender });
            }
        }
    }
}
