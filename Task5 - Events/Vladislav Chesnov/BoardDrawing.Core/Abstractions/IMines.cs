using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public delegate void ExplosionHandler(IHero hero, MineArgs args);

    public interface IMine
    {
        int PosX { get; set; }
        int PosY { get; set; }
        char Sym { get; set; }
        void StartListenHero(IUserInteraction userInteraction);
    }
}
