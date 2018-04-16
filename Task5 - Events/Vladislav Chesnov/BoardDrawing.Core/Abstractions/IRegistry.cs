using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDrawing.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IUserInteraction UserInteraction { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }
        IModel Model { get; set; }
    }
}
