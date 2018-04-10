using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsCore.Abstractions;

namespace Events.Implementations
{
    class Model : IModel
    {
        public IEnumerable<IHero> Heroes { get; set; }
        public IEnumerable<IMine> Mines { get; set; }
        public Model(IMine[] mines, IHero[] heroes)
        {
            Heroes = heroes ?? new IHero[0];
            Mines = mines ?? new IMine[0];
        }
    }
}
