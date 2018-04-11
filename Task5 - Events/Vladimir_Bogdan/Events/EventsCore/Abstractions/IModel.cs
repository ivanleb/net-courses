using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCore.Abstractions
{
    public interface IModel
    {
        IEnumerable<IHero> Heroes { get; set; }
        IEnumerable<IMine> Mines { get; set; }
    }
}
