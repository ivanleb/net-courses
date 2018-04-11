using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class ConsoleAppModel : IModel
    {

        public IEnumerable<IMine> Mines { get; set; }
        public IHero Hero { get; set; }

        public ConsoleAppModel(IHero hero, IMine[] mines)
        {
            Hero = hero;
            Mines = mines;
        }
    }
}
