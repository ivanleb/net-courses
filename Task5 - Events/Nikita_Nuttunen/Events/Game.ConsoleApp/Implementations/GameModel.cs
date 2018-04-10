using Game.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.ConsoleApp.Implementations
{
    public class GameModel : IModel
    {
        public IHero Hero { get; set; }
        public IEnumerable<IMine> Mines { get; set; }

        public GameModel(IHero hero, IMine[] mines)
        {
            this.Hero = hero;
            this.Mines = mines;
        }
    }
}
