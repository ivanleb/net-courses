using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IModel Model { get; set; }
        IUserInteraction UserInteraction { get; set; }
    }
}
