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
        public IEnumerable<IHero> Heroes { get; set; }
        public IEnumerable<IMine> Mines { get; set; }

        public ConsoleAppModel(IHero[] heroes, IMine[] mines)
        {
            Heroes = heroes;
            Mines = mines;
        }
    }
}
