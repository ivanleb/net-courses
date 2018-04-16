using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public interface IHero
    {    
        int PositionX { get; set; }
        int PositionY { get; set; }
        char HeroSymbol { get; }

        void StartListenInput(IUserInteraction userInteraction);
    }
}
