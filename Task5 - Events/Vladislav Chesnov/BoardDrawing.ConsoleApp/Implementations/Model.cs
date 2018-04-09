using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardDrawing.Core.Abstractions;

namespace BoardDrawing.ConsoleApp.Implementations
{
    class Model : IModel
    {
        public IEnumerable<IHero> Heroes { get; set; }

        public Model(params IHero[] heroes)
        {
            Heroes = heroes;
        }
    }
}
