using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Abstractions
{
    public interface IRegistry
    {
        IShowDrawingToUser ShowDrawingToUser { get; set; }
        IProcessUserInput ProcessUserInput { get; set; }
        IBoard GetEmptyBoard { get; }
    }
}
