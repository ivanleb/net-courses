using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public interface IModel
    {
        IHero Hero { get; set; }
        IEnumerable<IMine> Mines { get; set; }
    }
}
