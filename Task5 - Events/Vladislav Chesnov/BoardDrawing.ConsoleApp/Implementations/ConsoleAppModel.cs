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

        public ConsoleAppModel(params IHero[] heroes)
        {
            Heroes = heroes;
        }
    }
}
