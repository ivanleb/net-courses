using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.Abstractions
{
    public class MovingArgs : GameEventArgs { }
    public delegate void MovingHandler(IHero sender, MovingArgs args);
    public interface IHero
    {
        int PosX { get; set; }
        int PosY { get; set; }
        string Mark { get; }
        void ListenToTheInput(IUserInput input);
        void ListenToTheOtherHeroes(IEnumerable<IHero> heroes);
        event MovingHandler OnMove;
    }
}
