using System.Collections.Generic;

namespace HeroesCore.Abstractions
{
    public interface IModel
    {
        IHero Hero { get; set; }
        IEnumerable<IHero> Mines { get; set; }
    }
}
