using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drawing.Core.Abstractions
{
    public interface IRegistry
    {
        IBoard Board { get; set; }
        IItemsBuilder ItemsBuilder { get; set; }
        IProcessUserActions ProcessUserActions { get; set; }
        IShowMessageToUser ShowMessageToUser { get; set; }
    }
}
