using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.Abstractions
{
    public class ExplosionEventArgs : GameEventArgs { }
    public delegate void ExplosionHandler(IMine sender, ExplosionEventArgs args);
    public interface IMine
    {
        int PosX { get; set; }
        int PosY { get; set; }
        void ListenToTheHeroes(IEnumerable<IHero> heroes);
        event ExplosionHandler OnExplosion;
    }
}
