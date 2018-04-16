using System.Collections.Generic;

namespace Events.Core.Abstractions
{
    public interface IModel
    {
        IHero Hero { get; set; }
        IList<IMine> Mines { get; set; }
    }
}
