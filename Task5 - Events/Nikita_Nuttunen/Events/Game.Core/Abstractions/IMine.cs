using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public interface IMine
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        char MineSymbol { get; }

        void StartListenHero(IUserInteraction userInteraction);
    }
}
