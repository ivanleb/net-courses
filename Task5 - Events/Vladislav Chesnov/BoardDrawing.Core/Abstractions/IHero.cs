using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public delegate void HeroMovesHandler(object sender, GameEventArgs eventArgs);

    public interface IHero
    {
        int PosX { get; set; }
        int PosY { get; set; }
        char Sym { get; set; }
        void StartListenInput(IUserInteraction input);
    }
}
